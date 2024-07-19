using Desafios.GerenciadorBiblioteca.Website.Components;
using MudBlazor.Services;
using Desafios.GerenciadorBiblioteca.Service;
using Desafios.GerenciadorBiblioteca.Data;
using System.Net.Http.Headers;
using Desafios.GerenciadorBiblioteca.Website.Services;
using Desafios.GerenciadorBiblioteca.Website.Services.Auth;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddApplicationModule();
builder.Services.AddDataModule(builder.Configuration);

builder.Services.AddMudServices();
builder.Services.AddServerSideBlazor().AddCircuitOptions(options => { options.DetailedErrors = true; });

builder.Services.AddAuthentication("Cookies")
           .AddCookie(options =>
           {
               options.Cookie.HttpOnly = true;
               options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
               options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
               options.SlidingExpiration = true;
               options.LoginPath = "/login";
           });
builder.Services.AddAuthorization();

builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped(sp => new HttpService(builder.Configuration["API:BaseUrl"], sp.GetService<AuthService>()));
builder.Services.AddScoped<AlertService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
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
