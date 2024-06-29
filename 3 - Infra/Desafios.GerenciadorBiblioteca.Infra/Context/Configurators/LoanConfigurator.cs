using Desafios.GerenciadorBiblioteca.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Desafios.GerenciadorBiblioteca.Infra.Context.Configurators
{
    public class LoanConfigurator : IEntityTypeConfiguration<Loan>
    {
        public void Configure(EntityTypeBuilder<Loan> builder)
        {
            // Primary Key
            builder.HasKey(l => l.Id);

            // Library Relationship
            builder.HasOne(l => l.Library)
                   .WithMany()
                   .HasForeignKey(l => l.LibraryId)
                   .OnDelete(DeleteBehavior.Cascade);

            // User Relationship
            builder.HasOne(l => l.User)
                   .WithMany()
                   .HasForeignKey(l => l.UserId)
                   .OnDelete(DeleteBehavior.Cascade);

            // Book Relationship
            builder.HasOne(l => l.Book)
                   .WithMany()
                   .HasForeignKey(l => l.BookId)
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
