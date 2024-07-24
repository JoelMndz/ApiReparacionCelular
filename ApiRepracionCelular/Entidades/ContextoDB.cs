﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ApiRepracionCelular.Entidades;

public partial class ContextoDB : DbContext
{
    public ContextoDB(DbContextOptions<ContextoDB> options)
        : base(options)
    {
    }

    public virtual DbSet<Cliente> Cliente { get; set; }

    public virtual DbSet<Reparacion> Reparacion { get; set; }

    public virtual DbSet<ReparacionDetalle> ReparacionDetalle { get; set; }

    public virtual DbSet<Rol> Rol { get; set; }

    public virtual DbSet<Usuario> Usuario { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Cliente__3214EC07F86DDEC8");

            entity.Property(e => e.Email)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Nombre)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Telefono)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Reparacion>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Reparaci__3214EC073DAB9CD8");

            entity.Property(e => e.Estado)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.FechaEntrega).HasColumnType("datetime");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.IdClienteNavigation).WithMany(p => p.Reparacion)
                .HasForeignKey(d => d.IdCliente)
                .HasConstraintName("FK__Reparacio__IdCli__2D27B809");

            entity.HasOne(d => d.IdTecnicoNavigation).WithMany(p => p.Reparacion)
                .HasForeignKey(d => d.IdTecnico)
                .HasConstraintName("FK__Reparacio__IdTec__2E1BDC42");
        });

        modelBuilder.Entity<ReparacionDetalle>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Reparaci__3214EC07475ACF62");

            entity.Property(e => e.Descripcion)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Precio).HasColumnType("decimal(9, 2)");

            entity.HasOne(d => d.IdReparacionNavigation).WithMany(p => p.ReparacionDetalle)
                .HasForeignKey(d => d.IdReparacion)
                .HasConstraintName("FK__Reparacio__IdRep__30F848ED");
        });

        modelBuilder.Entity<Rol>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Rol__3214EC07AAD46B8C");

            entity.Property(e => e.Nombre)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Usuario__3214EC0753640BAA");

            entity.Property(e => e.Email)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Nombre)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Password)
                .IsRequired()
                .IsUnicode(false);
            entity.Property(e => e.Telefono)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.IdRolNavigation).WithMany(p => p.Usuario)
                .HasForeignKey(d => d.IdRol)
                .HasConstraintName("FK__Usuario__IdRol__267ABA7A");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}