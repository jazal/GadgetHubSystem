using GadgetHub.Dtos;
using GadgetHub.Web.Models;

namespace GadgetHub.Web.Services;

public class QuotationService
{
    //private readonly HttpClient _httpClient;

    //public QuotationService(HttpClient httpClient)
    //{
    //    _httpClient = httpClient;
    //}

    //public async Task<List<QuotationResponse>> GetQuotationsAsync(QuotationRequest request)
    //{
    //    var response = await _httpClient.PostAsJsonAsync("api/quotation/get-quotations", request);

    //    if (response.IsSuccessStatusCode)
    //    {
    //        var quotations = await response.Content.ReadFromJsonAsync<List<QuotationResponse>>();
    //        return quotations ?? new List<QuotationResponse>();
    //    }

    //    return new List<QuotationResponse>();
    //}

    private readonly HttpClient _httpClient;

    public QuotationService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    // For now, 1 distributor API endpoint
    private readonly string[] distributorEndpoints = new[]
    {
            "https://localhost:7100/api/Quotation/GetQuotation" // Distributor API endpoint
        };

    public async Task<List<QuotationResponseDto>> GetQuotationsAsync(QuotationRequestDto request)
    {
        var responses = new List<QuotationResponseDto>();

        foreach (var endpoint in distributorEndpoints)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync(endpoint, request);
                if (response.IsSuccessStatusCode)
                {
                    var quote = await response.Content.ReadFromJsonAsync<QuotationResponseDto>();
                    if (quote != null)
                    {
                        responses.Add(quote);
                    }
                }
            }
            catch (Exception ex)
            {
                // Log or ignore for now
            }
        }

        return responses;
    }
}
