//using GadgetHub2.WEB.Pages.Account;

//namespace GadgetHub2.WEB.Services
//{
//    public class LoginService
//    {
//        private readonly HttpClient _httpClient;

//        public LoginService(HttpClient httpClient)
//        {
//            _httpClient = httpClient;
//        }

//        public async Task<bool> LoginAsync(LoginModel login)
//        {
//            var response = await _httpClient.PostAsJsonAsync("api/user/login", login);
//            return response.IsSuccessStatusCode;
//        }
//    }
//}
