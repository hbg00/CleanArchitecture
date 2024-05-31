using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RestaurantUI.Model.Account;

namespace RestaurantUI.Pages.Account
{
    public class RegisterModel : PageModel
    {
        private readonly HttpClient _httpClient;
        public RegisterModel(HttpClient httpClient)
        {

            _httpClient = httpClient;

        }
        [BindProperty]
        public RegisterVM RegisterVM { get; set; }

        public void OnGet()
        {
        }

        public async Task<ActionResult> OnPost()
        {

            if (!ModelState.IsValid)
            {
                return Page();
            }
            var response = await _httpClient.PostAsJsonAsync($"https://localhost:7160/api/Account/register", RegisterVM);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToPage("/Account/Login");
            }
            return Page();
        }
    }
}
