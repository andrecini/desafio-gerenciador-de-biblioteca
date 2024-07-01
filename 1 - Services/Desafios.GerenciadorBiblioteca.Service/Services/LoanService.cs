﻿using AutoMapper;
using Desafios.GerenciadorBiblioteca.Domain.Entities;
using Desafios.GerenciadorBiblioteca.Domain.Entities.Filters;
using Desafios.GerenciadorBiblioteca.Domain.Enums;
using Desafios.GerenciadorBiblioteca.Domain.Exceptions;
using Desafios.GerenciadorBiblioteca.Domain.UnitOfWork;
using Desafios.GerenciadorBiblioteca.Service.DTOs.Requests;
using Desafios.GerenciadorBiblioteca.Service.Services.Interfaces;
using System.Net;

namespace Desafios.GerenciadorBiblioteca.Service.Services
{
    public class LoanService(IUnitOfWork unitOfWork, IMapper mapper) : ILoanService
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;

        public async Task<IEnumerable<Loan>> GetAllAsync()
        {
            var data = await _unitOfWork.Loans.GetAllAsync();

            return data;
        }

        public async Task<Loan> GetByIdAsync(int id)
        {
            CustomException.ThrowIfLessThan(0, "Id");

            var data = await _unitOfWork.Loans.GetByIdAsync(id);

            return data;
        }

        public async Task<IEnumerable<Loan>> FindAsync(LoanFilter filter)
        {
            var data = await GetAllAsync();

            data = FilterLoans(data, filter);

            return data;
        }

        public async Task<bool> AddAsync(LoanDTO dto)
        {
            CustomException.ThrowIfLessThan(0, "Empréstimo");

            var entity = _mapper.Map<Loan>(dto);

            await _unitOfWork.Loans.AddAsync(entity);
            var result = await _unitOfWork.SaveAsync();

            return result > 0 ? true : throw new CustomException(
                "Não foi possível adicionar o Empréstimo. Tente novamente!",
                HttpStatusCode.InternalServerError);
        }

        public async Task<bool> Update(int id, LoanDTO dto)
        {
            CustomException.ThrowIfLessThan(0, "Empréstimo");

            var loanRegistered = await GetByIdAsync(id) ??
                throw new CustomException("Nenhum Emrpéstimo foi encontrado com essas informações. Tente novamente!", HttpStatusCode.NotFound);

            var entity = _mapper.Map<Loan>(dto);

            _unitOfWork.Loans.Update(entity);
            var result = await _unitOfWork.SaveAsync();

            return result > 0 ? true : throw new CustomException(
                "Não foi possível atualizar o Empréstimo. Tente novamente!",
                HttpStatusCode.InternalServerError);
        }

        public async Task<bool> Remove(int id)
        {
            CustomException.ThrowIfLessThan(0, "Id");

            var loanRegistered = await GetByIdAsync(id) ??
                throw new CustomException("Nenhum Empréstimo foi encontrado com essas informações. Tente novamente!", HttpStatusCode.NotFound);

            _unitOfWork.Loans.Remove(loanRegistered);
            var result = await _unitOfWork.SaveAsync();

            return result > 0 ? true : throw new CustomException(
                "Não foi possível deletar o Empréstimo. Tente novamente!",
                HttpStatusCode.InternalServerError);
        }

        private IEnumerable<Loan> FilterLoans(IEnumerable<Loan> loans, LoanFilter filter)
        {
            if (filter.LibraryId >= 0)
                loans = loans.Where(x => x.LibraryId == filter.LibraryId);
            if (filter.UserId >= 0)
                loans = loans.Where(x => x.UserId == filter.UserId);
            if (filter.BookId >= 0)
                loans = loans.Where(x => x.BookId == filter.BookId);
            if (filter.LoanDate != default)
                loans = loans.Where(x => x.LoanDate == filter.LoanDate);
            if (filter.LoanValidity != default)
                loans = loans.Where(x => x.LoanValidity == filter.LoanValidity);
            if (filter.Status == LoanStatus.Returned)
                loans = loans.Where(x => x.Returned == true);
            if (filter.Status == LoanStatus.Returned)
                loans = loans.Where(x => x.Returned == false);

            return loans;
        }
    }
}
