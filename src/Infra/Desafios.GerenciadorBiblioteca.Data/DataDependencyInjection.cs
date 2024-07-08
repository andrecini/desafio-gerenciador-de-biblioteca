using Desafios.GerenciadorBiblioteca.Data.Context;
using Desafios.GerenciadorBiblioteca.Data.Repositories;
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
            services.AddDbContext<LibraryDbContext>(options => options.UseSqlServer(connectionString), ServiceLifetime.Transient);

            services.AddTransient<IUnitOfWork, UnitOfWork.UnitOfWork>();

            services.AddTransient<ILibraryRepository, LibraryRepository>();
            services.AddTransient<IBookRepository, BookRepository>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IInventoryRepository, InventoryRepository>();
            services.AddTransient<ILoanRepository, LoanRepository>();

            return services;
        }
    }
}
