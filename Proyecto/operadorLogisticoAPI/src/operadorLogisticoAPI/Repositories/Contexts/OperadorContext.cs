using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using operadorLogisticoAPI.Repositories.Entities;

namespace operadorLogisticoAPI.Repositories.Contexts
{
    public partial class OperadorContext : DbContext
    {
        public OperadorContext()
        {
        }

        public OperadorContext(DbContextOptions<OperadorContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Contacto> Contacto { get; set; }
        public virtual DbSet<Envio> Envio { get; set; }
        public virtual DbSet<Producto> Producto { get; set; }
        public virtual DbSet<Repartidores> Repartidores { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Name=ConnectionStrings:SqlServerConnection");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Contacto>(entity =>
            {
                entity.HasKey(e => e.Documento)
                    .HasName("PK__contacto__A25B3E60393E23B0");

                entity.ToTable("contacto");

                entity.Property(e => e.Documento)
                    .HasColumnName("documento")
                    .ValueGeneratedNever();

                entity.Property(e => e.Apellido)
                    .HasColumnName("apellido")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Email)
                    .HasColumnName("email")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Nombre)
                    .HasColumnName("nombre")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Envio>(entity =>
            {
                entity.ToTable("envio");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CalleDestino)
                    .HasColumnName("calleDestino")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.CalleOrigen)
                    .HasColumnName("calleOrigen")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.CodPostalDestino).HasColumnName("codPostalDestino");

                entity.Property(e => e.CodPostalOrigen).HasColumnName("codPostalOrigen");

                entity.Property(e => e.Delicado)
                    .HasColumnName("delicado")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.DniContacto).HasColumnName("dniContacto");

                entity.Property(e => e.DniRepartidor).HasColumnName("dniRepartidor");

                entity.Property(e => e.Estado)
                    .HasColumnName("estado")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.FechaEntrega)
                    .HasColumnName("fechaEntrega")
                    .HasColumnType("date");

                entity.Property(e => e.FechaRecepcion)
                    .HasColumnName("fechaRecepcion")
                    .HasColumnType("date");

                entity.Property(e => e.LocalidadDestino)
                    .HasColumnName("localidadDestino")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.LocalidadOrigen)
                    .HasColumnName("localidadOrigen")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.NroDestino).HasColumnName("nroDestino");

                entity.Property(e => e.NroOrigen).HasColumnName("nroOrigen");

                entity.Property(e => e.NroProducto).HasColumnName("nroProducto");

                entity.Property(e => e.PesoProd).HasColumnName("pesoProd");

                entity.Property(e => e.ProvinciaDestino)
                    .HasColumnName("provinciaDestino")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.ProvinciaOrigen)
                    .HasColumnName("provinciaOrigen")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.TamañoProd)
                    .HasColumnName("tamañoProd")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                // entity.HasOne(d => d.DniContactoNavigation)
                //     .WithMany(p => p.Envio)
                //     .HasForeignKey(d => d.DniContacto)
                //     .HasConstraintName("FK__envio__dniContac__5CD6CB2B");

                // entity.HasOne(d => d.DniRepartidorNavigation)
                //     .WithMany(p => p.Envio)
                //     .HasForeignKey(d => d.DniRepartidor)
                //     .HasConstraintName("FK__envio__dniRepart__5EBF139D");

                // entity.HasOne(d => d.NroProductoNavigation)
                //     .WithMany(p => p.Envio)
                //     .HasForeignKey(d => d.NroProducto)
                //     .HasConstraintName("FK__envio__nroProduc__5FB337D6");
            });

            modelBuilder.Entity<Producto>(entity =>
            {
                entity.ToTable("producto");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Delicado)
                    .HasColumnName("delicado")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Peso).HasColumnName("peso");

                entity.Property(e => e.Tamaño)
                    .HasColumnName("tamaño")
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Repartidores>(entity =>
            {
                entity.HasKey(e => e.Documento)
                    .HasName("PK__repartid__A25B3E60014DC5F4");

                entity.ToTable("repartidores");

                entity.Property(e => e.Documento)
                    .HasColumnName("documento")
                    .ValueGeneratedNever();

                entity.Property(e => e.Apellido)
                    .HasColumnName("apellido")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Email)
                    .HasColumnName("email")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Nombre)
                    .HasColumnName("nombre")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.IsDeleted)
                    .HasColumnName("isDeleted");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
