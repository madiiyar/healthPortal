using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace healthPortal.Models;

public partial class HealthPortalContext : DbContext
{
    public HealthPortalContext()
    {
    }

    public HealthPortalContext(DbContextOptions<HealthPortalContext> options)
        : base(options)
    {
    }

    public virtual DbSet<FitnessTip> FitnessTips { get; set; }

    public virtual DbSet<Ingredient> Ingredients { get; set; }

    public virtual DbSet<Instruction> Instructions { get; set; }

    public virtual DbSet<Nutrition> Nutritions { get; set; }

    public virtual DbSet<Recipe> Recipes { get; set; }

    public virtual DbSet<RecipeTag> RecipeTags { get; set; }

    public virtual DbSet<Tag> Tags { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=apishkalau-sql-server.database.windows.net;Initial Catalog=healthPortal;Persist Security Info=True;User ID=sqladmin;Password=Madiyar777.;Encrypt=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<FitnessTip>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__FitnessT__3214EC0735D2BC9A");

            entity.Property(e => e.Date)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.FitnessLevel).HasMaxLength(50);
            entity.Property(e => e.Title).HasMaxLength(255);
            entity.Property(e => e.Views).HasDefaultValue(0);

            entity.HasMany(d => d.Tags).WithMany(p => p.FitnessTips)
                .UsingEntity<Dictionary<string, object>>(
                    "FitnessTipTag",
                    r => r.HasOne<Tag>().WithMany()
                        .HasForeignKey("TagId")
                        .HasConstraintName("FK__FitnessTi__TagId__6477ECF3"),
                    l => l.HasOne<FitnessTip>().WithMany()
                        .HasForeignKey("FitnessTipId")
                        .HasConstraintName("FK__FitnessTi__Fitne__6383C8BA"),
                    j =>
                    {
                        j.HasKey("FitnessTipId", "TagId").HasName("PK__FitnessT__0324587522B0A778");
                        j.ToTable("FitnessTipTags");
                    });
        });

        modelBuilder.Entity<Ingredient>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Ingredie__3214EC077F68CF6F");

            entity.Property(e => e.Ingredient1)
                .HasMaxLength(255)
                .HasColumnName("Ingredient");

            entity.HasOne(d => d.Recipe).WithMany(p => p.Ingredients)
                .HasForeignKey(d => d.RecipeId)
                .HasConstraintName("FK__Ingredien__Recip__6FE99F9F");
        });

        modelBuilder.Entity<Instruction>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Instruct__3214EC07CC38F44A");

            entity.Property(e => e.Instruction1).HasColumnName("Instruction");

            entity.HasOne(d => d.Recipe).WithMany(p => p.Instructions)
                .HasForeignKey(d => d.RecipeId)
                .HasConstraintName("FK__Instructi__Recip__72C60C4A");
        });

        modelBuilder.Entity<Nutrition>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Nutritio__3214EC079D0FCF62");

            entity.ToTable("Nutrition");

            entity.Property(e => e.Carbs).HasColumnType("decimal(5, 2)");
            entity.Property(e => e.Fat).HasColumnType("decimal(5, 2)");
            entity.Property(e => e.Protein).HasColumnType("decimal(5, 2)");

            entity.HasOne(d => d.Recipe).WithMany(p => p.Nutritions)
                .HasForeignKey(d => d.RecipeId)
                .HasConstraintName("FK__Nutrition__Recip__75A278F5");
        });

        modelBuilder.Entity<Recipe>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Recipes__3214EC07D1B852CF");

            entity.Property(e => e.Category).HasMaxLength(50);
            entity.Property(e => e.PrepTime).HasMaxLength(50);
            entity.Property(e => e.Title).HasMaxLength(255);

            entity.HasMany(d => d.Tags).WithMany(p => p.Recipes)
                .UsingEntity<Dictionary<string, object>>(
                    "RecipeTagMapping",
                    r => r.HasOne<RecipeTag>().WithMany()
                        .HasForeignKey("TagId")
                        .HasConstraintName("FK__RecipeTag__TagId__6D0D32F4"),
                    l => l.HasOne<Recipe>().WithMany()
                        .HasForeignKey("RecipeId")
                        .HasConstraintName("FK__RecipeTag__Recip__6C190EBB"),
                    j =>
                    {
                        j.HasKey("RecipeId", "TagId").HasName("PK__RecipeTa__2B8E472A6F8AFF70");
                        j.ToTable("RecipeTagMapping");
                    });
        });

        modelBuilder.Entity<RecipeTag>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__RecipeTa__3214EC07485CE87D");

            entity.HasIndex(e => e.Name, "UQ__RecipeTa__737584F6B1B1FFE0").IsUnique();

            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<Tag>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Tags__3214EC07F8806D9F");

            entity.HasIndex(e => e.Name, "UQ__Tags__737584F6879E3026").IsUnique();

            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Users__3214EC07976DE160");

            entity.HasIndex(e => e.Username, "UQ__Users__536C85E4213014A3").IsUnique();

            entity.HasIndex(e => e.Email, "UQ__Users__A9D1053458488B47").IsUnique();

            entity.Property(e => e.Email).HasMaxLength(255);
            entity.Property(e => e.Password).HasMaxLength(255);
            entity.Property(e => e.Username).HasMaxLength(100);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
