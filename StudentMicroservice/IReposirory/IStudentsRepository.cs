using StudentMicroservice.Dtos;

namespace StudentMicroservice.IReposirory;

public interface IStudentsRepository
{
    Task<IEnumerable<ResponseStudentDto>> GetStudentsAsync();
}
