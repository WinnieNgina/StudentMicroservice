using StudentMicroservice.Dtos;

namespace StudentMicroservice.IRepository;

public interface IStudentsRepository
{
    Task<IEnumerable<ResponseStudentDto>> GetStudentsAsync();
}
