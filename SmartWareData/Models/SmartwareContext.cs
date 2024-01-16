using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace SmartWareData.Models;

public partial class SmartwareContext : DbContext
{
    public SmartwareContext()
    {
    }

    public SmartwareContext(DbContextOptions<SmartwareContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Article> Articles { get; set; }

    public virtual DbSet<Movement> Movements { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    { 
        //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
      //        => optionsBuilder.UseMySql("server=127.0.0.1;port=3306;database=smartware;user=smartware;password=smartware", Microsoft.EntityFrameworkCore.ServerVersion.Parse("11.1.0-mariadb"));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("latin1_swedish_ci")
            .HasCharSet("latin1");

        modelBuilder.Entity<Article>(entity =>
        {
            entity.HasKey(e => e.ArticleId).HasName("PRIMARY");

            entity.ToTable("articles");

            entity.Property(e => e.ArticleId).HasColumnType("bigint(20)");
            entity.Property(e => e.ArticleType).HasColumnType("int(11)");
            entity.Property(e => e.Box).HasColumnType("text");
            entity.Property(e => e.Category).HasColumnType("text");
            entity.Property(e => e.Name).HasColumnType("text");
            entity.Property(e => e.Note).HasColumnType("text");
            entity.Property(e => e.PartNumber).HasColumnType("text");
            entity.Property(e => e.Quantity)
                .HasDefaultValueSql("'0'")
                .HasColumnType("int(11)");
            entity.Property(e => e.Room).HasColumnType("text");
            entity.Property(e => e.Shelf).HasColumnType("text");
            entity.Property(e => e.SubCategory1).HasColumnType("text");
            entity.Property(e => e.SubCategory2).HasColumnType("text");
            entity.Property(e => e.SubCategory3).HasColumnType("text");
            entity.Property(e => e.Tag).HasColumnType("int(11)");
        });

        modelBuilder.Entity<Movement>(entity =>
        {
            entity.HasKey(e => e.MovementId).HasName("PRIMARY");

            entity.ToTable("movements");

            entity.HasIndex(e => e.ArticleId, "fk_article");

            entity.Property(e => e.MovementId).HasColumnType("bigint(20)");
            entity.Property(e => e.ArticleId).HasColumnType("bigint(20)");
            entity.Property(e => e.Date).HasColumnType("text");
            entity.Property(e => e.MovementType).HasColumnType("int(11)");
            entity.Property(e => e.Note).HasColumnType("text");
            entity.Property(e => e.ProjectCode).HasColumnType("text");
            entity.Property(e => e.Quantity).HasColumnType("int(11)");
            entity.Property(e => e.Reason).HasColumnType("int(11)");
            entity.Property(e => e.UrlLink).HasColumnType("text");

            entity.HasOne(d => d.Article).WithMany(p => p.Movements)
                .HasForeignKey(d => d.ArticleId)
                .HasConstraintName("fk_article");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
