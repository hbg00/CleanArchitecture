using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RestaurantUI.Model.Dish;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Security.Claims;

namespace RestaurantUI.Pages.Dish
{
    public class IndexModel : PageModel
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public IndexModel(HttpClient httpClient, IHttpContextAccessor httpContextAccessor)
        {
            _httpClient = httpClient;
            _httpContextAccessor = httpContextAccessor;
        }

        [BindProperty]
        public List<DishVM> Dishes { get; set; }

        [BindProperty(SupportsGet = true)]
        public int RestaurantId { get; set; }

        public async Task<ActionResult> OnGetAsync()
        {
            var token = _httpContextAccessor.HttpContext.Session.GetString("authToken");

            if (string.IsNullOrEmpty(token))
            {
                return Forbid();
            }
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.GetAsync($"https://localhost:7160/api/{RestaurantId}/dish");

            if (response.IsSuccessStatusCode)
            {
                Dishes = await response.Content.ReadFromJsonAsync<List<DishVM>>();
                return Page();
            }
            else
            {
                Dishes = new List<DishVM>();
                return Page();
            }
        }
    }
}