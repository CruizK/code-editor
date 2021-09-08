using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace CodeEditorApiDataAccess.Data
{
    public partial class CodeEditorApiContext : DbContext
    {
        public CodeEditorApiContext()
        {
        }

        public CodeEditorApiContext(DbContextOptions<CodeEditorApiContext> options)
            : base(options)
        {
        }

        public virtual DbSet<CfgDifficultyLevel> CfgDifficultyLevels { get; set; }
        public virtual DbSet<CfgProgrammingLanguage> CfgProgrammingLanguages { get; set; }
        public virtual DbSet<CfgRole> CfgRoles { get; set; }
        public virtual DbSet<Course> Courses { get; set; }
        public virtual DbSet<Tutorial> Tutorials { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Name=ConnectionStrings:DefaultConnection");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<CfgDifficultyLevel>(entity =>
            {
                entity.ToTable("cfgDifficultyLevel");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Difficulty)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<CfgProgrammingLanguage>(entity =>
            {
                entity.HasKey(e => e.Language)
                    .HasName("PK__tmp_ms_x__C3D59251E247CEAC");

                entity.ToTable("cfgProgrammingLanguages");

                entity.Property(e => e.Language)
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<CfgRole>(entity =>
            {
                entity.ToTable("cfgRoles");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Role)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsFixedLength(true);
            });

            modelBuilder.Entity<Course>(entity =>
            {
                entity.ToTable("Course");

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.Description)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.ModifyDate).HasColumnType("datetime");

                entity.Property(e => e.Title)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.AuthorNavigation)
                    .WithMany(p => p.Courses)
                    .HasForeignKey(d => d.Author)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Course_UserId");
            });

            modelBuilder.Entity<Tutorial>(entity =>
            {
                entity.ToTable("Tutorial");

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.ModifyDate)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsFixedLength(true);

                entity.Property(e => e.Prompt).HasColumnType("text");

                entity.Property(e => e.Title)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.AuthorNavigation)
                    .WithMany(p => p.Tutorials)
                    .HasForeignKey(d => d.Author)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Tutorial_UserId");

                entity.HasOne(d => d.Course)
                    .WithMany(p => p.Tutorials)
                    .HasForeignKey(d => d.CourseId)
                    .HasConstraintName("FK_Tutorial_CourseId");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_User_cfgRoles");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
