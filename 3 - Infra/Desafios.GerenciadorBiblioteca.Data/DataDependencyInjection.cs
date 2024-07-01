using Desafios.GerenciadorBiblioteca.Domain.Repositories;
using Desafios.GerenciadorBiblioteca.Domain.UnitOfWork;
using Desafios.GerenciadorBiblioteca.Infra.Context;
using Desafios.GerenciadorBiblioteca.Infra.Repositories;
using Desafios.GerenciadorBiblioteca.Infra.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Desafios.GerenciadorBiblioteca.CrossCutting.IoC
{
    public static class DataDependencyInjection
    {
        public static IServiceCollection AddInfraModule(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<LibraryDbContext>(options => options.UseSqlServer(connectionString), ServiceLifetime.Singleton);

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<ILibraryRepository, LibraryRepository>();
            services.AddScoped<IBookRepository, BookRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IInventoryRepository, InventoryRepository>();
            services.AddScoped<ILoanRepository, LoanRepository>();

            return services;
        }
    }
}
