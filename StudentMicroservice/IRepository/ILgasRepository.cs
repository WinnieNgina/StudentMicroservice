using StudentMicroservice.Models;

namespace StudentMicroservice.IRepository;

public interface ILgasRepository
{
    Task<Lga> AddLgaAsync(Lga lga);
}
