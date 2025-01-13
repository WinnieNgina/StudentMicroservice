namespace StudentMicroservice.Bank;

public interface IBankService
{
    Task<IEnumerable<BankInfo>> GetAllBanksAsync();
}
