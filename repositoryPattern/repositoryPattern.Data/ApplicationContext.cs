using Microsoft.EntityFrameworkCore;
using repositoryPattern.Data.Configurations;
using repositoryPattern.Entities;

namespace repositoryPattern.Data
{
    public class ApplicationContext : DbContext
    {
        protected override void OnConfiguring(
            DbContextOptionsBuilder optionsBuilder
        )
        {
            optionsBuilder
                .UseSqlServer(@"Server=.\;Database=SchoolDb;Trusted_Connection=True;MultipleActiveResultSets=true");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new StudentConfig());
        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Grade> Grades { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<User> Users { get; set; }

    }
}
