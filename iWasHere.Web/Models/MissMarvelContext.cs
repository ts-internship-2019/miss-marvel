using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace iWasHere.Web.Models
{
    public partial class MissMarvelContext : DbContext
    {
        public MissMarvelContext()
        {
        }

        public MissMarvelContext(DbContextOptions<MissMarvelContext> options)
            : base(options)
        {
        }

        //public virtual DbSet<DictionaryCounty> DictionaryCounty { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=ts-internship-2019.database.windows.net;Initial Catalog=MissMarvel;Persist Security Info=False;User ID=sa_admin;Password=A123456a;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            //modelBuilder.Entity<DictionaryCounty>(entity =>
            //{
            //    entity.HasKey(e => e.CountyId)
            //        .HasName("PK__County__B68F9D97709F2225");

            //    entity.HasIndex(e => e.CountyCode)
            //        .HasName("UQ__Dictiona__34DC46AD1211764E")
            //        .IsUnique();

            //    entity.HasIndex(e => e.CountyName)
            //        .HasName("UQ__County__F3C4051041AF0D0A")
            //        .IsUnique();

            //    entity.Property(e => e.CountyCode)
            //        .IsRequired()
            //        .HasMaxLength(256)
            //        .IsUnicode(false);

            //    entity.Property(e => e.CountyName)
            //        .IsRequired()
            //        .HasMaxLength(256)
            //        .IsUnicode(false);
            //});
        }
    }
}
