using StudentMicroservice.Models;

namespace StudentMicroservice.IReposirory;

public interface IStatesRepository
{
    Task<State> AddStateAsync(State state);
}
