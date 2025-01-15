using StudentMicroservice.Data;
using StudentMicroservice.IRepository;
using StudentMicroservice.Models;

namespace StudentMicroservice.Repository;

public class LgasRepository : ILgasRepository
{
    private readonly ApplicationDbContext _context;
    public LgasRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<Lga> AddLgaAsync(Lga lga)
    {
        var result = await _context.Lgas.AddAsync(lga);
        return result.Entity;
    }
}
