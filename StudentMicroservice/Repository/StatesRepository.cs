using StudentMicroservice.Data;
using StudentMicroservice.IRepository;
using StudentMicroservice.Models;

namespace StudentMicroservice.Repository;

public class StatesRepository : IStatesRepository
{
    private readonly ApplicationDbContext _context;
    public StatesRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<State> AddStateAsync(State state)
    {
        var result = await _context.states.AddAsync(state);
        return result.Entity;
    }
}
