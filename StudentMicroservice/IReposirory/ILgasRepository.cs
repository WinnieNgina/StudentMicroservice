using StudentMicroservice.Models;

namespace StudentMicroservice.IReposirory;

public interface ILgasRepository
{
    Task<Lga> AddLgaAsync(Lga lga);
}
