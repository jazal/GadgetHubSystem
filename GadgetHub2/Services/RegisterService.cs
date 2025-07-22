using GadgetHub2.WEB.Models;

namespace GadgetHub2.WEB.Services;

public class RegisterService
{
    private readonly HttpClient _httpClient;

    public RegisterService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task RegisterAsync(RegisterViewModel model)
    {
        await _httpClient.PostAsJsonAsync("api/user", model);
    }
}
