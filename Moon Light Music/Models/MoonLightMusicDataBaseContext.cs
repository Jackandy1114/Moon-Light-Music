using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Moon_Light_Music.Models
{
    public partial class MoonLightMusicDataBaseContext : DbContext
    {
        public MoonLightMusicDataBaseContext()
        {
        }

        public MoonLightMusicDataBaseContext(DbContextOptions<MoonLightMusicDataBaseContext> options)
            : base(options)
        {
        }

        public virtual DbSet<UserAccount> UserAccounts { get; set; } = null!;
        public virtual DbSet<UserProfile> UserProfiles { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=tcp:moonlightmusic.database.windows.net,1433; User ID=jayandy;Password=Iloveuzienoi1114 ;Database=MoonLightMusicDataBase;Trusted_Connection=False;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserAccount>(entity =>
            {
                entity.ToTable("UserAccount");

                entity.HasIndex(e => e.Email, "UQ__UserAcco__AB6E6164850EBCE5")
                    .IsUnique();

                entity.Property(e => e.Devices)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("devices")
                    .HasDefaultValueSql("('TestUser')");

                entity.Property(e => e.Email)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("email")
                    .HasDefaultValueSql("('TestUser@gmail.com')");

                entity.Property(e => e.Password)
                    .HasMaxLength(128)
                    .IsUnicode(false)
                    .HasColumnName("password")
                    .HasDefaultValueSql("('TestUser')");

                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("phoneNumber")
                    .HasDefaultValueSql("('TestUser')");

                entity.Property(e => e.Role)
                    .HasMaxLength(50)
                    .HasColumnName("role")
                    .HasDefaultValueSql("('TestUser')");

                entity.Property(e => e.UserProfile)
                    .HasMaxLength(128)
                    .IsUnicode(false)
                    .HasColumnName("userProfile")
                    .HasDefaultValueSql("('TestUser')");

                entity.HasOne(d => d.UserProfileNavigation)
                    .WithMany(p => p.UserAccounts)
                    .HasForeignKey(d => d.UserProfile)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__UserAccou__userP__1BC821DD");
            });

            modelBuilder.Entity<UserProfile>(entity =>
            {
                entity.ToTable("UserProfile");

                entity.Property(e => e.Id)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.Address)
                    .HasMaxLength(200)
                    .HasColumnName("address")
                    .HasDefaultValueSql("('TestUser')");

                entity.Property(e => e.Avatar)
                    .HasColumnName("avatar")
                    .HasDefaultValueSql("('TestUser')");

                entity.Property(e => e.Birthday)
                    .HasColumnType("date")
                    .HasColumnName("birthday");

                entity.Property(e => e.Cccd)
                    .HasMaxLength(50)
                    .HasColumnName("CCCD")
                    .HasDefaultValueSql("('TestUser')");

                entity.Property(e => e.Country)
                    .HasMaxLength(128)
                    .HasColumnName("country")
                    .HasDefaultValueSql("('TestUser')");

                entity.Property(e => e.Description)
                    .HasMaxLength(100)
                    .HasColumnName("description")
                    .HasDefaultValueSql("('TestUser')");

                entity.Property(e => e.Images)
                    .HasColumnName("images")
                    .HasDefaultValueSql("('TestUser')");

                entity.Property(e => e.Name)
                    .HasMaxLength(200)
                    .HasColumnName("name")
                    .HasDefaultValueSql("('TestUser')");

                entity.Property(e => e.Sex)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("sex")
                    .HasDefaultValueSql("('TestUser')");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
