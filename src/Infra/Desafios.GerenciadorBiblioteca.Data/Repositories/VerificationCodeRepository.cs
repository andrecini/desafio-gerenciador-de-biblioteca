using Desafios.GerenciadorBiblioteca.Data.Context;
using Desafios.GerenciadorBiblioteca.Data.Repositories.Base;
using Desafios.GerenciadorBiblioteca.Domain.Entities;
using Desafios.GerenciadorBiblioteca.Domain.Repositories;
using Microsoft.Extensions.Configuration;

namespace Desafios.GerenciadorBiblioteca.Data.Repositories
{
    public class VerificationCodeRepository(LibraryDbContext context, IConfiguration configuration) : GenericRepository<VerificationCode, int>(context), IVerificationCodeRepository
    {
        
    }
}
