using RestaurantUI.Contracts;

namespace RestaurantUI.Services
{
    public class AuthService : IAuthService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task LogOffAsync()
        {
            _httpContextAccessor.HttpContext.Session.Remove("authToken");
            _httpContextAccessor.HttpContext.Session.Remove("user");
            _httpContextAccessor.HttpContext.Response.Redirect("/");
            await Task.CompletedTask;
        }
    }
}
