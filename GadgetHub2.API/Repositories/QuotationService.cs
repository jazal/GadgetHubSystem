using GadgetHub.API.Base;
using GadgetHub.API.Models;
using GadgetHub.Dtos;
using GadgetHub.Dtos.Quotations;

namespace GadgetHub.API.Repositories;

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

    public async Task AddRange(List<CreateQuotationDto> input)
    {
        #region Delete the previous qoutation of that distributor on that order

        var previousQoutation = _repo.GetAll().Where(x =>
            x.DistributorId == input[0].DistributorId &&
            x.OrderItemId == input[0].OrderId
        );

        foreach (var item in previousQoutation)
        {
            await Delete(item.Id);
        }

        #endregion

        foreach (var item in input)
        {
            await Add(new Quotation
            {
                DistributorId = item.DistributorId,
                OrderId = item.OrderId,
                OrderItemId = item.OrderItemId,
                Price = item.Price,
                Quantity = item.Quantity,
                EstimatedDeliveryDays = item.EstimatedDeliveryDays,
                CreatedOn = item.CreatedOn,
            });
        }
    }

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


