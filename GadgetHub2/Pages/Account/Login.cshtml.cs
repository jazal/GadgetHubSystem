using GadgetHub.Dtos.Users;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;

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

            var loginRequest = new LoginDto { Email = this.Email, Password = this.Password };

            var response = await _httpClient.PostAsJsonAsync("User/login", loginRequest);

            if (response.IsSuccessStatusCode)
            {
                LoggedInUser = await response.Content.ReadFromJsonAsync<UserDto>();

                // Set cookie
                HttpContext.Response.Cookies.Append("currentUser", JsonSerializer.Serialize(LoggedInUser), new CookieOptions
                {
                    Expires = DateTimeOffset.UtcNow.AddDays(7), // Cookie expiry
                    HttpOnly = false,  // true means JS can't access this cookie
                    Secure = false,    // true if HTTPS
                    SameSite = SameSiteMode.Lax
                });

                // Redirect after successful login
                return Page();
            }

            ErrorMessage = "Invalid username or password.";
            return null;
        }
    }
}
