using Desafios.GerenciadorBiblioteca.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Desafios.GerenciadorBiblioteca.Data.Context.Configurators
{
    internal class LibraryConfigurator : IEntityTypeConfiguration<Library>
    {
        public void Configure(EntityTypeBuilder<Library> builder)
        {
            builder.HasKey(l => l.Id);
            builder.Property(l => l.Id).ValueGeneratedOnAdd();

            builder.Property(l => l.Name)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(l => l.CNPJ)
                   .IsRequired()
                   .HasMaxLength(14);

            builder.Property(l => l.Phone)
                   .IsRequired()
                   .HasMaxLength(11);
        }
    }
}
