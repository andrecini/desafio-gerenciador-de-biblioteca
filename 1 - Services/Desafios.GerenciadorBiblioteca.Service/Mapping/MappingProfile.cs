using AutoMapper;
using Desafios.GerenciadorBiblioteca.Domain.Entities;
using Desafios.GerenciadorBiblioteca.Service.DTOs.Requests;
using System.Diagnostics.CodeAnalysis;

namespace Desafios.GerenciadorBiblioteca.Service.Mapping
{
    [ExcludeFromCodeCoverage]
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<LibraryDTO, Library>()
                .ForMember(x => x.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(x => x.CNPJ, opt => opt.MapFrom(src => src.CNPJ))
                .ForMember(x => x.Phone, opt => opt.MapFrom(src => src.Phone));

            CreateMap<UserDTO, User>()
                .ForMember(x => x.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(x => x.Phone, opt => opt.MapFrom(src => src.Phone))
                .ForMember(x => x.Email, opt => opt.MapFrom(src => src.Email));

            CreateMap<BookDTO, Book>()
                .ForMember(x => x.Title, opt => opt.MapFrom(src => src.Title))
                .ForMember(x => x.Author, opt => opt.MapFrom(src => src.Author))
                .ForMember(x => x.ISBN, opt => opt.MapFrom(src => src.ISBN))
                .ForMember(x => x.Year, opt => opt.MapFrom(src => src.Year));

            CreateMap<InventoryDTO, Inventory>()
                .ForMember(x => x.LibraryId, opt => opt.MapFrom(src => src.LibraryId))
                .ForMember(x => x.BookId, opt => opt.MapFrom(src => src.BookId))
                .ForMember(x => x.Available, opt => opt.MapFrom(src => src.Available));

            CreateMap<LoanDTO, Loan>()
                .ForMember(x => x.LibraryId, opt => opt.MapFrom(src => src.LibraryId))
                .ForMember(x => x.UserId, opt => opt.MapFrom(src => src.UserId))
                .ForMember(x => x.BookId, opt => opt.MapFrom(src => src.BookId))
                .ForMember(x => x.LoanDate, opt => opt.MapFrom(src => src.LoanDate))
                .ForMember(x => x.LoanValidity, opt => opt.MapFrom(src => src.LoanValidity));
        }
    }
}
