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
        public DbSet<Permiso> Permisos { get; set; } = null!;
        public DbSet<GrupoPermiso> GruposPermisos { get; set; } = null!;
        
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            this.Database.EnsureCreated();
            SeedInitialData();
        }

        internal AppDbContext()
        {
            this.Database.EnsureCreated();
            SeedInitialData();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuración de Usuario
            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd(); 
                entity.Property(e => e.Username).IsRequired().HasMaxLength(50);
                entity.Property(e => e.Email).IsRequired().HasMaxLength(200);
                entity.Property(e => e.PasswordHash).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Salt).IsRequired().HasMaxLength(255);
                
                // Índice único para Username
                entity.HasIndex(e => e.Username).IsUnique();
                entity.HasIndex(e => e.Email).IsUnique();

                // Relación con GrupoPermiso
                entity.HasOne(e => e.Grupo)
                    .WithMany()
                    .HasForeignKey(e => e.GrupoPermisoId)
                    .OnDelete(DeleteBehavior.SetNull);

                
                // Los usuarios  se crean dinámicamente en SeedInitialData()
                // para garantizar que el hash de contraseña sea correcto
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
                entity.Property(e => e.FechaFinalizacion).IsRequired();
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
                entity.HasOne(rp => rp.Reserva)
                    .WithMany(r => r.Productos)
                    .HasForeignKey(rp => rp.ReservaId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(rp => rp.Producto)
                    .WithMany()
                    .HasForeignKey(rp => rp.ProductoId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // Configuracion de permiso
            modelBuilder.Entity<Permiso>(entity =>
            {
                entity.ToTable("Permisos");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd(); // ? AUTO-INCREMENT
                entity.Property(e => e.Nombre).IsRequired().HasMaxLength(50);
                entity.Property(e => e.Descripcion).IsRequired().HasMaxLength(200);
                entity.Property(e => e.Categoria).IsRequired().HasMaxLength(30);
                entity.Property(e => e.Activo).IsRequired();

                // para evitar permisos duplicados
                entity.HasIndex(e => new { e.Nombre, e.Categoria })
                    .IsUnique();
            });

            // Configuracion de grupo permiso
            modelBuilder.Entity<GrupoPermiso>(entity =>
            {
                entity.ToTable("GruposPermisos");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd(); // ? AUTO-INCREMENT
                entity.Property(e => e.Nombre).IsRequired().HasMaxLength(50);
                entity.Property(e => e.Descripcion).IsRequired().HasMaxLength(200);
                entity.Property(e => e.FechaCreacion).IsRequired();
                entity.Property(e => e.Activo).IsRequired();

                // para evitar grupos de permisos duplicados
                entity.HasIndex(e => e.Nombre)
                    .IsUnique();

                // Configurar la relación muchos a muchos entre GrupoPermiso y Permiso
                modelBuilder.Entity<GrupoPermiso>()
                    .HasMany(gp => gp.Permisos)
                    .WithMany(p => p.Grupos)
                    .UsingEntity<Dictionary<string, object>>(
                        "GrupoPermisoPermiso",
                        j => j
                            .HasOne<Permiso>()
                            .WithMany()
                            .HasForeignKey("PermisoId")
                            .OnDelete(DeleteBehavior.Cascade),
                        j => j
                            .HasOne<GrupoPermiso>()
                            .WithMany()
                            .HasForeignKey("GrupoPermisoId")
                            .OnDelete(DeleteBehavior.Cascade),
                        j =>
                        {
                            j.HasKey("GrupoPermisoId", "PermisoId");
                            j.ToTable("GrupoPermisoPermisos");
                        });
            });

            // seed de permisos
            modelBuilder.Entity<Permiso>().HasData(
                //Permisos para usuarios
                new { Id = 1, Nombre = "leer", Descripcion = "Leer usuarios", Categoria = "usuarios", Activo = true },
                new { Id = 2, Nombre = "agregar", Descripcion = "Agregar usuarios", Categoria = "usuarios", Activo = true },
                new { Id = 3, Nombre = "actualizar", Descripcion = "Actualizar usuarios", Categoria = "usuarios", Activo = true },
                new { Id = 4, Nombre = "eliminar", Descripcion = "Eliminar usuarios", Categoria = "usuarios", Activo = true },

                //Permisos para clientes
                new { Id = 5, Nombre = "leer", Descripcion = "Leer clientes", Categoria = "clientes", Activo = true },
                new { Id = 6, Nombre = "agregar", Descripcion = "Agregar clientes", Categoria = "clientes", Activo = true },
                new { Id = 7, Nombre = "actualizar", Descripcion = "Actualizar clientes", Categoria = "clientes", Activo = true },
                new { Id = 8, Nombre = "eliminar", Descripcion = "Eliminar clientes", Categoria = "clientes", Activo = true },

                //Permisos para eventos
                new { Id = 9, Nombre = "leer", Descripcion = "Leer eventos", Categoria = "eventos", Activo = true },
                new { Id = 10, Nombre = "agregar", Descripcion = "Agregar eventos", Categoria = "eventos", Activo = true },
                new { Id = 11, Nombre = "actualizar", Descripcion = "Actualizar eventos", Categoria = "eventos", Activo = true },
                new { Id = 12, Nombre = "eliminar", Descripcion = "Eliminar eventos", Categoria = "eventos", Activo = true },

                //Permisos para categorias
                new { Id = 13, Nombre = "leer", Descripcion = "Leer categorias", Categoria = "categorias", Activo = true },
                new { Id = 14, Nombre = "agregar", Descripcion = "Agregar categorias", Categoria = "categorias", Activo = true },
                new { Id = 15, Nombre = "actualizar", Descripcion = "Actualizar categorias", Categoria = "categorias", Activo = true },
                new { Id = 16, Nombre = "eliminar", Descripcion = "Eliminar categorias", Categoria = "categorias", Activo = true },

                // Permisos para productos
                new { Id = 17, Nombre = "leer", Descripcion = "Leer productos", Categoria = "productos", Activo = true },
                new { Id = 18, Nombre = "agregar", Descripcion = "Agregar productos", Categoria = "productos", Activo = true },
                new { Id = 19, Nombre = "actualizar", Descripcion = "Actualizar productos", Categoria = "productos", Activo = true },
                new { Id = 20, Nombre = "eliminar", Descripcion = "Eliminar productos", Categoria = "productos", Activo = true },

                // Permisos para reservas
                new { Id = 21, Nombre = "leer", Descripcion = "Leer reservas", Categoria = "reservas", Activo = true },
                new { Id = 22, Nombre = "agregar", Descripcion = "Agregar reservas", Categoria = "reservas", Activo = true },
                new { Id = 23, Nombre = "actualizar", Descripcion = "Actualizar reservas", Categoria = "reservas", Activo = true },
                new { Id = 24, Nombre = "eliminar", Descripcion = "Eliminar reservas", Categoria = "reservas", Activo = true },

                // Permisos para reservas productos
                new { Id = 25, Nombre = "leer", Descripcion = "Leer productos de reserva", Categoria = "reservaproducto", Activo = true},
                new { Id = 26, Nombre = "agregar", Descripcion = "Agregar productos a reserva", Categoria = "reservaproducto", Activo = true },
                new { Id = 27, Nombre = "actualizar", Descripcion = "Actualizar productos de reserva", Categoria = "reservaproducto", Activo = true },
                new { Id = 28, Nombre = "eliminar", Descripcion = "Eliminar productos de reserva", Categoria = "reservaproducto", Activo = true }

            );

            // seed de grupos de permisos
            var fechaCreacion = DateTime.Now;
            modelBuilder.Entity<GrupoPermiso>().HasData(
                new { Id = 1, Nombre = "Administrador", Descripcion = "Acceso completo a todas las funcionalidades", Activo = true, FechaCreacion = fechaCreacion },
                new { Id = 2, Nombre = "Operador", Descripcion = "Manejo de reservas", Activo = true, FechaCreacion = fechaCreacion }
            );


        }

        /// <summary>
        /// ? Método refactorizado para crear datos iniciales dinámicamente
        /// </summary>
        private void SeedInitialData()
        {
            try
            {
                System.Diagnostics.Debug.WriteLine("[AppDbContext] Iniciando SeedInitialData");

                // Verificar si ya existen usuarios
                if (Usuarios.Any())
                {
                    System.Diagnostics.Debug.WriteLine($"[AppDbContext] Ya existen {Usuarios.Count()} usuarios en la BD");
                    
                    // Si ya hay usuarios, solo actualizar relaciones si es necesario
                    if (!Usuarios.Any(u => u.GrupoPermisoId != null) &&
                        GruposPermisos.Any() &&
                        Permisos.Any())
                    {
                        System.Diagnostics.Debug.WriteLine("[AppDbContext] Asignando relaciones a usuarios existentes");
                        AsignarRelacionesIniciales();
                    }
                    return;
                }

                System.Diagnostics.Debug.WriteLine("[AppDbContext] No hay usuarios, procediendo a crear usuarios iniciales");

                // Si no hay usuarios, crearlos
                if (!Usuarios.Any() && GruposPermisos.Any() && Permisos.Any())
                {
                    CrearUsuariosIniciales();
                }
            }
            catch (Exception ex)
            {
                // Log del error pero no detener la aplicación
                System.Diagnostics.Debug.WriteLine($"[AppDbContext] Error en SeedInitialData: {ex.Message}");
                System.Diagnostics.Debug.WriteLine($"[AppDbContext] Stack trace: {ex.StackTrace}");
            }
        }

        /// <summary>
        /// ? Crea los usuarios iniciales admin y operador con contraseñas hasheadas 
        /// </summary>
        private void CrearUsuariosIniciales()
        {
            try
            {
                System.Diagnostics.Debug.WriteLine("[AppDbContext] Creando usuarios iniciales");

                // ? Crear usuario admin con hash correcto
                var adminUser = new Usuario("admin", "admin@sics.com", "admin123");
                Usuarios.Add(adminUser);
                System.Diagnostics.Debug.WriteLine("[AppDbContext] Usuario 'admin' creado");
                
                // ? Crear usuario operador con hash correcto
                var operadorUser = new Usuario("operador", "operador@sics.com", "operador123");
                Usuarios.Add(operadorUser);
                System.Diagnostics.Debug.WriteLine("[AppDbContext] Usuario 'operador' creado");

                SaveChanges();
                System.Diagnostics.Debug.WriteLine("[AppDbContext] Usuarios guardados en BD");

                // Asignar grupos después de crear los usuarios
                AsignarRelacionesIniciales();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[AppDbContext] Error creando usuarios iniciales: {ex.Message}");
            }
        }

        /// <summary>
        /// ? Asigna grupos y permisos a los usuarios existentes
        /// </summary>
        private void AsignarRelacionesIniciales()
        {
            try
            {
                System.Diagnostics.Debug.WriteLine("[AppDbContext] Asignando relaciones iniciales");

                // Cargar datos necesarios
                var adminUser = Usuarios.Include(u => u.Grupo).FirstOrDefault(u => u.Username == "admin");
                var operadorUser = Usuarios.Include(u => u.Grupo).FirstOrDefault(u => u.Username == "operador");
                var grupoAdmin = GruposPermisos.Include(g => g.Permisos).FirstOrDefault(g => g.Nombre == "Administrador");
                var grupoOperador = GruposPermisos.Include(g => g.Permisos).FirstOrDefault(g => g.Nombre == "Operador");
                var todosLosPermisos = Permisos.ToList();

                if (grupoAdmin == null || grupoOperador == null || !todosLosPermisos.Any())
                {
                    System.Diagnostics.Debug.WriteLine("[AppDbContext] No se encontraron grupos o permisos necesarios");
                    return;
                }

                System.Diagnostics.Debug.WriteLine($"[AppDbContext] Grupos encontrados: {grupoAdmin.Nombre}, {grupoOperador.Nombre}");
                System.Diagnostics.Debug.WriteLine($"[AppDbContext] Total de permisos: {todosLosPermisos.Count}");

                // Asignar TODOS los permisos al grupo Administrador
                foreach (var permiso in todosLosPermisos)
                {
                    if (!grupoAdmin.Permisos.Contains(permiso))
                    {
                        grupoAdmin.AgregarPermiso(permiso);
                    }
                }
                System.Diagnostics.Debug.WriteLine($"[AppDbContext] Permisos asignados al grupo Administrador: {grupoAdmin.Permisos.Count}");

                // Asignar permisos específicos al grupo Operador
                var permisosOperador = todosLosPermisos.Where(p =>
                            (p.Categoria == "reservas") || // Todos los permisos de reservas, es quien las lleva a cabo.
                            (p.Categoria == "reservaproducto") || // Todos los permisos de productos en reservas
                            (p.Categoria == "clientes" && p.Nombre == "leer") || // Solo leer clientes
                            (p.Categoria == "productos" && p.Nombre == "leer") || // Solo leer productos
                            (p.Categoria == "productos" && p.Nombre == "actualizar") || // Actualizar stock de productos
                            (p.Categoria == "eventos") // Para asignarle eventos a las reservas, pero debería poder crearlos en el momento si no existe el requerido.
                ).ToList();

                foreach (var permiso in permisosOperador)
                {
                    if (!grupoOperador.Permisos.Contains(permiso))
                    {
                        grupoOperador.AgregarPermiso(permiso);
                    }
                }

                // Asignar usuario admin al grupo Administrador
                if (adminUser != null && adminUser.GrupoPermisoId == null)
                {
                    adminUser.SetGrupo(grupoAdmin);
                }

                // Asignar usuario operador al grupo operador
                if (operadorUser != null && operadorUser.GrupoPermisoId == null)
                {
                    operadorUser.SetGrupo(grupoOperador);
                }

                SaveChanges();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[AppDbContext] Error asignando relaciones iniciales: {ex.Message}");
            }
        }
    }
}