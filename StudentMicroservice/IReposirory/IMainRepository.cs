using Microsoft.EntityFrameworkCore.Storage;

namespace StudentMicroservice.IReposirory;

public interface IMainRepository
{
    Task<int> SaveChangesAsync();
    Task<IDbContextTransaction> BeginTransactionAsync();
}
