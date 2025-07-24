using GadgetHub2.WEB.Models;

namespace GadgetHub2.WEB.Services;

public class CartService
{
    private readonly HttpClient _httpClient;

    public CartService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<QuotationResponse>> GetQuotationsAsync(List<CartItem> cart)
    {
        var response = await _httpClient.PostAsJsonAsync("api/quotation", cart);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<List<QuotationResponse>>();
    }

    public async Task PlaceOrderAsync(List<QuotationResponse> selectedItems)
    {
        var response = await _httpClient.PostAsJsonAsync("api/order", selectedItems);
        response.EnsureSuccessStatusCode();
    }
}

