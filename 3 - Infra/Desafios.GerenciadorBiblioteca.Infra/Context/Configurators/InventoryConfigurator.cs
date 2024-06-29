using Desafios.GerenciadorBiblioteca.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Desafios.GerenciadorBiblioteca.Infra.Context.Configurators
{
    public class InventoryConfigurator : IEntityTypeConfiguration<Inventory>
    {
        public void Configure(EntityTypeBuilder<Inventory> builder)
        {
            builder.HasKey(i => i.Id);

            // Library Relationship
            builder.HasOne(i => i.Library)
                   .WithMany(l => l.Inventories)
                   .HasForeignKey(i => i.LibraryId)
                   .OnDelete(DeleteBehavior.Cascade);

            // Book Relationship
            builder.HasOne(i => i.Book)
                   .WithMany(b => b.Inventories)
                   .HasForeignKey(i => i.BookId)
                   .OnDelete(DeleteBehavior.Cascade);

            // Properties Configure
            builder.Property(i => i.Amount)
                   .IsRequired();

            builder.Property(i => i.Available)
                   .IsRequired();
        }
    }
}
