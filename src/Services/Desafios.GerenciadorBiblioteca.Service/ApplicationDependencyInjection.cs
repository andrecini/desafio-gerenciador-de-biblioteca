using Desafios.GerenciadorBiblioteca.Service.Mapping;
using Desafios.GerenciadorBiblioteca.Service.Security;
using Desafios.GerenciadorBiblioteca.Service.Security.Interfaces;
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

            services.AddTransient<ILibraryService, LibraryService>();
            services.AddTransient<IBookService, BookService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IInventoryService, InventoryService>();
            services.AddTransient<ILoanService, LoanService>();

            services.AddTransient<ICipherService, CipherService>();
            services.AddTransient<ITokenService, TokenService>();

            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies()));

            return services;
        }
    }
}
