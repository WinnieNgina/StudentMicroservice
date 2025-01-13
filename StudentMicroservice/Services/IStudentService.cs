using StudentMicroservice.Dtos;

namespace StudentMicroservice.Services;

public interface IStudentService
{
    Task<IEnumerable<ResponseStudentDto>> GetAllStudentsAsync();
    Task<ResponseStudentDto> AddStudentAsync(CreateStudentDto createStudentDto);
}
