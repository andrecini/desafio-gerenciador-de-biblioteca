﻿using Desafios.GerenciadorBiblioteca.Domain.Entities;
using Desafios.GerenciadorBiblioteca.Domain.Repositories;
using Desafios.GerenciadorBiblioteca.Infra.Context;
using Desafios.GerenciadorBiblioteca.Infra.Repositories.Base;

namespace Desafios.GerenciadorBiblioteca.Infra.Repositories
{
    public class LibraryRepository(LibraryDbContext context) : GenericRepository<Library, int>(context), ILibraryRepository
    {
    }
}
