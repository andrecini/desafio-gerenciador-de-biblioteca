using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;
using System.Text.Json;

namespace Desafios.GerenciadorBiblioteca.Website.Services.Auth
{
    public class CustomAuthStateProvider(TokenStorageService tokenService) : AuthenticationStateProvider
    {
        private readonly TokenStorageService _tokenService = tokenService;

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var tokenModel = _tokenService.TokenModel ;
            var identity = new ClaimsIdentity();

            if (tokenModel != null)
            {
                var token = tokenModel.Token;

                if (!string.IsNullOrEmpty(token))
                    identity = new ClaimsIdentity(ParseClaimsFromJwt(token), "jwt");

            }
            var user = new ClaimsPrincipal(identity);
            var state = new AuthenticationState(user);

            NotifyAuthenticationStateChanged(Task.FromResult(state));

            return state;
        }

        public static IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
        {
            var payload = jwt.Split('.')[1];
            var jsonBytes = ParseBase64WithoutPadding(payload);
            var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);
            return keyValuePairs.Select(kvp => new Claim(kvp.Key, kvp.Value.ToString()));
        }

        private static byte[] ParseBase64WithoutPadding(string base64)
        {
            switch (base64.Length % 4)
            {
                case 2: base64 += "=="; break;
                case 3: base64 += "="; break;
            }
            return Convert.FromBase64String(base64);
        }
    }
}
