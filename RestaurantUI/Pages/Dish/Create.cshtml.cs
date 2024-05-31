using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RestaurantUI.Model.Dish;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Security.Claims;

namespace RestaurantUI.Pages.Dish
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
        public DishVM DishVM { get; set; }

        [BindProperty(SupportsGet = true)]
        public int RestaurantId { get; set; }

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


            DishVM = new DishVM { RestaurantId = RestaurantId };
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

            var response = await _httpClient.PostAsJsonAsync($"https://localhost:7160/api/{RestaurantId}/dish", DishVM);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToPage("/Dish/Index", new { restaurantId = RestaurantId });
            }

            ModelState.AddModelError(string.Empty, "An error occurred while creating the dish.");
            return Page();
        }
    }
}