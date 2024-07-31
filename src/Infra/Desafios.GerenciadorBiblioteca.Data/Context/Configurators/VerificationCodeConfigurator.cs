using Desafios.GerenciadorBiblioteca.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Desafios.GerenciadorBiblioteca.Data.Context.Configurators
{
    public class VerificationCodeConfigurator : IEntityTypeConfiguration<VerificationCode>
    {
        public void Configure(EntityTypeBuilder<VerificationCode> builder)
        {
            // Primary Key
            builder.HasKey(v => v.Id);
            builder.Property(v => v.Id).ValueGeneratedOnAdd();

            // User Relationship
            builder.HasOne(u => u.User)
                   .WithMany(u => u.VerificationCodes)
                   .HasForeignKey(u => u.UserId)
                   .OnDelete(DeleteBehavior.Cascade);

            // Properties Configure
            builder.Property(u => u.Code)
                   .IsRequired();

            builder.Property(u => u.ValidTo)
                   .IsRequired();
        }
    }
}
