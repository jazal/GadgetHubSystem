using GadgetHub2.API.Base;
using GadgetHub2.API.DTOs;
using GadgetHub2.API.Models;

namespace GadgetHub2.API.Repositories;

public class QuotationService
{
    private readonly HttpClient _httpClient;
    private readonly IBaseRepository<Quotation> _repo;

    public QuotationService(
        HttpClient httpClient,
        IBaseRepository<Quotation> repo
        )
    {
        _httpClient = httpClient;
        _repo = repo;
    }

    public Task<List<Quotation>> GetAll() => _repo.GetAllAsync();
    public Task<Quotation?> GetById(int id) => _repo.GetByIdAsync(id);
    public Task Add(Quotation order) => _repo.AddAsync(order);
    public Task Update(Quotation order) => _repo.UpdateAsync(order);
    public Task Delete(int id) => _repo.DeleteAsync(id);

    public async Task<List<QuotationResponseDto>> GetQuotationsAsync(QuotationRequestDto request)
    {
        var quotations = new List<QuotationResponseDto>();

        // URLs of distributor APIs
        var distributorApis = new[]
        {
            "https://localhost:7166/api/quotation/get-quotation", // TechWorld
            "https://localhost:7121/api/quotation/get-quotation", // ElectroCom
            "https://localhost:7196/api/quotation/get-quotation"  // Gadget Central
        };

        foreach (var apiUrl in distributorApis)
        {
            var response = await _httpClient.PostAsJsonAsync(apiUrl, request);

            if (response.IsSuccessStatusCode)
            {
                var quote = await response.Content.ReadFromJsonAsync<QuotationResponseDto>();
                if (quote != null)
                {
                    // Add distributor name based on URL
                    quote.DistributorName = GetDistributorName(apiUrl);
                    quotations.Add(quote);
                }
            }
        }

        return quotations;
    }

    private string GetDistributorName(string apiUrl)
    {
        if (apiUrl.Contains("7166")) return "TechWorld";
        if (apiUrl.Contains("7121")) return "ElectroCom";
        if (apiUrl.Contains("7196")) return "Gadget Central";
        return "Unknown";
    }
}


