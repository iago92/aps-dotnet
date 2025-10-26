using Microsoft.EntityFrameworkCore;
using University.Domain.Entities;

namespace University.Infrastructure.Data
{
    public class UniversityDbContext : DbContext
    {
        public UniversityDbContext(DbContextOptions<UniversityDbContext> options) : base(options)
        {
        }

    public DbSet<University.Domain.Entities.Aluno> Alunos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<University.Domain.Entities.Aluno>(b =>
            {
                b.HasKey(x => x.Id);
                b.Property("PrimeiroNome").IsRequired().HasMaxLength(100);
                b.Property("Sobrenome").IsRequired().HasMaxLength(100);
                b.Property("Email").IsRequired().HasMaxLength(200);
                b.Property("Idade").IsRequired();
                b.Property("CriadoEm").IsRequired();
                b.ToTable("Alunos");
            });
        }
    }
}
