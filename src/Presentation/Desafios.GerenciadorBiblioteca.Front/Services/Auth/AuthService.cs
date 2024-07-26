using Desafios.GerenciadorBiblioteca.Service.DTOs.ViewModels;
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
        public async Task Logout(TokenModel tokenViewModel)
        {
            await _tokenService.ClearTokenIdentity();
            await _authenticationStateProvider.GetAuthenticationStateAsync();
        }

       
    }
}
