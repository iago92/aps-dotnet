using Microsoft.EntityFrameworkCore;
using University.Domain.Entities;

namespace University.Infrastructure.Data
{
    public class UniversityDbContext : DbContext
    {
        public UniversityDbContext(DbContextOptions<UniversityDbContext> options) : base(options)
        {
        }

        public DbSet<Aluno> Alunos { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuração de Aluno
            modelBuilder.Entity<Aluno>(b =>
            {
                b.HasKey(x => x.Id);
                b.Property("PrimeiroNome").IsRequired().HasMaxLength(100);
                b.Property("Sobrenome").IsRequired().HasMaxLength(100);
                b.Property("Email").IsRequired().HasMaxLength(200);
                b.Property("Idade").IsRequired();
                b.Property("CriadoEm").IsRequired();
                b.ToTable("Alunos");
            });

            // Configuração de Category
            modelBuilder.Entity<Category>(b =>
            {
                b.HasKey(x => x.Id);
                b.Property(x => x.Name).IsRequired().HasMaxLength(150);
                b.Property(x => x.Description).HasMaxLength(500);
                b.Property(x => x.CreatedAt).IsRequired();
                b.Property(x => x.UpdatedAt);
                b.ToTable("Categories");
                
                // Relacionamento 1:N com Product
                b.HasMany(x => x.Products)
                    .WithOne(x => x.Category)
                    .HasForeignKey(x => x.CategoryId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // Configuração de Product
            modelBuilder.Entity<Product>(b =>
            {
                b.HasKey(x => x.Id);
                b.Property(x => x.Name).IsRequired().HasMaxLength(200);
                b.Property(x => x.Description).HasMaxLength(1000);
                b.Property(x => x.Price).IsRequired().HasPrecision(18, 2);
                b.Property(x => x.Stock).IsRequired();
                b.Property(x => x.CategoryId).IsRequired();
                b.Property(x => x.CreatedAt).IsRequired();
                b.Property(x => x.UpdatedAt);
                b.ToTable("Products");

                // Configuração da chave estrangeira explícita
                b.HasOne(x => x.Category)
                    .WithMany(x => x.Products)
                    .HasForeignKey(x => x.CategoryId)
                    .IsRequired()
                    .OnDelete(DeleteBehavior.Cascade);

                // Índices para melhor performance nas buscas
                b.HasIndex(x => x.CategoryId);
                b.HasIndex(x => x.Name);
            });
        }
    }
}
