using Microsoft.EntityFrameworkCore;
using Domain.Model;

namespace Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Usuario> Usuarios { get; set; } = null!;
        public DbSet<Cliente> Clientes { get; set; } = null!;
        public DbSet<Evento> Eventos { get; set; } = null!;
        public DbSet<Reserva> Reservas { get; set; } = null!;
        public DbSet<Categoria> Categorias { get; set; } = null!;
        public DbSet<Producto> Productos { get; set; } = null!;
        public DbSet<ReservaProducto> ReservaProductos { get; set; } = null!;

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuración de Usuario
            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd(); 
                entity.Property(e => e.Nombre).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Apellido).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Email).IsRequired().HasMaxLength(200);
                entity.Property(e => e.Username).IsRequired().HasMaxLength(50);
                entity.Property(e => e.PasswordHash).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Salt).IsRequired().HasMaxLength(100);
                
                // Índice único para Username
                entity.HasIndex(e => e.Username).IsUnique();
            });

            // Configuración de Cliente
            modelBuilder.Entity<Cliente>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd(); // ? AUTO-INCREMENT
                entity.Property(e => e.Nombre).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Apellido).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Email).IsRequired().HasMaxLength(200);
                entity.Property(e => e.Telefono).IsRequired().HasMaxLength(20);
                entity.Property(e => e.Direccion).IsRequired().HasMaxLength(300);
            });

            // Configuración de Evento
            modelBuilder.Entity<Evento>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd(); // ? AUTO-INCREMENT
                entity.Property(e => e.NombreEvento).IsRequired().HasMaxLength(200);
                entity.Property(e => e.FechaEvento).IsRequired();
            });

            // Configuración de Categoria
            modelBuilder.Entity<Categoria>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd(); // ? AUTO-INCREMENT
                entity.Property(e => e.Nombre).IsRequired().HasMaxLength(100);
            });

            // Configuración de Producto
            modelBuilder.Entity<Producto>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd(); // ? AUTO-INCREMENT
                entity.Property(e => e.Nombre).IsRequired().HasMaxLength(200);
                entity.Property(e => e.Descripcion).IsRequired().HasMaxLength(500);
                entity.Property(e => e.Precio).IsRequired().HasColumnType("decimal(18,2)");
                entity.Property(e => e.Stock).IsRequired();

                // Relación con Categoria
                entity.HasOne(p => p.Categoria)
                    .WithMany(c => c.Productos)
                    .HasForeignKey(p => p.CategoriaId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // Configuración de Reserva
            modelBuilder.Entity<Reserva>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd(); // ? AUTO-INCREMENT
                entity.Property(e => e.FechaReserva).IsRequired();
                entity.Property(e => e.Estado).IsRequired().HasMaxLength(50);

                // Relaciones
                entity.HasOne(r => r.Cliente)
                    .WithMany()
                    .HasForeignKey(r => r.ClienteId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(r => r.Evento)
                    .WithMany()
                    .HasForeignKey(r => r.EventoId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // Configuración de ReservaProducto
            modelBuilder.Entity<ReservaProducto>(entity =>
            {
                entity.HasKey(e => new { e.ReservaId, e.ProductoId }); // Clave compuesta
                entity.Property(e => e.CantidadReservada).IsRequired();

                // Relaciones
                entity.HasOne<Reserva>()
                    .WithMany(r => r.Productos)
                    .HasForeignKey(rp => rp.ReservaId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(rp => rp.Producto)
                    .WithMany()
                    .HasForeignKey(rp => rp.ProductoId)
                    .OnDelete(DeleteBehavior.Restrict);
            });
        }
    }
}