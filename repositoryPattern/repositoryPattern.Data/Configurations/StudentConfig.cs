using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using repositoryPattern.Entities;

namespace repositoryPattern.Data.Configurations
{
    public class StudentConfig : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.HasKey(o => o.Id);
            builder.Property(t => t.FirstName).IsRequired().HasMaxLength(200);
            builder.Property(t => t.LastName).IsRequired().HasMaxLength(200);

            builder
                .HasData(new Student {
                    Id = 1,
                    FirstName="Ahmad",
                    LastName="Rahmani"
                });
        }
    }
}
