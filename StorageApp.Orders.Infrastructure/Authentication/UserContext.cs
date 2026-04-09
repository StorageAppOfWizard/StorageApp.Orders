using Microsoft.AspNetCore.Http;
using StorageApp.Orders.Domain.Contracts;
using System.Security.Claims;

namespace StorageApp.Orders.Infrastructure.Authentication
{
    public class UserContextAuth : IUserContextAuth
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserContextAuth(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string UserId => _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value
        ?? throw new UnauthorizedAccessException();

        public string UserName => _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.Name)?.Value
        ?? throw new UnauthorizedAccessException();

        public bool IsAuthenticated => _httpContextAccessor.HttpContext?.User?.Identity?.IsAuthenticated ?? false;
    }
}
