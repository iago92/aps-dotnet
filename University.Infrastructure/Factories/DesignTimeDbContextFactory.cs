using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
namespace University.Infrastructure.Factories
{
    // This factory is used by EF Core tools at design time to create the DbContext for migrations
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<Data.UniversityDbContext>
    {
        public Data.UniversityDbContext CreateDbContext(string[] args)
        {
            // Use a sensible default for design-time. Update connection string as needed.
            var conn = "Server=localhost,1433;Database=UniversityDb;User Id=sa;Password=Your_password123;";

            var optionsBuilder = new DbContextOptionsBuilder<Data.UniversityDbContext>();
            optionsBuilder.UseSqlServer(conn);

            return new Data.UniversityDbContext(optionsBuilder.Options);
        }
    }
}
