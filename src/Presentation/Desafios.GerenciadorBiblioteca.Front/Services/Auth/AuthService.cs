using Desafios.GerenciadorBiblioteca.Domain.Enums;
using Desafios.GerenciadorBiblioteca.Website.Models.Responses;
using Microsoft.AspNetCore.Components.Authorization;

namespace Desafios.GerenciadorBiblioteca.Website.Services.Auth
{
    public class AuthService(TokenStorageService tokenService, AuthenticationStateProvider authenticationStateProvider)
    {
        private readonly TokenStorageService _tokenService = tokenService;
        private readonly AuthenticationStateProvider _authenticationStateProvider = authenticationStateProvider;

        public async Task Login(TokenModel tokenViewModel)
        {
            await _tokenService.SetTokenIdentity(tokenViewModel);
            await _authenticationStateProvider.GetAuthenticationStateAsync();
        }

        public async Task Logout()
        {
            await _tokenService.ClearTokenIdentity();
            await _authenticationStateProvider.GetAuthenticationStateAsync();
        }

        public async Task<bool> VerifyUserSessionIsOpenAsync()
        {
            try
            {
                var token = await _tokenService.GetTokenIdentity();

                if (token == null)
                    return false;

                if (token.ValidTo >= DateTime.Now)
                {
                    token.ValidTo = DateTime.Now;
                    await Login(token);
                    return true;
                }

                await _tokenService.ClearTokenIdentity();
                return false;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> VerifyIfUserIsAdmin()
        {
            var token = await _tokenService.GetTokenIdentity();
            return token.Role == Roles.Administrator;
        }
    }
}
