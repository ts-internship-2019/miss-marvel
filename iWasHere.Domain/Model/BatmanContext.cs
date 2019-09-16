using System;
using iWasHere.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace iWasHere.Domain.Model
{
    public partial class DatabaseContext : DbContext
    {
        public DatabaseContext()
        {
        }

        public DatabaseContext(DbContextOptions<DatabaseContext> options)
            : base(options)
        {
        }

        public virtual DbSet<DictionaryCountry> DictionaryCountry { get; set; }

        public virtual DbSet<DictionaryLandmarkType> DictionaryLandmarkType { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            modelBuilder.Entity<DictionaryCountry>(entity =>
            {
                entity.HasKey(e => e.CountryId)
                    .HasName("PK__Country__10D1609FA42B66F3");

                entity.HasIndex(e => e.CountryCode)
                    .HasName("UQ__Dictiona__5D9B0D2CFE731AFA")
                    .IsUnique();

                entity.HasIndex(e => e.CountryName)
                    .HasName("UQ__Country__E056F20167A528C4")
                    .IsUnique();

                entity.Property(e => e.CountryCode)
                    .IsRequired()
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.CountryName)
                    .IsRequired()
                    .HasMaxLength(256)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<DictionaryLandmarkType>(entity =>
            {
                entity.HasKey(e => e.DictionaryItemId);

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.DictionaryItemCode)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.DictionaryItemName)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });
        }
    }
}
