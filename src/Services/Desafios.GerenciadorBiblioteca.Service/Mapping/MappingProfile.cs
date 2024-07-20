using AutoMapper;
using Desafios.GerenciadorBiblioteca.Domain.Entities;
using Desafios.GerenciadorBiblioteca.Domain.QueryModels.QueryRequests;
using Desafios.GerenciadorBiblioteca.Domain.QueryModels.QueryResults;
using Desafios.GerenciadorBiblioteca.Service.CQRS.Books.Commands.AddBook;
using Desafios.GerenciadorBiblioteca.Service.CQRS.Books.Queries.GetBooksDetailsByFilter;
using Desafios.GerenciadorBiblioteca.Service.CQRS.Books.Queries.GetBooksDetailsByLibrary;
using Desafios.GerenciadorBiblioteca.Service.CQRS.Inventories.Command.AddInventory;
using Desafios.GerenciadorBiblioteca.Service.CQRS.Libraries.Commands.AddLibrary;
using Desafios.GerenciadorBiblioteca.Service.CQRS.Loans.Commands.AddLoan;
using Desafios.GerenciadorBiblioteca.Service.CQRS.Loans.Queries.GetLoansDetailsByLibrary;
using Desafios.GerenciadorBiblioteca.Service.CQRS.Loans.Queries.GetLoansDetailsFiltered;
using Desafios.GerenciadorBiblioteca.Service.CQRS.Users.Commands.AddUser;
using Desafios.GerenciadorBiblioteca.Service.DTOs.Requests;
using Desafios.GerenciadorBiblioteca.Service.DTOs.Responses;
using System.Diagnostics.CodeAnalysis;

namespace Desafios.GerenciadorBiblioteca.Service.Mapping
{
    [ExcludeFromCodeCoverage]
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Libraries
            CreateMap<AddLibraryCommand, Library>()
                .ForMember(x => x.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(x => x.CNPJ, opt => opt.MapFrom(src => src.CNPJ))
                .ForMember(x => x.Phone, opt => opt.MapFrom(src => src.Phone));

            // Users
            CreateMap<AddUserCommand, User>()
                .ForMember(x => x.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(x => x.Phone, opt => opt.MapFrom(src => src.Phone))
                .ForMember(x => x.Email, opt => opt.MapFrom(src => src.Email));
            
            CreateMap<User, UserRegisterInputModel>()
                .ForMember(x => x.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(x => x.Phone, opt => opt.MapFrom(src => src.Phone))
                .ForMember(x => x.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(x => x.Password, opt => opt.MapFrom(src => src.Password));
            
            CreateMap<User, UserViewModel>()
                .ForMember(x => x.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(x => x.Phone, opt => opt.MapFrom(src => src.Phone))
                .ForMember(x => x.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(x => x.Role, opt => opt.MapFrom(src => src.Role));

            // Books
            CreateMap<AddBookCommand, Book>()
                .ForMember(x => x.Title, opt => opt.MapFrom(src => src.Title))
                .ForMember(x => x.Author, opt => opt.MapFrom(src => src.Author))
                .ForMember(x => x.ISBN, opt => opt.MapFrom(src => src.ISBN))
                .ForMember(x => x.Year, opt => opt.MapFrom(src => src.Year));

            CreateMap<GetBooksDetailsByLibraryQuery, BookDetailsQueryRequest>()
                .ForMember(x => x.LibraryId, opt => opt.MapFrom(src => src.LibraryId));

            CreateMap<GetBooksDetailsByFilterQuery, BookDetailsFilteredQueryRequest>()
                .ForMember(x => x.LibraryId, opt => opt.MapFrom(src => src.LibraryId))
                .ForMember(x => x.Title, opt => opt.MapFrom(src =>  src.Title))
                .ForMember(x => x.Author, opt => opt.MapFrom(src => src.Author))
                .ForMember(x => x.ISBN, opt => opt.MapFrom(src => src.ISBN))
                .ForMember(x => x.Year, opt => opt.MapFrom(src => src.Year))
                .ForMember(x => x.Available, opt => opt.MapFrom(src => src.Available));

            CreateMap<BookDetailsQueryResult, BookDetailsViewModel>()
                .ForMember(x => x.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(x => x.Title, opt => opt.MapFrom(src => src.Title))
                .ForMember(x => x.Author, opt => opt.MapFrom(src => src.Author))
                .ForMember(x => x.ISBN, opt => opt.MapFrom(src => src.ISBN))
                .ForMember(x => x.Year, opt => opt.MapFrom(src => src.Year))
                .ForMember(x => x.InventoryId, opt => opt.MapFrom(src => src.InventoryId))
                .ForMember(x => x.Available, opt => opt.MapFrom(src => src.Available));

            // Inventories
            CreateMap<AddInventoryCommand, Inventory>()
                .ForMember(x => x.LibraryId, opt => opt.MapFrom(src => src.LibraryId))
                .ForMember(x => x.BookId, opt => opt.MapFrom(src => src.BookId));

            // Loans
            CreateMap<AddLoanCommand, Loan>()
                .ForMember(x => x.InventoryId, opt => opt.MapFrom(src => src.InventoryId))
                .ForMember(x => x.UserId, opt => opt.MapFrom(src => src.UserId))
                .ForMember(x => x.LoanDate, opt => opt.MapFrom(src => src.LoanDate))
                .ForMember(x => x.LoanValidity, opt => opt.MapFrom(src => src.LoanValidity));

            CreateMap<GetLoansDetailsByLibraryQuery, LoanDetailsQueryRequest>()
                .ForMember(x => x.LibraryId, opt => opt.MapFrom(src => src.LibraryId));

            CreateMap<GetLoansDetailsFilteredQuery, LoanDetailsFilteredQueryRequest>()
                .ForMember(x => x.LibraryId, opt => opt.MapFrom(src => src.LibraryId))
                .ForMember(x => x.LoanDate, opt => opt.MapFrom(src => src.LoanDate))
                .ForMember(x => x.LoanValidity, opt => opt.MapFrom(src => src.LoanValidity))
                .ForMember(x => x.Status, opt => opt.MapFrom(src => src.Status))
                .ForMember(x => x.BookName, opt => opt.MapFrom(src => src.BookName))
                .ForMember(x => x.UserName, opt => opt.MapFrom(src => src.UserName));

            CreateMap<LoanDetailsQueryResult, LoanDetailsViewModel>()
                .ForMember(x => x.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(x => x.InventoryId, opt => opt.MapFrom(src => src.InventoryId))
                .ForMember(x => x.UserId, opt => opt.MapFrom(src => src.UserId))
                .ForMember(x => x.LoanDate, opt => opt.MapFrom(src => src.LoanDate))
                .ForMember(x => x.LoanValidity, opt => opt.MapFrom(src => src.LoanValidity))
                .ForMember(x => x.Returned, opt => opt.MapFrom(src => src.Returned))
                .ForMember(x => x.Username, opt => opt.MapFrom(src => src.Username))
                .ForMember(x => x.BookName, opt => opt.MapFrom(src => src.BookName));

        }
    }
}
