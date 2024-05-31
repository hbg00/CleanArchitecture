using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RestaurantUI.Model.Restaurant;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Threading.Tasks;

namespace RestaurantUI.Pages.Restaurant
{
    public class CreateModel : PageModel
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CreateModel(HttpClient httpClient, IHttpContextAccessor httpContextAccessor)
        {
            _httpClient = httpClient;
            _httpContextAccessor = httpContextAccessor;

        }

        [BindProperty]
        public CreateRestaurantVM CreateRestaurantVM { get; set; }

        public async Task<IActionResult> OnGet()
        {
            var token = _httpContextAccessor.HttpContext.Session.GetString("authToken");

            if (string.IsNullOrEmpty(token))
            {
                return Forbid();
            }

            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(token) as JwtSecurityToken;


            if (!jsonToken.Claims.Any(c => c.Type == ClaimTypes.Role && c.Value == "Administrator"))
            {
                return Forbid();
            }


            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var token = _httpContextAccessor.HttpContext.Session.GetString("authToken");

            if (string.IsNullOrEmpty(token))
            {
                return Forbid();
            }

            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(token) as JwtSecurityToken;


            if (!jsonToken.Claims.Any(c => c.Type == ClaimTypes.Role && c.Value == "Administrator"))
            {
                return Forbid();
            }

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            if (!ModelState.IsValid)
            {
                return Page();
            }

            var response = await _httpClient.PostAsJsonAsync("https://localhost:7160/api/Restaurant", CreateRestaurantVM);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToPage("/Restaurant/Index");
            }

            ModelState.AddModelError(string.Empty, "An error occurred while creating the restaurant.");
            return Page();
        }
    }
}