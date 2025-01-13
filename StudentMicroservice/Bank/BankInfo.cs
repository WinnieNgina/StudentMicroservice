using System.Text.Json.Serialization;

namespace StudentMicroservice.Bank;

public class BankInfo
{
    [JsonPropertyName("bankName")] 
    public string BankName { get; set; }
    [JsonPropertyName("bankCode")] 
    public string BankCode { get; set; }
    [JsonPropertyName("bankLogo")] 
    public string BankLogo { get; set; }
}
