using GadgetHub.Dtos.Distributors;
using GadgetHub.Dtos.Order;
using Microsoft.Extensions.Options;

namespace GadgetHub.API.Distributors;

public class ExternalApiService
{
    private readonly HttpClient _httpClient;
    private readonly List<ExternalApiEndpoint> _apiSettings;

    public ExternalApiService(HttpClient httpClient, IOptions<List<ExternalApiEndpoint>> apiSettings)
    {
        _httpClient = httpClient;
        _apiSettings = apiSettings.Value;
    }

    public async Task<List<QuotationsViewModel>> GetAllOrdersFromExternalApisAsync(FilterQuotationDto filter)
    {
        var allOrders = new List<QuotationsViewModel>();

        foreach (var api in _apiSettings)
        {
            try
            {
                _httpClient.BaseAddress = new Uri(api.BaseUrl);

                var response = await _httpClient.PostAsJsonAsync(api.Endpoint, filter);

                if (response.IsSuccessStatusCode)
                {
                    var orders = await response.Content.ReadFromJsonAsync<List<QuotationsViewModel>>();
                    if (orders != null)
                        allOrders.AddRange(orders);
                }
                else
                {
                    // Optional: Log failure
                    Console.WriteLine($"Failed API: {api.Name}, Status: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                // Optional: Log exception
                Console.WriteLine($"Error calling API {api.Name}: {ex.Message}");
            }
        }

        return allOrders;
    }
}

