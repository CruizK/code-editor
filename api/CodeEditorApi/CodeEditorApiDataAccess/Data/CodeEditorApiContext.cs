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
        public virtual DbSet<UserRegisteredCourse> UserRegisteredCourses { get; set; }
        public virtual DbSet<UserTutorial> UserTutorials { get; set; }

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
                entity.ToTable("cfgProgrammingLanguage");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Language)
                    .IsRequired()
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
                    .IsRequired()
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

                entity.Property(e => e.Description).HasMaxLength(255);

                entity.Property(e => e.ModifyDate)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsFixedLength(true);

                entity.Property(e => e.Prompt).HasColumnType("text");

                entity.Property(e => e.Title)
                    .IsRequired()
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
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Tutorial_CourseId");

                entity.HasOne(d => d.Difficulty)
                    .WithMany(p => p.Tutorials)
                    .HasForeignKey(d => d.DifficultyId)
                    .HasConstraintName("FK_Tutorial_cfgDifficultyLevel");

                entity.HasOne(d => d.Language)
                    .WithMany(p => p.Tutorials)
                    .HasForeignKey(d => d.LanguageId)
                    .HasConstraintName("FK_Tutorial_cfgProgrammingLanguage");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");

                entity.HasIndex(e => e.Email, "AK_Email")
                    .IsUnique();

                entity.Property(e => e.AccessToken)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Hash)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_User_cfgRoles");
            });

            modelBuilder.Entity<UserRegisteredCourse>(entity =>
            {
                entity.HasKey(e => new { e.CourseId, e.UserId });

                entity.ToTable("UserRegisteredCourse");

                entity.HasOne(d => d.Course)
                    .WithMany(p => p.UserRegisteredCourses)
                    .HasForeignKey(d => d.CourseId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserRegisteredCourse_Course");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserRegisteredCourses)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserRegisteredCourse_User");
            });

            modelBuilder.Entity<UserTutorial>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.TutorialId })
                    .HasName("PK__UserTuto__22630A28E0EDDDD5");

                entity.ToTable("UserTutorial");

                entity.HasOne(d => d.Tutorial)
                    .WithMany(p => p.UserTutorials)
                    .HasForeignKey(d => d.TutorialId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserTutorial_Tutorial");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserTutorials)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserTutorial_User");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
