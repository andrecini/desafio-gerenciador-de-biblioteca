using Desafios.GerenciadorBiblioteca.Domain.Application.Services;
using Desafios.GerenciadorBiblioteca.Service.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Desafios.GerenciadorBiblioteca.CrossCutting.IoC
{
    public static class ApplicationDependencyInjection
    {
        public static IServiceCollection AddApplicationModule(this IServiceCollection services)
        {
            services.AddScoped<ILibraryService, LibrarySevice>();
            services.AddScoped<IBookService, BookSevice>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IInventoryService, InventoryService>();
            services.AddScoped<ILoanService, LoanService>();

            return services;
        }
    }
}
