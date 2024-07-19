using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using System.Text.Json;
using Desafios.GerenciadorBiblioteca.Service.DTOs.ViewModels;

namespace Desafios.GerenciadorBiblioteca.Website.Services.Auth
{
    public class AuthService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task LoginAsync(TokenViewModel tokenModel)
        {
            var claims = JwtParserService.ParseClaimsFromJwt(tokenModel.Token);
            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);

            await _httpContextAccessor.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, new AuthenticationProperties
            {
                IsPersistent = true,
                ExpiresUtc = tokenModel.ValidTo
            });

            var tokenModelJson = JsonSerializer.Serialize(tokenModel);

            // Store the serialized TokenModel in a cookie
            _httpContextAccessor.HttpContext.Response.Cookies.Append("AuthToken", tokenModelJson, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = tokenModel.ValidTo
            });
        }

        public async Task LogoutAsync()
        {
            await _httpContextAccessor.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            _httpContextAccessor.HttpContext.Response.Cookies.Delete("AuthToken");
        }

        public TokenViewModel GetTokenModel()
        {
            if (_httpContextAccessor.HttpContext.Request.Cookies.TryGetValue("AuthToken", out string tokenModelJson))
            {
                return JsonSerializer.Deserialize<TokenViewModel>(tokenModelJson);
            }

            return null;
        }
    }
}
