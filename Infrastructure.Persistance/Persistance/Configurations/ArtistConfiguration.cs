using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistance.Configurations
{
    public class BurgerConfiguration : IEntityTypeConfiguration<Artist>
    {
        public void Configure(EntityTypeBuilder<Artist> builder)
        {
            builder.ToTable("Artist")
                .HasKey(b => b.Id);

            builder
                .HasIndex(a => a.Name)
                .IsUnique();

            builder.Property(a => a.RowVersion)
                .IsRowVersion()
                .IsConcurrencyToken();
        }
    }
}
