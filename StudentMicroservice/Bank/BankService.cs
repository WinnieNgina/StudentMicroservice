using Microsoft.Extensions.Options;
using System.Runtime;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace StudentMicroservice.Bank;


public class BankService : IBankService
{
    private readonly HttpClient _client;
    private readonly BankApiOptions _bankApiOptions;

    public BankService(IOptions<BankApiOptions> options, HttpClient client)
    {
        _client = client;
        _bankApiOptions = options.Value;
    }

    public async Task<IEnumerable<BankInfo>> GetAllBanksAsync()
    {
        var request = new HttpRequestMessage(HttpMethod.Get, _bankApiOptions.BaseUrl);


        request.Headers.Add("Ocp-Apim-Subscription-Key", _bankApiOptions.ApiKey);

        try
        {
            var response = await _client.SendAsync(request);
            response.EnsureSuccessStatusCode(); 
            var responseContent = await response.Content.ReadAsStringAsync(); 
            var responseObject = JsonSerializer.Deserialize<Response>(responseContent);
            return responseObject?.Result ?? new List<BankInfo>();
        }
        catch
        {
         
            throw;
        }
    }
    private class Response 
    {
        [JsonPropertyName("result")]
        public List<BankInfo> Result { get; set; }
    }
}


