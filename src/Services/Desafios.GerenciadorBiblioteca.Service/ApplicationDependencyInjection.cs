using Desafios.GerenciadorBiblioteca.Service.Mapping;
using Desafios.GerenciadorBiblioteca.Service.Services;
using Desafios.GerenciadorBiblioteca.Service.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Desafios.GerenciadorBiblioteca.Service
{
    public static class ApplicationDependencyInjection
    {
        public static IServiceCollection AddApplicationModule(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(MappingProfile));

            services.AddScoped<ILibraryService, LibrarySevice>();
            services.AddScoped<IBookService, BookSevice>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IInventoryService, InventoryService>();
            services.AddScoped<ILoanService, LoanService>();

            return services;
        }
    }
}
