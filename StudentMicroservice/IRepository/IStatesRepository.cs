using StudentMicroservice.Models;

namespace StudentMicroservice.IRepository;

public interface IStatesRepository
{
    Task<State> AddStateAsync(State state);
}
