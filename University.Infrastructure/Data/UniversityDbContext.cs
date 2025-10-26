using Microsoft.EntityFrameworkCore;
using University.Domain.Entities;

namespace University.Infrastructure.Data
{
    public class UniversityDbContext : DbContext
    {
        public UniversityDbContext(DbContextOptions<UniversityDbContext> options) : base(options)
        {
        }

        public DbSet<Student> Students { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Student>(b =>
            {
                b.HasKey(x => x.Id);
                b.Property(x => x.FirstName).IsRequired().HasMaxLength(100);
                b.Property(x => x.LastName).IsRequired().HasMaxLength(100);
                b.Property(x => x.Email).IsRequired().HasMaxLength(200);
                b.Property(x => x.Age).IsRequired();
                b.Property(x => x.CreatedAt).IsRequired();
            });
        }
    }
}
