using Domain.Entities;
using Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistance.Configurations
{
    public class SongConfiguration : IEntityTypeConfiguration<Song>
    {
        public void Configure(EntityTypeBuilder<Song> builder)
        {
            builder.ToTable("Songs")
                .HasKey(b => b.Id);

            builder.Property(s => s.Bpm) // ← Property naam = "Bpm"
                .HasConversion(
                    bpm => bpm.Value, // from code to db (where does the value come from that we store in the db: from s.Bpm.Value)
                    value => new Bpm(value)) // from db to the code (where does the value go to when we get the value from the db: to s.Bpm)
                .IsRequired();

            builder.Property(s => s.Duration)
                .HasConversion(
                    duration => duration.TotalSeconds,
                    seconds => new Duration(seconds))
                .IsRequired();

            builder.Property(s => s.Year)
                .HasConversion(
                    year => year.Value,
                    value => new Year(value))
                .IsRequired();

            builder
                .HasOne(s => s.Artist)
                .WithMany(a => a.Songs);
        }
    }
}
