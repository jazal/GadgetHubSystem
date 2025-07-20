using GadgetHub2.Web.Models;
using System.Net.Http.Json;

namespace GadgetHub2.Web.Services;

public class CustomerService
{
    private readonly HttpClient _httpClient;

    public CustomerService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    //Get all customers
    public async Task<List<CustomerDto>> GetAllAsync()
    {
        return await _httpClient.GetFromJsonAsync<List<CustomerDto>>("https://localhost:7003/api/Customer");
    }

    //  Get a single customer by ID
    public async Task<CustomerDto> GetByIdAsync(int id)
    {
        return await _httpClient.GetFromJsonAsync<CustomerDto>($"https://localhost:7003/api/Customer/{id}");
    }

    // Create a new customer
    public async Task<HttpResponseMessage> CreateAsync(CustomerDto customer)
    {
        return await _httpClient.PostAsJsonAsync("https://localhost:7003/api/Customer", customer);
    }

    //  Update an existing customer
    public async Task<HttpResponseMessage> UpdateAsync(CustomerDto customer)
    {
        return await _httpClient.PutAsJsonAsync($"https://localhost:7003/api/Customer/{customer.Id}", customer);
    }

    // Delete a customer by ID
    public async Task<HttpResponseMessage> DeleteAsync(int id)
    {
        return await _httpClient.DeleteAsync($"https://localhost:7003/api/Customer/{id}");
    }

    }
