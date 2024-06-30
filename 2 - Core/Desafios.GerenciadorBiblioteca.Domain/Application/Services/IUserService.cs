using Desafios.GerenciadorBiblioteca.Domain.Application.Services.Base;
using Desafios.GerenciadorBiblioteca.Domain.Infra.Entities;

namespace Desafios.GerenciadorBiblioteca.Domain.Application.Services
{
    public interface IUserService : IService<User>
    {
        Task<IEnumerable<User>> FindAsync(string name);
    }
}
