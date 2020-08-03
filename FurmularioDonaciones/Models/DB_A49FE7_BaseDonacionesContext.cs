using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace FurmularioDonaciones.Models
{
    public partial class DB_A49FE7_BaseDonacionesContext : DbContext
    {
        public DB_A49FE7_BaseDonacionesContext()
        {
        }

        public DB_A49FE7_BaseDonacionesContext(DbContextOptions<DB_A49FE7_BaseDonacionesContext> options)
            : base(options)
        {
        }

        public virtual DbSet<FormularioDonacion> FormularioDonacion { get; set; }
        public virtual DbSet<Marcas> Marcas { get; set; }
        public virtual DbSet<Rol> Rol { get; set; }
        public virtual DbSet<Usuario> Usuario { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("server=sql5045.site4now.net;database=DB_A49FE7_BaseDonaciones;persist security info=True;user id=DB_A49FE7_BaseDonaciones_admin;password=dyhUNZbFzK5kj2LT;MultipleActiveResultSets=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FormularioDonacion>(entity =>
            {
                entity.HasKey(e => e.IdFormulario);

                entity.ToTable("FORMULARIO_DONACION");

                entity.Property(e => e.IdFormulario)
                    .HasColumnName("ID_FORMULARIO")
                    .HasColumnType("numeric(18, 0)")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.ApellidoDonante)
                    .IsRequired()
                    .HasColumnName("APELLIDO_DONANTE")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.CiDonante)
                    .IsRequired()
                    .HasColumnName("CI_DONANTE")
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.FechaAcopioDonacion)
                    .HasColumnName("FECHA_ACOPIO_DONACION")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.FechaVencimientoProducto)
                    .HasColumnName("FECHA_VENCIMIENTO_PRODUCTO")
                    .HasColumnType("datetime");

                entity.Property(e => e.Foto)
                    .IsRequired()
                    .HasColumnName("FOTO")
                    .HasColumnType("image");

                entity.Property(e => e.IdMarca).HasColumnName("ID_MARCA");

                entity.Property(e => e.IdUsuario).HasColumnName("ID_USUARIO");

                entity.Property(e => e.LoteProductoDonancion)
                    .IsRequired()
                    .HasColumnName("LOTE_PRODUCTO_DONANCION")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.NombreDonante)
                    .IsRequired()
                    .HasColumnName("NOMBRE_DONANTE")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.Tipoimagen)
                    .IsRequired()
                    .HasColumnName("TIPOIMAGEN")
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdMarcaNavigation)
                    .WithMany(p => p.FormularioDonacion)
                    .HasForeignKey(d => d.IdMarca)
                    .HasConstraintName("FK_FORMULAR_REFERENCE_MARCAS");

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.FormularioDonacion)
                    .HasForeignKey(d => d.IdUsuario)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FORMULAR_REFERENCE_USUARIO");
            });

            modelBuilder.Entity<Marcas>(entity =>
            {
                entity.HasKey(e => e.IdMarca);

                entity.ToTable("MARCAS");

                entity.Property(e => e.IdMarca).HasColumnName("ID_MARCA");

                entity.Property(e => e.NombreMarca)
                    .IsRequired()
                    .HasColumnName("NOMBRE_MARCA")
                    .HasMaxLength(25)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Rol>(entity =>
            {
                entity.HasKey(e => e.IdRol);

                entity.ToTable("ROL");

                entity.Property(e => e.IdRol).HasColumnName("ID_ROL");

                entity.Property(e => e.NombreRol)
                    .IsRequired()
                    .HasColumnName("NOMBRE_ROL")
                    .HasMaxLength(25)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasKey(e => e.IdUsuario);

                entity.ToTable("USUARIO");

                entity.Property(e => e.IdUsuario).HasColumnName("ID_USUARIO");

                entity.Property(e => e.ApellidoUsuario)
                    .IsRequired()
                    .HasColumnName("APELLIDO_USUARIO")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.EMailUsuario)
                    .IsRequired()
                    .HasColumnName("E_MAIL_USUARIO")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.FechaCreacionUsuario)
                    .HasColumnName("FECHA_CREACION_USUARIO")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.IdRol).HasColumnName("ID_ROL");

                entity.Property(e => e.NombreUsuario)
                    .IsRequired()
                    .HasColumnName("NOMBRE_USUARIO")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.PasswordUsuario)
                    .IsRequired()
                    .HasColumnName("PASSWORD_USUARIO")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdRolNavigation)
                    .WithMany(p => p.Usuario)
                    .HasForeignKey(d => d.IdRol)
                    .HasConstraintName("FK_USUARIO_REFERENCE_ROL");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
