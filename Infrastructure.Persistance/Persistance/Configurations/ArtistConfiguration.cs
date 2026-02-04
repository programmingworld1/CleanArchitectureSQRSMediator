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
                .IsRowVersion() // For each update/insert it automatically creates a new token in the db in the column

                // used for : Last Write Wins (lost update / / lost race / write-write conflict), Inconsistent State Prevention
                // EF core uses the field for optimisci concurreny control, meaning an exception will be thrown when there is concurrency (with pessimistic the second user cant even retrieve the record because of a hard lock)
                // the issue with this, it's either all or nothing, if inconsisten state prevention is not an issue but last write wins is.. in that case IsConcurrencyToken can be used for each property you want the check to happen
                .IsConcurrencyToken(); 
        }
    }
}
