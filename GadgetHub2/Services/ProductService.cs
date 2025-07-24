using GadgetHub.Web.Models;

namespace GadgetHub.Web.Services;

public class ProductService
{
    private readonly HttpClient _httpClient;

    public ProductService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<ProductViewModel>> GetAllAsync()
    {
        return await _httpClient.GetFromJsonAsync<List<ProductViewModel>>("api/Products");
    }

    public async Task<ProductViewModel> GetByIdAsync(int id)
    {
        return await _httpClient.GetFromJsonAsync<ProductViewModel>($"api/products/{id}");
    }

    public async Task CreateAsync(ProductViewModel product)
    {
        await _httpClient.PostAsJsonAsync("api/products", product);
    }

    public async Task UpdateAsync(int id, ProductViewModel product)
    {
        await _httpClient.PutAsJsonAsync($"api/products/{id}", product);
    }

    public async Task DeleteAsync(int id)
    {
        await _httpClient.DeleteAsync($"api/products/{id}");
    }

    
}
