using Desafios.GerenciadorBiblioteca.Data.Context.Configurators;
using Desafios.GerenciadorBiblioteca.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Desafios.GerenciadorBiblioteca.Data.Context
{
    public class LibraryDbContext(DbContextOptions<LibraryDbContext> options) : DbContext(options)
    {
        public DbSet<Library> Libraries { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Inventory> Inventories { get; set; }
        public DbSet<Loan> Loans { get; set; }
        public DbSet<VerificationCode> VerificationCodes { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Aplicar configurações
            builder.ApplyConfiguration(new LibraryConfigurator());
            builder.ApplyConfiguration(new BookConfigurator());
            builder.ApplyConfiguration(new UserConfigurator());
            builder.ApplyConfiguration(new InventoryConfigurator());
            builder.ApplyConfiguration(new LoanConfigurator());
            builder.ApplyConfiguration(new VerificationCodeConfigurator());
        }
    }
}
