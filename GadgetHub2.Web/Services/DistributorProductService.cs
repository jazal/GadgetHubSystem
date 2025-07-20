using GadgetHub2.Web.Models;
using System.Net.Http.Json;

namespace GadgetHub2.Web.Services
{
    public class DistributorProductService
    {
        private readonly HttpClient _httpClient;

        public DistributorProductService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // Fetches all distributor products from their respective APIs
        public async Task<List<ProductDto>> GetAllDistributorProductsAsync(List<Products> distributors)
        {
            var allProducts = new List<ProductDto>();

            foreach (var distributor in distributors)
            {
                try
                {
                    var products = await _httpClient.GetFromJsonAsync<List<ProductDto>>(distributor.ApiUrl);
                    if (products != null)
                        allProducts.AddRange(products);
                }
                catch
                {
                    // Optionally log or handle error
                }
            }

            return allProducts;
        }

        // Fetches all distributors or Products from the GadgetHub API 
        internal async Task<List<Products>> GetAllAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<Products>>("https://localhost:7166/api/products,https://localhost:7003/api/products,")
                   ?? new List<Products>();
        }
    }
}
