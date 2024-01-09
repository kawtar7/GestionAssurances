using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace WebApplication4.Models;

public partial class AssurancesContext : DbContext
{
    public AssurancesContext()
    {
    }

    public AssurancesContext(DbContextOptions<AssurancesContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Client> Clients { get; set; }

    public virtual DbSet<Contrat> Contrats { get; set; }

    public virtual DbSet<Correspondant> Correspondants { get; set; }

    public virtual DbSet<Couverture> Couvertures { get; set; }

    public virtual DbSet<DossiersSinistre> DossiersSinistres { get; set; }

    public virtual DbSet<Expert> Experts { get; set; }

    public virtual DbSet<Formule> Formules { get; set; }

    public virtual DbSet<Garanty> Garanties { get; set; }

    public virtual DbSet<Intervention> Interventions { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-ND3V0A6;Database=Assurances;Integrated Security=true;TrustServerCertificate=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Client>(entity =>
        {
            entity.HasKey(e => e.IdClient).HasName("PK__Clients__C1961B33569D5CF4");

            entity.Property(e => e.Adresse)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Nom)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Prenom)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Ville)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Contrat>(entity =>
        {
            entity.HasKey(e => e.IdContrat).HasName("PK__Contrats__253DE8F1E7A73B25");

            entity.Property(e => e.DateEcheance).HasColumnType("date");
            entity.Property(e => e.DateSouscription).HasColumnType("date");

            entity.HasOne(d => d.IdClientNavigation).WithMany(p => p.Contrats)
                .HasForeignKey(d => d.IdClient)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("contrats_ibfk_2");

            entity.HasOne(d => d.IdFormuleNavigation).WithMany(p => p.Contrats)
                .HasForeignKey(d => d.IdFormule)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("contrats_ibfk_1");
        });

        modelBuilder.Entity<Correspondant>(entity =>
        {
            entity.HasKey(e => e.IdCorrespondant).HasName("PK__Correspo__4355DC7583E0731C");

            entity.Property(e => e.NomCorrespondant)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Telephone)
                .HasMaxLength(10)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Couverture>(entity =>
        {
            entity.HasKey(e => new { e.IdFormule, e.IdGarantie }).HasName("PK__Couvertu__8F47434A8DC1C5C5");

            entity.HasOne(d => d.IdFormuleNavigation).WithMany(p => p.Couvertures)
                .HasForeignKey(d => d.IdFormule)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("couvertures_ibfk_1");

            entity.HasOne(d => d.IdGarantieNavigation).WithMany(p => p.Couvertures)
                .HasForeignKey(d => d.IdGarantie)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("couvertures_ibfk_2");
        });

        modelBuilder.Entity<DossiersSinistre>(entity =>
        {
            entity.HasKey(e => e.IdDossierSinistre).HasName("PK__Dossiers__4D6EEC348704E47E");

            entity.Property(e => e.DateCloture).HasColumnType("date");
            entity.Property(e => e.DateCouverture).HasColumnType("date");

            entity.HasOne(d => d.IdContratNavigation).WithMany(p => p.DossiersSinistres)
                .HasForeignKey(d => d.IdContrat)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("dossierssinistres_ibfk_1");

            entity.HasOne(d => d.IdCorrespondantNavigation).WithMany(p => p.DossiersSinistres)
                .HasForeignKey(d => d.IdCorrespondant)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("dossierssinistres_ibfk_3");

            entity.HasOne(d => d.IdExpertNavigation).WithMany(p => p.DossiersSinistres)
                .HasForeignKey(d => d.IdExpert)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("dossierssinistres_ibfk_2");
        });

        modelBuilder.Entity<Expert>(entity =>
        {
            entity.HasKey(e => e.IdExpert).HasName("PK__Experts__38C76CE22FC3BC2B");

            entity.Property(e => e.NomExpert)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Telephone)
                .HasMaxLength(10)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Formule>(entity =>
        {
            entity.HasKey(e => e.IdFormule).HasName("PK__Formules__AC52AF0D8A815835");

            entity.Property(e => e.Libelle)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Garanty>(entity =>
        {
            entity.HasKey(e => e.IdGarantie).HasName("PK__Garantie__315EC47382693748");

            entity.Property(e => e.Libelle)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Intervention>(entity =>
        {
            entity.HasKey(e => new { e.IdDossierIntervention, e.DateIntervention }).HasName("PK__Interven__A9B5C81C7D9B3EA8");

            entity.Property(e => e.DateIntervention).HasColumnType("date");

            entity.HasOne(d => d.IdDossierInterventionNavigation).WithMany(p => p.Interventions)
                .HasForeignKey(d => d.IdDossierIntervention)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("interventions_ibfk_1");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
