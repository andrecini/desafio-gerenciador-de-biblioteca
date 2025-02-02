﻿using Desafios.GerenciadorBiblioteca.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Desafios.GerenciadorBiblioteca.Data.Context.Configurators
{
    public class LoanConfigurator : IEntityTypeConfiguration<Loan>
    {
        public void Configure(EntityTypeBuilder<Loan> builder)
        {
            // Primary Key
            builder.HasKey(l => l.Id);
            builder.Property(l => l.Id).ValueGeneratedOnAdd();

            // Library Relationship
            builder.HasOne(l => l.Inventory)
                   .WithMany(l => l.Loans)
                   .HasForeignKey(l => l.InventoryId)
                   .OnDelete(DeleteBehavior.Cascade);

            // User Relationship
            builder.HasOne(l => l.User)
                   .WithMany(u => u.Loans)
                   .HasForeignKey(l => l.UserId)
                   .OnDelete(DeleteBehavior.Cascade);

            // Properties Configure
            builder.Property(l => l.LoanDate)
                   .IsRequired();

            builder.Property(l => l.LoanValidity)
                   .IsRequired();

            builder.Property(l => l.Returned)
                   .IsRequired();
        }
    }
}
