using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Examen.Models;

public partial class ExamenContext : DbContext
{
    public ExamenContext()
    {
    }

    public ExamenContext(DbContextOptions<ExamenContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Estudiante> Estudiantes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
      //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
      // => optionsBuilder.UseSqlServer("server=localhost; database=Examen; integrated security=true; TrustServerCertificate=True");

        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Estudiante>(entity =>
        {
            entity.Property(e => e.Cedula).IsUnicode(false);
            entity.Property(e => e.CedulaMadre).IsUnicode(false);
            entity.Property(e => e.CedulaPadre).IsUnicode(false);
            entity.Property(e => e.FechaDeNacimiento).HasColumnType("date");
            entity.Property(e => e.Nombre).IsUnicode(false);
            entity.Property(e => e.PrimerApellido).IsUnicode(false);
            entity.Property(e => e.SegundoApellido).IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
