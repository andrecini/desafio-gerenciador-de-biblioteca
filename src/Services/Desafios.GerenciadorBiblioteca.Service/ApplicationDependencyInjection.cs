using Desafios.GerenciadorBiblioteca.Service.Mapping;
using Desafios.GerenciadorBiblioteca.Service.Security;
using Desafios.GerenciadorBiblioteca.Service.Security.Interfaces;

using Microsoft.Extensions.DependencyInjection;

namespace Desafios.GerenciadorBiblioteca.Service
{
    public static class ApplicationDependencyInjection
    {
        public static IServiceCollection AddApplicationModule(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(MappingProfile));

            services.AddTransient<ICipherService, CipherService>();
            services.AddTransient<ITokenService, TokenService>();
            services.AddTransient<IHangfireApiManager, HangfireApiManager>();

            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies()));

            return services;
        }
    }
}
