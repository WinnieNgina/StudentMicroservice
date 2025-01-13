using Microsoft.Extensions.Options;
using System.Runtime;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace StudentMicroservice.Bank;


public class BankService : IBankService
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly BankApiOptions _bankApiOptions;

    public BankService(IOptions<BankApiOptions> options, IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
        _bankApiOptions = options.Value;
    }

    public async Task<IEnumerable<BankInfo>> GetAllBanksAsync()
    {
        var client = _httpClientFactory.CreateClient();
        var request = new HttpRequestMessage(HttpMethod.Get, _bankApiOptions.BaseUrl);


        request.Headers.Add("Ocp-Apim-Subscription-Key", _bankApiOptions.ApiKey);

        try
        {
            var response = await client.SendAsync(request); 
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


