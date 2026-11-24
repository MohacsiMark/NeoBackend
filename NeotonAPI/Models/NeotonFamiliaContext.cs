using Microsoft.EntityFrameworkCore;

namespace NeotonFamiliaAPI.Models;

public class NeotonFamiliaContext : DbContext
{
    public NeotonFamiliaContext(DbContextOptions<NeotonFamiliaContext> options) : base(options)
    {
    }

    public DbSet<Koncert> Koncertek { get; set; }
    public DbSet<Album> Albumok { get; set; }
    public DbSet<Megtekintes> Megtekintesek { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Koncert>(entity =>
        {
            entity.ToTable("koncertek");
            entity.HasKey(x => x.Id);
            entity.Property(x => x.Id).HasColumnName("id");
            entity.Property(x => x.Datum).HasColumnName("datum").HasColumnType("date");
            entity.Property(x => x.Helyszin).HasColumnName("helyszin").HasMaxLength(150);
        });

        modelBuilder.Entity<Album>(entity =>
        {
            entity.ToTable("albumok");
            entity.HasKey(x => x.Id);
            entity.Property(x => x.Id).HasColumnName("id");
            entity.Property(x => x.Cim).HasColumnName("cim").HasMaxLength(150);
            entity.Property(x => x.Ev).HasColumnName("ev");
            entity.HasOne(x => x.Megtekintes)
                  .WithOne(x => x.Album)
                  .HasForeignKey<Megtekintes>(x => x.AlbumId)
                  .HasConstraintName("fk_megtekintesek_albumok")
                  .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<Megtekintes>(entity =>
        {
            entity.ToTable("megtekintesek");
            entity.HasKey(x => x.Id);
            entity.Property(x => x.Id).HasColumnName("id");
            entity.Property(x => x.AlbumId).HasColumnName("album_id");
            entity.Property(x => x.Darab).HasColumnName("darab");
            entity.HasIndex(x => x.AlbumId).IsUnique();
        });
    }
}
