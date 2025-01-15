using Microsoft.EntityFrameworkCore.Storage;
using StudentMicroservice.Data;
using StudentMicroservice.IRepository;

namespace StudentMicroservice.Repository;

public class MainRepository : IMainRepository
{
    private readonly ApplicationDbContext _context;
    public MainRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public async Task<IDbContextTransaction> BeginTransactionAsync()
    {
        return await _context.Database.BeginTransactionAsync();
    }
}
