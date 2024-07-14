using AutoMapper;
using Desafios.GerenciadorBiblioteca.Domain.Entities;
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
            CreateMap<LibraryInputModel, Library>()
                .ForMember(x => x.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(x => x.CNPJ, opt => opt.MapFrom(src => src.CNPJ))
                .ForMember(x => x.Phone, opt => opt.MapFrom(src => src.Phone));

            CreateMap<UserRegisterInputModel, User>()
                .ForMember(x => x.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(x => x.Phone, opt => opt.MapFrom(src => src.Phone))
                .ForMember(x => x.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(x => x.Password, opt => opt.MapFrom(src => src.Password));

            CreateMap<UserUpdateInputModel, User>()
                .ForMember(x => x.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(x => x.Phone, opt => opt.MapFrom(src => src.Phone))
                .ForMember(x => x.Email, opt => opt.MapFrom(src => src.Email));

            CreateMap<User, UserViewModel>()
                .ForMember(x => x.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(x => x.Phone, opt => opt.MapFrom(src => src.Phone))
                .ForMember(x => x.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(x => x.Role, opt => opt.MapFrom(src => src.Role));

            CreateMap<BookInputModel, Book>()
                .ForMember(x => x.Title, opt => opt.MapFrom(src => src.Title))
                .ForMember(x => x.Author, opt => opt.MapFrom(src => src.Author))
                .ForMember(x => x.ISBN, opt => opt.MapFrom(src => src.ISBN))
                .ForMember(x => x.Year, opt => opt.MapFrom(src => src.Year));

            CreateMap<InventoryInputModel, Inventory>()
                .ForMember(x => x.LibraryId, opt => opt.MapFrom(src => src.LibraryId))
                .ForMember(x => x.BookId, opt => opt.MapFrom(src => src.BookId));

            CreateMap<LoanInputModel, Loan>()
                .ForMember(x => x.InventoryId, opt => opt.MapFrom(src => src.InventoryId))
                .ForMember(x => x.UserId, opt => opt.MapFrom(src => src.UserId))
                .ForMember(x => x.LoanDate, opt => opt.MapFrom(src => src.LoanDate))
                .ForMember(x => x.LoanValidity, opt => opt.MapFrom(src => src.LoanValidity));
        }
    }
}
