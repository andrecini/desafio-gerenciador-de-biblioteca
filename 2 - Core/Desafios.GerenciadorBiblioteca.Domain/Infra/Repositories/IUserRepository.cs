using Desafios.GerenciadorBiblioteca.Domain.Infra.Entities;
using Desafios.GerenciadorBiblioteca.Domain.Infra.Repositories.Base;

namespace Desafios.GerenciadorBiblioteca.Domain.Infra.Repositories
{
    public interface IUserRepository : IGenericRepository<User, int>
    {
    }
}
