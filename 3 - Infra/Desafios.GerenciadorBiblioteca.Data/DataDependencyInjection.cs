using Desafios.GerenciadorBiblioteca.Data.Context;
using Desafios.GerenciadorBiblioteca.Data.Repositories;
using Desafios.GerenciadorBiblioteca.Data;
using Desafios.GerenciadorBiblioteca.Domain.Repositories;
using Desafios.GerenciadorBiblioteca.Domain.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Desafios.GerenciadorBiblioteca.Data
{
    public static class DataDependencyInjection
    {
        public static IServiceCollection AddDataModule(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<LibraryDbContext>(options => options.UseSqlServer(connectionString), ServiceLifetime.Singleton);

            services.AddScoped<IUnitOfWork, UnitOfWork.UnitOfWork>();

            services.AddScoped<ILibraryRepository, LibraryRepository>();
            services.AddScoped<IBookRepository, BookRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IInventoryRepository, InventoryRepository>();
            services.AddScoped<ILoanRepository, LoanRepository>();

            return services;
        }
    }
}
