using Microsoft.EntityFrameworkCore.Storage;

namespace StudentMicroservice.IRepository;

public interface IMainRepository
{
    Task<int> SaveChangesAsync();
    Task<IDbContextTransaction> BeginTransactionAsync();
}
