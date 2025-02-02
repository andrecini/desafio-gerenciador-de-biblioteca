using Desafios.GerenciadorBiblioteca.Api.Configurators;
using Desafios.GerenciadorBiblioteca.Api.Handlers;
using Desafios.GerenciadorBiblioteca.Data;
using Desafios.GerenciadorBiblioteca.Service;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Layer's Modules
builder.Services.AddDataModule(builder.Configuration);
builder.Services.AddApplicationModule();

// Swashbuckle - API Documentation
builder.Services.ConfigureSwagger(builder.Environment.EnvironmentName);

// Add Authentication and Authorization
builder.Services.ConfigureAuth(builder.Configuration);

//Serialization
builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
    options.SerializerOptions.WriteIndented = true;
    options.SerializerOptions.Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping;
    options.SerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
});

// Handlers
builder.Services.AddExceptionHandler<CustomExceptionHandler>();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.RoutePrefix = "swagger";
    });
}

app.UseExceptionHandler();

app.UseAuthentication();
app.UseAuthorization();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
