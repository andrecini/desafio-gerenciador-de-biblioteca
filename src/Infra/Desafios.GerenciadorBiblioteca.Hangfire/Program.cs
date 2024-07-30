using Desafios.GerenciadorBiblioteca.Data;
using Desafios.GerenciadorBiblioteca.Hangfire.Jobs;
using Desafios.GerenciadorBiblioteca.Hangfire.Services.Email;
using Desafios.GerenciadorBiblioteca.Service;
using Hangfire;

var builder = WebApplication.CreateBuilder(args);

// Secrets Configuration
if (builder.Environment.IsDevelopment())
{
    builder.Configuration.AddUserSecrets<Program>();
}

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddApplicationModule();
builder.Services.AddDataModule(builder.Configuration);
builder.Services.AddScoped<EmailService>();
builder.Services.AddScoped<OverdueLoanAlarmJob>();

var conn = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddHangfire((sp, config) =>
    config.UseSqlServerStorage(conn)
);

builder.Services.AddHangfireServer();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseHangfireDashboard();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
