using GadgetHub2.WEB.Models;

namespace GadgetHub2.WEB.Services;

public class UserService
{
    private readonly HttpClient _httpClient;

    public UserService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<CustomerViewModel>> GetAllAsync()
    {
        return await _httpClient.GetFromJsonAsync<List<CustomerViewModel>>("api/user");
    }

    public async Task<CustomerViewModel> GetByIdAsync(int id)
    {
        return await _httpClient.GetFromJsonAsync<CustomerViewModel>($"api/user/{id}");
    }

    public async Task CreateAsync(CustomerViewModel customer)
    {
        await _httpClient.PostAsJsonAsync("api/user", customer);
    }

    public async Task UpdateAsync(int id, CustomerViewModel customer)
    {
        var response = await _httpClient.PutAsJsonAsync($"api/user/{id}", customer);
        response.EnsureSuccessStatusCode(); // <--- this will throw if it fails silently
    }
    

    public async Task DeleteAsync(int id)
    {
        await _httpClient.DeleteAsync($"api/user/{id}");
    }

    //Distrubutor View List
    public async Task<List<DistributorViewModel>> GetDistributorsAsync()
    {
        var users = await _httpClient.GetFromJsonAsync<List<DistributorViewModel>>("api/user");
        return users.Where(u => u.UserType == 1).ToList();
    }
}

