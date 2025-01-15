using Microsoft.EntityFrameworkCore;
using StudentMicroservice.Data;
using StudentMicroservice.Dtos;
using StudentMicroservice.IRepository;

namespace StudentMicroservice.Repository;

public class StudentsRepository : IStudentsRepository
{
    private readonly ApplicationDbContext _context;

    public StudentsRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<IEnumerable<ResponseStudentDto>> GetStudentsAsync()
    {
        return await _context.Users
            .Include(s => s.Lga)
            .ThenInclude(l => l.State)
            .Select(s => new ResponseStudentDto
            {
                PhoneNumber = s.PhoneNumber,
                Email = s.Email,
                StateOfResidence = s.Lga.State.StateName,
                Lga = s.Lga.LgaName
            })
            .ToListAsync();
    }
}