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

        public virtual DbSet<AspNetRoleClaims> AspNetRoleClaims { get; set; }
        public virtual DbSet<AspNetRoles> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaims> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogins> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUserRoles> AspNetUserRoles { get; set; }
        public virtual DbSet<AspNetUserTokens> AspNetUserTokens { get; set; }
        public virtual DbSet<AspNetUsers> AspNetUsers { get; set; }
        public virtual DbSet<DictionaryCity> DictionaryCity { get; set; }
        public virtual DbSet<DictionaryCountry> DictionaryCountry { get; set; }
        public virtual DbSet<DictionaryCounty> DictionaryCounty { get; set; }
        public virtual DbSet<DictionaryCurrencyType> DictionaryCurrencyType { get; set; }
        public virtual DbSet<DictionaryLandmarkPeriod> DictionaryLandmarkPeriod { get; set; }
        public virtual DbSet<DictionaryLandmarkType> DictionaryLandmarkType { get; set; }
        public virtual DbSet<DictionaryTicketType> DictionaryTicketType { get; set; }
        public virtual DbSet<Landmark> Landmark { get; set; }
        public virtual DbSet<LandmarkPicture> LandmarkPicture { get; set; }
        public virtual DbSet<LandmarkReview> LandmarkReview { get; set; }
        public virtual DbSet<TicketXlandmark> TicketXlandmark { get; set; }

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

            modelBuilder.Entity<AspNetRoleClaims>(entity =>
            {
                entity.HasIndex(e => e.RoleId);

                entity.Property(e => e.RoleId).IsRequired();

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetRoleClaims)
                    .HasForeignKey(d => d.RoleId);
            });

            modelBuilder.Entity<AspNetRoles>(entity =>
            {
                entity.HasIndex(e => e.NormalizedName)
                    .HasName("RoleNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedName] IS NOT NULL)");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Name).HasMaxLength(256);

                entity.Property(e => e.NormalizedName).HasMaxLength(256);
            });

            modelBuilder.Entity<AspNetUserClaims>(entity =>
            {
                entity.HasIndex(e => e.UserId);

                entity.Property(e => e.UserId).IsRequired();

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserClaims)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserLogins>(entity =>
            {
                entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });

                entity.HasIndex(e => e.UserId);

                entity.Property(e => e.LoginProvider).HasMaxLength(128);

                entity.Property(e => e.ProviderKey).HasMaxLength(128);

                entity.Property(e => e.UserId).IsRequired();

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserLogins)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserRoles>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.RoleId });

                entity.HasIndex(e => e.RoleId);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetUserRoles)
                    .HasForeignKey(d => d.RoleId);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserRoles)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserTokens>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });

                entity.Property(e => e.LoginProvider).HasMaxLength(128);

                entity.Property(e => e.Name).HasMaxLength(128);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserTokens)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUsers>(entity =>
            {
                entity.HasIndex(e => e.NormalizedEmail)
                    .HasName("EmailIndex");

                entity.HasIndex(e => e.NormalizedUserName)
                    .HasName("UserNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedUserName] IS NOT NULL)");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Email).HasMaxLength(256);

                entity.Property(e => e.NormalizedEmail).HasMaxLength(256);

                entity.Property(e => e.NormalizedUserName).HasMaxLength(256);

                entity.Property(e => e.UserName).HasMaxLength(256);
            });

            modelBuilder.Entity<DictionaryCity>(entity =>
            {
                entity.HasKey(e => e.CityId)
                    .HasName("PK__City__F2D21B76ECC7E189");

                entity.HasIndex(e => e.CityCode)
                    .HasName("UQ__Dictiona__B488218CDC49CB64")
                    .IsUnique();

                entity.HasIndex(e => e.CityName)
                    .HasName("UQ__City__886159E5AF0BA727")
                    .IsUnique();

                entity.Property(e => e.CityCode)
                    .IsRequired()
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.CityName)
                    .IsRequired()
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.HasOne(d => d.County)
                    .WithMany(p => p.DictionaryCity)
                    .HasForeignKey(d => d.CountyId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__City__CountyId__571DF1D5");
            });

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

            modelBuilder.Entity<DictionaryCounty>(entity =>
            {
                entity.HasKey(e => e.CountyId)
                    .HasName("PK__County__B68F9D97709F2225");

                entity.HasIndex(e => e.CountyCode)
                    .HasName("UQ__Dictiona__34DC46AD1211764E")
                    .IsUnique();

                entity.HasIndex(e => e.CountyName)
                    .HasName("UQ__County__F3C4051041AF0D0A")
                    .IsUnique();

                entity.Property(e => e.CountyCode)
                    .IsRequired()
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.CountyName)
                    .IsRequired()
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.DictionaryCounty)
                    .HasForeignKey(d => d.CountryId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__County__CountryI__534D60F1");
            });

            modelBuilder.Entity<DictionaryCurrencyType>(entity =>
            {
                entity.HasKey(e => e.CurrencyTypeId)
                    .HasName("PK__Currency__1DD4BB3E652E44FA");

                entity.HasIndex(e => e.CurrencyCode)
                    .HasName("UQ__Currency__408426BF5425558A")
                    .IsUnique();

                entity.HasIndex(e => e.CurrencyName)
                    .HasName("UQ__Currency__3D13D29843446505")
                    .IsUnique();

                entity.Property(e => e.CurrencyCode)
                    .IsRequired()
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.CurrencyExRate).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.CurrencyName)
                    .IsRequired()
                    .HasMaxLength(256)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<DictionaryLandmarkPeriod>(entity =>
            {
                entity.HasKey(e => e.LandmarkPeriodId)
                    .HasName("PK__Landmark__80F73FED07CDCF1A");

                entity.Property(e => e.LandmarkPeriodName)
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

            modelBuilder.Entity<DictionaryTicketType>(entity =>
            {
                entity.HasKey(e => e.TicketTypeId)
                    .HasName("PK__TicketTy__6CD68431A2C5D9D4");

                entity.HasIndex(e => e.TicketTypeName)
                    .HasName("UQ__TicketTy__82A19D43AF693E17")
                    .IsUnique();

                entity.Property(e => e.TicketTypeName)
                    .IsRequired()
                    .HasMaxLength(256)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Landmark>(entity =>
            {
                entity.HasIndex(e => e.LandmarkCode)
                    .HasName("UQ__Landmark__85B833E8B5EE4D5E")
                    .IsUnique();

                entity.Property(e => e.LandmarkCode)
                    .IsRequired()
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.LandmarkDescription)
                    .HasMaxLength(5096)
                    .IsUnicode(false);

                entity.Property(e => e.LandmarkName)
                    .IsRequired()
                    .HasMaxLength(524)
                    .IsUnicode(false);

                entity.Property(e => e.Latitude)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.Longitude)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.HasOne(d => d.City)
                    .WithMany(p => p.Landmark)
                    .HasForeignKey(d => d.CityId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__Landmark__CityId__2180FB33");

                entity.HasOne(d => d.LandmarkPeriod)
                    .WithMany(p => p.Landmark)
                    .HasForeignKey(d => d.LandmarkPeriodId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__Landmark__Landma__778AC167");

                entity.HasOne(d => d.LandmarkType)
                    .WithMany(p => p.Landmark)
                    .HasForeignKey(d => d.LandmarkTypeId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__Landmark__Landma__76969D2E");
            });

            modelBuilder.Entity<LandmarkPicture>(entity =>
            {
                entity.HasKey(e => e.PictureId)
                    .HasName("PK__Landmark__8C2866D8C94CFA68");

                entity.Property(e => e.PictureName)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.HasOne(d => d.Landmark)
                    .WithMany(p => p.LandmarkPicture)
                    .HasForeignKey(d => d.LandmarkId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__LandmarkP__Landm__02084FDA");
            });

            modelBuilder.Entity<LandmarkReview>(entity =>
            {
                entity.Property(e => e.ReviewComment)
                    .HasMaxLength(5000)
                    .IsUnicode(false);

                entity.Property(e => e.ReviewTitle)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.UserId).HasMaxLength(900);

                entity.HasOne(d => d.Landmark)
                    .WithMany(p => p.LandmarkReview)
                    .HasForeignKey(d => d.LandmarkId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__LandmarkR__Landm__7B5B524B");
            });

            modelBuilder.Entity<TicketXlandmark>(entity =>
            {
                entity.ToTable("TicketXLandmark");

                entity.Property(e => e.TicketXlandmarkId).HasColumnName("TicketXLandmarkId");

                entity.Property(e => e.TicketValue).HasColumnType("decimal(18, 0)");

                entity.HasOne(d => d.CurrencyType)
                    .WithMany(p => p.TicketXlandmark)
                    .HasForeignKey(d => d.CurrencyTypeId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__TicketXLa__Curre__06CD04F7");

                entity.HasOne(d => d.Landmark)
                    .WithMany(p => p.TicketXlandmark)
                    .HasForeignKey(d => d.LandmarkId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__TicketXLa__Landm__04E4BC85");

                entity.HasOne(d => d.TicketType)
                    .WithMany(p => p.TicketXlandmark)
                    .HasForeignKey(d => d.TicketTypeId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__TicketXLa__Ticke__05D8E0BE");
            });
        }
    }
}
