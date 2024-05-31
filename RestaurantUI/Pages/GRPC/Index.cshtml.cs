using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;
using GrpcService.Protos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace RestaurantUI.Pages.GRPC
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly UserServiceClient _userServiceClient;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public IndexModel(ILogger<IndexModel> logger, UserServiceClient userServiceClient, IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;
            _userServiceClient = userServiceClient;
        }

        public UserList Users { get; private set; }

        public async Task<IActionResult> OnGetAsync()
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

            try
            {
                Users = await _userServiceClient.ListUsersAsync();
                Page();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error: {ex.Message}");
               
            }
            return Page();
        }

        public async Task<IActionResult> OnPostDeleteUserAsync(string userId)
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
            try
            {
                await _userServiceClient.DeleteUserAsync(userId);
                Users = await _userServiceClient.ListUsersAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error deleting user: {ex.Message}");
            }
            return RedirectToPage();
        }
    }
}
