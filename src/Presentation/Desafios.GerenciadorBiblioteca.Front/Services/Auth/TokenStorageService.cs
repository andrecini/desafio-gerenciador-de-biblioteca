using Blazored.LocalStorage;
using Desafios.GerenciadorBiblioteca.Domain.Enums;
using Desafios.GerenciadorBiblioteca.Service.DTOs.ViewModels;
using Desafios.GerenciadorBiblioteca.Website.Models.Responses;

namespace Desafios.GerenciadorBiblioteca.Website.Services.Auth
{
    public class TokenStorageService(ILocalStorageService localStorage)
    {
        public TokenModel TokenModel { get; set; }

        private readonly ILocalStorageService _localStorage = localStorage;

        public async Task SetTokenIdentityProperty()
        {
            TokenModel = await GetTokenIdentity();
        }

        public async Task SetTokenIdentity(TokenModel tokenViewModel)
        {
            await _localStorage.SetItemAsync("id", tokenViewModel.Id);
            await _localStorage.SetItemAsync("name", tokenViewModel.Name);
            await _localStorage.SetItemAsync("token", tokenViewModel.Token);
            await _localStorage.SetItemAsync("role", tokenViewModel.Role);
            await _localStorage.SetItemAsync("validity", tokenViewModel.ValidTo);

            await SetTokenIdentityProperty();
        }

        public async Task<TokenModel> GetTokenIdentity()
        {
            var id = await _localStorage.GetItemAsync<int>("id");
            var token = await _localStorage.GetItemAsync<string>("token");
            var role = await _localStorage.GetItemAsync<Roles>("role");
            var name = await _localStorage.GetItemAsync<string>("name");
            var validity = await _localStorage.GetItemAsync<DateTime>("validity");

            return new() 
            { 
                Id = id,
                Token = token, 
                Role = role,
                Name = name,
                ValidTo = validity,
            };
        }

        public async Task ClearTokenIdentity()
        {
            await _localStorage.ClearAsync();
        }
    }
}
