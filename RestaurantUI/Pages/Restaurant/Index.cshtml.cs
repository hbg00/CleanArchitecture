using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RestaurantUI.Model.Restaurant;
using System.Net.Http.Headers;

namespace RestaurantUI.Pages.Restaurant
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
        public List<RestaurantVM> Restaurants { get; set; }
        public string claim { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var token = _httpContextAccessor.HttpContext.Session.GetString("authToken");

            if (string.IsNullOrEmpty(token))
            {
                return Forbid("You must be logged");
            }

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.GetAsync("https://localhost:7160/api/Restaurant");

            if (response.IsSuccessStatusCode)
            {
                Restaurants = await response.Content.ReadFromJsonAsync<List<RestaurantVM>>();
                return Page();
            }
            else
            {
                Restaurants = new List<RestaurantVM>();
                return Page();
            }
        }
    }
}