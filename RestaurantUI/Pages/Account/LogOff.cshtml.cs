using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RestaurantUI.Contracts;

namespace RestaurantUI.Pages.Account
{
    public class LogOffModel : PageModel
    {
        private readonly IAuthService _authService;
        public LogOffModel(IAuthService authService)
        {
            _authService = authService;
        }
        public async Task<ActionResult> OnGet()
        {
            await _authService.LogOffAsync();
            return RedirectToPage("/Account/Login");
        }
    }
}