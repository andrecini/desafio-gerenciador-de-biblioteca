using Desafios.GerenciadorBiblioteca.Website.Components;
using MudBlazor.Services;
using Desafios.GerenciadorBiblioteca.Service;
using Desafios.GerenciadorBiblioteca.Data;
using System.Net.Http.Headers;
using Desafios.GerenciadorBiblioteca.Website.Services;
using Desafios.GerenciadorBiblioteca.Website.Services.Auth;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System.Text.Json;
using System.Text.Encodings.Web;
using System.Text;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddApplicationModule();
builder.Services.AddDataModule(builder.Configuration);

builder.Services.AddMudServices();
builder.Services.AddServerSideBlazor().AddCircuitOptions(options => { options.DetailedErrors = true; });

builder.Services.AddAuthentication();
builder.Services.AddAuthorization();

builder.Services.AddBlazoredLocalStorage(config => {
    config.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
    config.JsonSerializerOptions.WriteIndented = true;
    config.JsonSerializerOptions.Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping;
});

builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();

builder.Services.AddScoped<TokenStorageService>();
builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped(sp => new HttpService(builder.Configuration["API:BaseUrl"], sp.GetService<TokenStorageService>()));
builder.Services.AddScoped<AlertService>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
