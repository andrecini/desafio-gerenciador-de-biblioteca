using Desafios.GerenciadorBiblioteca.Domain.Infra.Entities;
using Desafios.GerenciadorBiblioteca.Infra.Context.Configurators;
using Microsoft.EntityFrameworkCore;

namespace Desafios.GerenciadorBiblioteca.Infra.Context
{
    public class LibraryDbContext(DbContextOptions<LibraryDbContext> options) : DbContext(options)
    {
        public DbSet<Library> Libraries { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Inventory> Inventories { get; set; }
        public DbSet<Loan> Loans { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Aplicar configurações
            builder.ApplyConfiguration(new InventoryConfigurator());
            builder.ApplyConfiguration(new LoanConfigurator());
        }
    }
}
