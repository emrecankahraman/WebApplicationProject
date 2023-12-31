using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace WebApplicationProject.Models;

public partial class WebDatabaseContext : DbContext
{
    public WebDatabaseContext()
    {
    }

    public WebDatabaseContext(DbContextOptions<WebDatabaseContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AdminLogin> AdminLogins { get; set; }

    public virtual DbSet<Attack> Attacks { get; set; }

    public virtual DbSet<ClickedMail> ClickedMails { get; set; }

    public virtual DbSet<FacebookLogin> FacebookLogins { get; set; }

    public virtual DbSet<NetflixLogin> NetflixLogins { get; set; }

    public virtual DbSet<SentEmail> SentEmails { get; set; }

    public virtual DbSet<SpotifyLogin> SpotifyLogins { get; set; }

    public virtual DbSet<Victim> Victims { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=.;Database=WebDatabase;Trusted_Connection=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.UseCollation("Turkish_CI_AI");

        modelBuilder.Entity<AdminLogin>(entity =>
        {
            entity.HasKey(e => e.AdminId).HasName("PK__AdminLog__719FE4E868353DF8");

            entity.ToTable("AdminLogin");

            entity.Property(e => e.AdminId).HasColumnName("AdminID");
            entity.Property(e => e.Password).HasMaxLength(255);
            entity.Property(e => e.Username).HasMaxLength(255);
        });

        modelBuilder.Entity<Attack>(entity =>
        {
            entity.HasKey(e => e.AttackId).HasName("PK__Attack__F55E13FB18F86CE1");

            entity.ToTable("Attack");

            entity.Property(e => e.AttackId).HasColumnName("AttackID");
            entity.Property(e => e.Type)
                .HasMaxLength(255)
                .HasColumnName("TYPE");
            entity.Property(e => e.Url).HasColumnName("URL");
        });

        modelBuilder.Entity<ClickedMail>(entity =>
        {
            entity.HasKey(e => e.ClickId).HasName("PK__ClickedM__F8E74E2EBB6784DD");

            entity.ToTable("ClickedMail");

            entity.Property(e => e.ClickId).HasColumnName("ClickID");
            entity.Property(e => e.Date)
                .HasColumnType("datetime")
                .HasColumnName("DATE");
            entity.Property(e => e.EmailId).HasColumnName("EmailID");
            entity.Property(e => e.Success).HasColumnName("SUCCESS");

            entity.HasOne(d => d.Email).WithMany(p => p.ClickedMails)
                .HasForeignKey(d => d.EmailId)
                .HasConstraintName("FK__ClickedMa__Email__46E78A0C");
        });

        modelBuilder.Entity<FacebookLogin>(entity =>
        {
            entity.HasKey(e => e.FacebookId).HasName("PK__Facebook__4D6564446103A001");

            entity.ToTable("FacebookLogin");

            entity.Property(e => e.FacebookId).HasColumnName("FacebookID");
            entity.Property(e => e.Email).HasMaxLength(255);
            entity.Property(e => e.Password).HasMaxLength(255);
        });

        modelBuilder.Entity<NetflixLogin>(entity =>
        {
            entity.HasKey(e => e.NetflixId).HasName("PK__NetflixL__4794EB89E9D8EFC3");

            entity.ToTable("NetflixLogin");

            entity.Property(e => e.NetflixId).HasColumnName("NetflixID");
            entity.Property(e => e.Email).HasMaxLength(255);
            entity.Property(e => e.Password).HasMaxLength(255);
        });

        modelBuilder.Entity<SentEmail>(entity =>
        {
            entity.HasKey(e => e.EmailId).HasName("PK__SentEmai__7ED91AEFDEFF6F57");

            entity.ToTable("SentEmail");

            entity.Property(e => e.EmailId).HasColumnName("EmailID");
            entity.Property(e => e.AttackId).HasColumnName("AttackID");
            entity.Property(e => e.SentDate).HasColumnType("datetime");
            entity.Property(e => e.Title).HasMaxLength(255);
            entity.Property(e => e.VictimId).HasColumnName("VictimID");

            entity.HasOne(d => d.Attack).WithMany(p => p.SentEmails)
                .HasForeignKey(d => d.AttackId)
                .HasConstraintName("FK__SentEmail__Attac__440B1D61");

            entity.HasOne(d => d.Victim).WithMany(p => p.SentEmails)
                .HasForeignKey(d => d.VictimId)
                .HasConstraintName("FK__SentEmail__Victi__4316F928");
        });

        modelBuilder.Entity<SpotifyLogin>(entity =>
        {
            entity.HasKey(e => e.SpotifyId).HasName("PK__SpotifyL__958BCEDC16CCD845");

            entity.ToTable("SpotifyLogin");

            entity.Property(e => e.SpotifyId).HasColumnName("SpotifyID");
            entity.Property(e => e.Email).HasMaxLength(255);
            entity.Property(e => e.Password).HasMaxLength(255);
        });

        modelBuilder.Entity<Victim>(entity =>
        {
            entity.HasKey(e => e.VictimId).HasName("PK__Victim__1ABDABAB51F08A7F");

            entity.ToTable("Victim");

            entity.Property(e => e.VictimId).HasColumnName("VictimID");
            entity.Property(e => e.Email).HasMaxLength(255);
            entity.Property(e => e.Name).HasMaxLength(255);
            entity.Property(e => e.Surname).HasMaxLength(255);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
