using GadgetHub2.API.DTOs.Order;
using GadgetHub2.API.DTOs.Users;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace GadgetHub2.WEB.Pages.Account
{
    public class LoginModel : PageModel
    {
        private readonly HttpClient _httpClient;

        public LoginModel(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("GadgetHubAPI");
        }

        [BindProperty]
        [Required]
        public string Email { get; set; }

        [BindProperty]
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public UserDto LoggedInUser { get; set; }

        public string ErrorMessage { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
                return Page();

            var loginRequest = new { Email = this.Email, Password = this.Password };

            var response = await _httpClient.PostAsJsonAsync("User/login", loginRequest);


            if (response.IsSuccessStatusCode)
            {
                LoggedInUser = await response.Content.ReadFromJsonAsync<UserDto>();

                // Redirect after successful login
                return RedirectToPage("/Index");
            }

            ErrorMessage = "Invalid username or password.";
            return null;
        }
    }
}
