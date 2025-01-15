using Microsoft.AspNetCore.Identity;
using StudentMicroservice.Data;
using StudentMicroservice.Dtos;
using StudentMicroservice.IRepository;
using StudentMicroservice.Models;

namespace StudentMicroservice.Services;

public class StudentService : IStudentService
{
    private readonly IMainRepository _mainRepository;
    private readonly IStudentsRepository _studentsRepository;
    private readonly ILgasRepository _lgasRepository;
    private readonly IStatesRepository _statesRepository;
    private readonly UserManager<Student> _userManager;
    public StudentService(ApplicationDbContext context,
        IMainRepository mainRepository,
        IStatesRepository statesRepository,
        IStudentsRepository studentsRepository,
        UserManager<Student> userManager,
        ILgasRepository lgasRepository
        )
    {
        _mainRepository = mainRepository;
        _studentsRepository = studentsRepository;
        _lgasRepository = lgasRepository;
        _statesRepository = statesRepository;
        _userManager = userManager;
    }
    public async Task<IEnumerable<ResponseStudentDto>> GetAllStudentsAsync()
    {
        try
        {
            return await _studentsRepository.GetStudentsAsync();
        }
        catch
        {
            throw;
        }
    }

    public async Task<ResponseStudentDto> AddStudentAsync(CreateStudentDto createStudentDto)
    {
        var (state, lga, student) = CreateEntities(createStudentDto);

        using (var transaction = await _mainRepository.BeginTransactionAsync())
        {
            try
            {
                var stateCreated = await _statesRepository.AddStateAsync(state);
                var lgaCreated = await _lgasRepository.AddLgaAsync(lga);
                var result = await _userManager.CreateAsync(student, createStudentDto.Password);
                if (result.Succeeded)
                {
                    await VerifyPhoneNumberAsync(student);
                }
                else 
                { 
                   
                    throw new Exception("User creation failed.");
                }
                await _mainRepository.SaveChangesAsync();
                await transaction.CommitAsync();
                return new ResponseStudentDto
                {
                    PhoneNumber = student.PhoneNumber,
                    Email = student.Email,
                    StateOfResidence = stateCreated.StateName,
                    Lga = lgaCreated.LgaName
                };
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
    }

    public async Task<bool> VerifyPhoneNumberAsync(Student student)
    {
        var code = await _userManager.GenerateChangePhoneNumberTokenAsync(student, student.PhoneNumber);
        Console.WriteLine($"Generated verification code: {code}");
        var result = await _userManager.VerifyChangePhoneNumberTokenAsync(student, code, student.PhoneNumber);
        if (result)
        {
            student.PhoneNumberConfirmed = true;
            await _userManager.UpdateAsync(student);

            return true;
        }
        else
        {
            return false;
        }

    }
    private (State state, Lga lga, Student student) CreateEntities(CreateStudentDto createStudentDto)
    {
        var state = new State
        {
            StateName = createStudentDto.StateOfResidence
        };

        var lga = new Lga
        {
            LgaName = createStudentDto.Lga,
            StateId = state.StateId
        };

        var student = new Student
        {
            UserName = createStudentDto.PhoneNumber,
            PhoneNumber = createStudentDto.PhoneNumber,
            Email = createStudentDto.Email,
            LgaId = lga.LgaId
          
        };

        return (state, lga, student);
    }


}
