using GadgetHub2.Web.Models;
using System.Net.Http;

namespace GadgetHub2.Web.Services;

public class ProductService
{
    private readonly HttpClient _httpClient;

    public ProductService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<ProductDto>> GetAllAsync()
    {
        return await _httpClient.GetFromJsonAsync<List<ProductDto>>("https://localhost:7003/api/Product");
    }

    public async Task<ProductDto> GetByIdAsync(int id)
    {
        return await _httpClient.GetFromJsonAsync<ProductDto>($"https://localhost:7003/api/Product/{id}");
    }

    public async Task<HttpResponseMessage> CreateAsync(ProductDto product)
    {
        return await _httpClient.PostAsJsonAsync("https://localhost:7003/api/Product", product);
    }

    public async Task<HttpResponseMessage> UpdateAsync(ProductDto product)
    {
        return await _httpClient.PutAsJsonAsync($"https://localhost:7003/api/Product/{product.Id}", product);
    }

    public async Task<HttpResponseMessage> DeleteAsync(int id)
    {
        return await _httpClient.DeleteAsync($"https://localhost:7003/api/Product/{id}");
    }

    internal static async Task<List<ProductDto>> GetAllDistributorProductsAsync(object products)
    {
        throw new NotImplementedException();
    }
}


