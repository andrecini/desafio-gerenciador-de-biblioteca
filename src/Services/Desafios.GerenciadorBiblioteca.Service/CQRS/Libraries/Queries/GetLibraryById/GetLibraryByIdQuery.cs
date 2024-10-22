﻿using Desafios.GerenciadorBiblioteca.Domain.Entities;
using Desafios.GerenciadorBiblioteca.Service.DTOs;
using MediatR;

namespace Desafios.GerenciadorBiblioteca.Service.CQRS.Libraries.Queries.GetLibraryById
{
    public record GetLibraryByIdQuery(int Id) : IRequest<CustomResponse<Library>>;
}
