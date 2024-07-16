﻿using Desafios.GerenciadorBiblioteca.Domain.Entities;
using MediatR;

namespace Desafios.GerenciadorBiblioteca.Service.Libraries.Commands.UpdateLibrary
{
    public record UpdateLibraryCommand(int Id, string Name, string CNPJ, string Phone) : IRequest<Library>;
}
