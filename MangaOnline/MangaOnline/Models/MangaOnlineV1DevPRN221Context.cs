using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace MangaOnline.Models
{
    public partial class MangaOnlineV1DevPRN221Context : DbContext
    {
        public MangaOnlineV1DevPRN221Context()
        {
        }

        public MangaOnlineV1DevPRN221Context(DbContextOptions<MangaOnlineV1DevPRN221Context> options)
            : base(options)
        {
        }

        public virtual DbSet<Author> Authors { get; set; } = null!;
        public virtual DbSet<Category> Categories { get; set; } = null!;
        public virtual DbSet<CategoryManga> CategoryMangas { get; set; } = null!;
        public virtual DbSet<Chaptere> Chapteres { get; set; } = null!;
        public virtual DbSet<Comment> Comments { get; set; } = null!;
        public virtual DbSet<FollowList> FollowLists { get; set; } = null!;
        public virtual DbSet<History> Histories { get; set; } = null!;
        public virtual DbSet<IpUserVote> IpUserVotes { get; set; } = null!;
        public virtual DbSet<Manga> Mangas { get; set; } = null!;
        public virtual DbSet<Notification> Notifications { get; set; } = null!;
        public virtual DbSet<Page> Pages { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;
        public virtual DbSet<RoleClaim> RoleClaims { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<UserRole> UserRoles { get; set; } = null!;
        public virtual DbSet<UserToken> UserTokens { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
                optionsBuilder.UseSqlServer(config.GetConnectionString("ConStr"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Author>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.SubId).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.SubId).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<CategoryManga>(entity =>
            {
                entity.HasKey(e => e.SubId)
                    .HasName("PK__Category__4D9BB84AE3574281");

                entity.ToTable("CategoryManga");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.CategoryMangas)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CategoryManga_Categories");

                entity.HasOne(d => d.Manga)
                    .WithMany(p => p.CategoryMangas)
                    .HasForeignKey(d => d.MangaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CategoryManga_Mangas");
            });

            modelBuilder.Entity<Chaptere>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.FilePdf).HasColumnName("FilePDF");

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.SubId).ValueGeneratedOnAdd();

                entity.HasOne(d => d.Manga)
                    .WithMany(p => p.Chapteres)
                    .HasForeignKey(d => d.MangaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Chapteres_Mangas");
            });

            modelBuilder.Entity<Comment>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.HasOne(d => d.Manga)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(d => d.MangaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Comments_Mangas");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Comments_Users");
            });

            modelBuilder.Entity<FollowList>(entity =>
            {
                entity.ToTable("FollowList");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.HasOne(d => d.Manga)
                    .WithMany(p => p.FollowLists)
                    .HasForeignKey(d => d.MangaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FollowList_Mangas");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.FollowLists)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FollowList_Users");
            });

            modelBuilder.Entity<History>(entity =>
            {
                entity.ToTable("History");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Date)
                    .HasColumnType("datetime")
                    .HasColumnName("date");

                entity.Property(e => e.From)
                    .HasMaxLength(256)
                    .HasColumnName("from");

                entity.Property(e => e.Hash)
                    .HasMaxLength(256)
                    .HasColumnName("hash");

                entity.Property(e => e.To)
                    .HasMaxLength(256)
                    .HasColumnName("to");

                entity.Property(e => e.User)
                    .HasMaxLength(256)
                    .HasColumnName("user");

                entity.Property(e => e.Value)
                    .HasMaxLength(256)
                    .HasColumnName("value");
            });

            modelBuilder.Entity<IpUserVote>(entity =>
            {
                entity.HasKey(e => e.MangaId)
                    .HasName("pk_my_table");

                entity.ToTable("IpUserVote");

                entity.Property(e => e.MangaId).ValueGeneratedNever();
            });

            modelBuilder.Entity<Manga>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.SubId).ValueGeneratedOnAdd();

                entity.HasOne(d => d.Author)
                    .WithMany(p => p.Mangas)
                    .HasForeignKey(d => d.AuthorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Mangas_Authors");
            });

            modelBuilder.Entity<Notification>(entity =>
            {
                entity.ToTable("Notification");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.HasOne(d => d.Chapter)
                    .WithMany(p => p.Notifications)
                    .HasForeignKey(d => d.ChapterId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Notification_Chapteres");
            });

            modelBuilder.Entity<Page>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.HasOne(d => d.Chapter)
                    .WithMany(p => p.Pages)
                    .HasForeignKey(d => d.ChapterId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Pages_Chapteres");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Name).HasMaxLength(256);

                entity.Property(e => e.SubId).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<RoleClaim>(entity =>
            {
                entity.HasKey(e => e.RoleId);

                entity.Property(e => e.RoleId).ValueGeneratedNever();

                entity.HasOne(d => d.Role)
                    .WithOne(p => p.RoleClaim)
                    .HasForeignKey<RoleClaim>(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RoleClaims_Roles");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Email).HasMaxLength(256);

                entity.Property(e => e.SubId).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<UserRole>(entity =>
            {
                entity.HasKey(e => e.UserId);

                entity.Property(e => e.UserId).ValueGeneratedNever();

                entity.Property(e => e.SubId).ValueGeneratedOnAdd();

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.UserRoles)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserRoles_Roles");

                entity.HasOne(d => d.User)
                    .WithOne(p => p.UserRole)
                    .HasForeignKey<UserRole>(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserRoles_Users");
            });

            modelBuilder.Entity<UserToken>(entity =>
            {
                entity.HasKey(e => e.UserId);

                entity.Property(e => e.UserId).ValueGeneratedNever();

                entity.Property(e => e.Email).HasMaxLength(256);

                entity.HasOne(d => d.User)
                    .WithOne(p => p.UserToken)
                    .HasForeignKey<UserToken>(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserTokens_Users");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
