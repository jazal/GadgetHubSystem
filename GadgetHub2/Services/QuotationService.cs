using GadgetHub2.WEB.Models;

namespace GadgetHub2.WEB.Services;

public class QuotationService
{
    private readonly HttpClient _httpClient;

    public QuotationService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<QuotationResponse>> GetQuotationsAsync(QuotationRequest request)
    {
        var response = await _httpClient.PostAsJsonAsync("api/quotation/get-quotations", request);

        if (response.IsSuccessStatusCode)
        {
            var quotations = await response.Content.ReadFromJsonAsync<List<QuotationResponse>>();
            return quotations ?? new List<QuotationResponse>();
        }

        return new List<QuotationResponse>();
    }
}


