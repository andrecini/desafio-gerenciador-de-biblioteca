using Desafios.GerenciadorBiblioteca.Data.Context;
using Desafios.GerenciadorBiblioteca.Data.Repositories.Base;
using Desafios.GerenciadorBiblioteca.Domain.Entities;
using Desafios.GerenciadorBiblioteca.Domain.Repositories;

namespace Desafios.GerenciadorBiblioteca.Data.Repositories
{
    public class LibraryRepository(LibraryDbContext context) : GenericRepository<Library, int>(context), ILibraryRepository
    {
    }
}
