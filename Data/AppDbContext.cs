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
                entity.Property(e => e.Id).ValueGeneratedOnAdd(); // 
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
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
                entity.Property(e => e.NombreEvento).IsRequired().HasMaxLength(200);
                entity.Property(e => e.FechaEvento).IsRequired();

            });

            // Configuración de Categoria
            modelBuilder.Entity<Categoria>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd(); 
                entity.Property(e => e.Nombre).IsRequired().HasMaxLength(100);
            });

            // Configuración de Producto
            modelBuilder.Entity<Producto>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
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
                entity.Property(e => e.Id).ValueGeneratedOnAdd(); 
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
                entity.Property(e => e.Id).ValueGeneratedOnAdd(); 
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
                entity.Property(e => e.Id).ValueGeneratedOnAdd(); 
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

                // Verificar si ya existen datos
                if (Usuarios.Any() && Clientes.Any() && Categorias.Any())
                {
                    System.Diagnostics.Debug.WriteLine($"[AppDbContext] Ya existen datos en la BD");
                    
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

                System.Diagnostics.Debug.WriteLine("[AppDbContext] No hay datos, procediendo a crear datos iniciales");

                // Crear datos en orden de dependencias
                if (GruposPermisos.Any() && Permisos.Any())
                {
                    // 1. Usuarios
                    CrearUsuariosIniciales();
                    
                    // 2. Clientes
                    CrearClientesIniciales();
                    
                    // 3. Categorías
                    CrearCategoriasIniciales();
                    
                    // 4. Productos (depende de Categorías)
                    CrearProductosIniciales();
                    
                    // 5. Eventos
                    CrearEventosIniciales();
                    
                    // 6. Reservas (depende de Clientes y Eventos)
                    CrearReservasIniciales();
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
                if (Usuarios.Any())
                {
                    System.Diagnostics.Debug.WriteLine("[AppDbContext] Ya existen usuarios");
                    return;
                }

                System.Diagnostics.Debug.WriteLine("[AppDbContext] Creando usuarios iniciales");

                // Crear usuario admin con hash correcto
                var adminUser = new Usuario("admin", "admin@sics.com", "admin123");
                Usuarios.Add(adminUser);
                System.Diagnostics.Debug.WriteLine("[AppDbContext] Usuario 'admin' creado");
                
                // Crear usuario operador con hash correcto
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
        /// ? Crea clientes de ejemplo
        /// </summary>
        private void CrearClientesIniciales()
        {
            try
            {
                if (Clientes.Any())
                {
                    System.Diagnostics.Debug.WriteLine("[AppDbContext] Ya existen clientes");
                    return;
                }

                System.Diagnostics.Debug.WriteLine("[AppDbContext] Creando clientes iniciales");

                var clientes = new List<Cliente>
                {
                    new Cliente("Juan", "Pérez", "juan.perez@email.com", "1123456789", "Av. Corrientes 1234, CABA"),
                    new Cliente("María", "González", "maria.gonzalez@email.com", "1134567890", "Av. Santa Fe 5678, CABA"),
                    new Cliente("Carlos", "Rodríguez", "carlos.rodriguez@email.com", "1145678901", "Av. Rivadavia 9012, CABA"),
                    new Cliente("Ana", "Martínez", "ana.martinez@email.com", "1156789012", "Av. Cabildo 3456, CABA"),
                    new Cliente("Laura", "López", "laura.lopez@email.com", "1167890123", "Av. Callao 7890, CABA")
                };

                Clientes.AddRange(clientes);
                SaveChanges();
                System.Diagnostics.Debug.WriteLine($"[AppDbContext] {clientes.Count} clientes creados");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[AppDbContext] Error creando clientes iniciales: {ex.Message}");
            }
        }

        /// <summary>
        /// ? Crea categorías de ejemplo
        /// </summary>
        private void CrearCategoriasIniciales()
        {
            try
            {
                if (Categorias.Any())
                {
                    System.Diagnostics.Debug.WriteLine("[AppDbContext] Ya existen categorías");
                    return;
                }

                System.Diagnostics.Debug.WriteLine("[AppDbContext] Creando categorías iniciales");

                var categorias = new List<Categoria>
                {
                    new Categoria("Mobiliario"),
                    new Categoria("Decoración"),
                    new Categoria("Iluminación"),
                    new Categoria("Audio y Video"),
                    new Categoria("Vajilla y Cubertería"),
                    new Categoria("Mantelería"),
                    new Categoria("Equipamiento de Cocina"),
                    new Categoria("Carpas y Gazebos")
                };

                Categorias.AddRange(categorias);
                SaveChanges();
                System.Diagnostics.Debug.WriteLine($"[AppDbContext] {categorias.Count} categorías creadas");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[AppDbContext] Error creando categorías iniciales: {ex.Message}");
            }
        }

        /// <summary>
        /// ? Crea productos de ejemplo
        /// </summary>
        private void CrearProductosIniciales()
        {
            try
            {
                if (Productos.Any())
                {
                    System.Diagnostics.Debug.WriteLine("[AppDbContext] Ya existen productos");
                    return;
                }

                System.Diagnostics.Debug.WriteLine("[AppDbContext] Creando productos iniciales");

                // Obtener las categorías
                var mobiliario = Categorias.First(c => c.Nombre == "Mobiliario");
                var decoracion = Categorias.First(c => c.Nombre == "Decoración");
                var iluminacion = Categorias.First(c => c.Nombre == "Iluminación");
                var audioVideo = Categorias.First(c => c.Nombre == "Audio y Video");
                var vajilla = Categorias.First(c => c.Nombre == "Vajilla y Cubertería");
                var manteleria = Categorias.First(c => c.Nombre == "Mantelería");
                var cocina = Categorias.First(c => c.Nombre == "Equipamiento de Cocina");
                var carpas = Categorias.First(c => c.Nombre == "Carpas y Gazebos");

                var productos = new List<Producto>
                {
                    // Mobiliario
                    new Producto("Silla Tiffany Transparente", 1500.00m, "Silla elegante de acrílico cristal para eventos", 50, mobiliario.Id),
                    new Producto("Mesa Redonda 1.80m", 3500.00m, "Mesa redonda de 1.80m de diámetro para 10 personas", 20, mobiliario.Id),
                    new Producto("Sillón Lounge", 4500.00m, "Sillón moderno estilo lounge para áreas de descanso", 15, mobiliario.Id),
                    new Producto("Banco Alto Bar", 2000.00m, "Banco alto para barra americana", 30, mobiliario.Id),

                    // Decoración
                    new Producto("Centro de Mesa Floral", 800.00m, "Arreglo floral de rosas y hortensias", 40, decoracion.Id),
                    new Producto("Cortina de Tul Blanco 3m", 1200.00m, "Cortina decorativa de tul blanco de 3 metros", 25, decoracion.Id),
                    new Producto("Globos Metalizados Pack x10", 350.00m, "Pack de 10 globos metalizados en diferentes colores", 100, decoracion.Id),
                    new Producto("Arco de Flores", 5500.00m, "Estructura decorativa con arco de flores naturales", 8, decoracion.Id),

                    // Iluminación
                    new Producto("Luz LED Ambiental RGB", 2800.00m, "Luz LED programable con control remoto", 35, iluminacion.Id),
                    new Producto("Guirnalda de Luces 10m", 450.00m, "Guirnalda de luces cálidas de 10 metros", 60, iluminacion.Id),
                    new Producto("Lámpara Colgante Edison", 1800.00m, "Lámpara vintage estilo Edison", 20, iluminacion.Id),
                    new Producto("Proyector LED Exterior", 3200.00m, "Proyector LED para iluminación de fachadas", 12, iluminacion.Id),

                    // Audio y Video
                    new Producto("Equipo de Sonido 500W", 8500.00m, "Sistema de audio profesional de 500W", 10, audioVideo.Id),
                    new Producto("Micrófono Inalámbrico", 1500.00m, "Micrófono inalámbrico profesional", 25, audioVideo.Id),
                    new Producto("Pantalla LED 55 pulgadas", 12000.00m, "Pantalla LED 4K de 55 pulgadas", 8, audioVideo.Id),
                    new Producto("Proyector Full HD", 15000.00m, "Proyector multimedia Full HD con 3000 lúmenes", 5, audioVideo.Id),

                    // Vajilla y Cubertería
                    new Producto("Plato Playo Porcelana Blanca", 180.00m, "Plato playo de porcelana blanca 27cm", 200, vajilla.Id),
                    new Producto("Copa de Vino Cristal", 250.00m, "Copa de vino tinto de cristal fino", 150, vajilla.Id),
                    new Producto("Cubierto Acero Inoxidable Set x3", 320.00m, "Set de cubiertos (tenedor, cuchillo, cuchara)", 180, vajilla.Id),
                    new Producto("Bandeja Rectangular Acero", 950.00m, "Bandeja rectangular de acero inoxidable 40x30cm", 40, vajilla.Id),

                    // Mantelería
                    new Producto("Mantel Blanco Rectangular 2.40m", 850.00m, "Mantel blanco para mesa rectangular de 2.40m", 45, manteleria.Id),
                    new Producto("Camino de Mesa Yute", 420.00m, "Camino de mesa de yute natural 2m", 50, manteleria.Id),
                    new Producto("Servilleta de Tela Blanca", 120.00m, "Servilleta de tela 100% algodón 40x40cm", 300, manteleria.Id),
                    new Producto("Funda para Silla Tiffany", 180.00m, "Funda de lycra blanca para silla tiffany", 80, manteleria.Id),

                    // Equipamiento de Cocina
                    new Producto("Cafetera Industrial 100 Tazas", 18000.00m, "Cafetera eléctrica industrial para 100 tazas", 3, cocina.Id),
                    new Producto("Hielera Portátil 50L", 2200.00m, "Hielera portátil aislada de 50 litros", 20, cocina.Id),
                    new Producto("Calentador de Alimentos", 4500.00m, "Calentador eléctrico para bandejas", 10, cocina.Id),
                    new Producto("Dispensador de Bebidas 20L", 3800.00m, "Dispensador de bebidas con grifo 20 litros", 15, cocina.Id),

                    // Carpas y Gazebos
                    new Producto("Carpa 3x3m Blanca", 15000.00m, "Carpa plegable impermeable 3x3 metros", 12, carpas.Id),
                    new Producto("Gazebo Hexagonal 4m", 28000.00m, "Gazebo hexagonal con paredes laterales", 6, carpas.Id),
                    new Producto("Carpa Transparente 6x6m", 45000.00m, "Carpa tipo cristal transparente 6x6 metros", 3, carpas.Id),
                    new Producto("Paredes Laterales para Carpa 3x3", 3500.00m, "Set de 4 paredes laterales para carpa 3x3m", 15, carpas.Id)
                };

                Productos.AddRange(productos);
                SaveChanges();
                System.Diagnostics.Debug.WriteLine($"[AppDbContext] {productos.Count} productos creados");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[AppDbContext] Error creando productos iniciales: {ex.Message}");
            }
        }

        /// <summary>
        /// ? Crea eventos de ejemplo
        /// </summary>
        private void CrearEventosIniciales()
        {
            try
            {
                if (Eventos.Any())
                {
                    System.Diagnostics.Debug.WriteLine("[AppDbContext] Ya existen eventos");
                    return;
                }

                System.Diagnostics.Debug.WriteLine("[AppDbContext] Creando eventos iniciales");

                var hoy = DateTime.Now;
                var eventos = new List<Evento>
                {
                    new Evento("Boda de Verano", hoy.AddDays(30)),
                    new Evento("Cumpleaños 15 Años", hoy.AddDays(15)),
                    new Evento("Evento Corporativo Año Nuevo", hoy.AddDays(45)),
                    new Evento("Fiesta de Graduación", hoy.AddDays(60)),
                    new Evento("Aniversario de Bodas", hoy.AddDays(20)),
                    new Evento("Baby Shower", hoy.AddDays(10)),
                    new Evento("Conferencia Empresarial", hoy.AddDays(25)),
                    new Evento("Cena de Gala Benéfica", hoy.AddDays(40))
                };

                Eventos.AddRange(eventos);
                SaveChanges();
                System.Diagnostics.Debug.WriteLine($"[AppDbContext] {eventos.Count} eventos creados");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[AppDbContext] Error creando eventos iniciales: {ex.Message}");
            }
        }

        /// <summary>
        /// ? Crea reservas de ejemplo con productos
        /// </summary>
        private void CrearReservasIniciales()
        {
            try
            {
                if (Reservas.Any())
                {
                    System.Diagnostics.Debug.WriteLine("[AppDbContext] Ya existen reservas");
                    return;
                }

                System.Diagnostics.Debug.WriteLine("[AppDbContext] Creando reservas iniciales");

                var hoy = DateTime.Now;
                
                // Obtener clientes y eventos
                var cliente1 = Clientes.First(c => c.Nombre == "Juan");
                var cliente2 = Clientes.First(c => c.Nombre == "María");
                var cliente3 = Clientes.First(c => c.Nombre == "Carlos");
                
                var evento1 = Eventos.First(e => e.NombreEvento == "Boda de Verano");
                var evento2 = Eventos.First(e => e.NombreEvento == "Cumpleaños 15 Años");
                var evento3 = Eventos.First(e => e.NombreEvento == "Evento Corporativo Año Nuevo");

                // Obtener algunos productos
                var sillaTiffany = Productos.First(p => p.Nombre == "Silla Tiffany Transparente");
                var mesaRedonda = Productos.First(p => p.Nombre == "Mesa Redonda 1.80m");
                var centroMesa = Productos.First(p => p.Nombre == "Centro de Mesa Floral");
                var equipoSonido = Productos.First(p => p.Nombre == "Equipo de Sonido 500W");
                var platoPlayo = Productos.First(p => p.Nombre == "Plato Playo Porcelana Blanca");
                var copaVino = Productos.First(p => p.Nombre == "Copa de Vino Cristal");
                var carpa = Productos.First(p => p.Nombre == "Carpa 3x3m Blanca");

                // Reserva 1 - Boda
                var reserva1 = new Reserva(cliente1.Id, evento1.Id, evento1.FechaEvento.AddDays(1), "Confirmada");
                reserva1.AgregarProducto(sillaTiffany.Id, 100);
                reserva1.AgregarProducto(mesaRedonda.Id, 10);
                reserva1.AgregarProducto(centroMesa.Id, 10);
                reserva1.AgregarProducto(equipoSonido.Id, 1);
                reserva1.AgregarProducto(platoPlayo.Id, 100);
                reserva1.AgregarProducto(copaVino.Id, 100);
                reserva1.AgregarProducto(carpa.Id, 2);
                Reservas.Add(reserva1);

                // Reserva 2 - Cumpleaños
                var reserva2 = new Reserva(cliente2.Id, evento2.Id, evento2.FechaEvento.AddDays(1), "Pendiente");
                reserva2.AgregarProducto(sillaTiffany.Id, 50);
                reserva2.AgregarProducto(mesaRedonda.Id, 5);
                reserva2.AgregarProducto(centroMesa.Id, 5);
                reserva2.AgregarProducto(platoPlayo.Id, 50);
                Reservas.Add(reserva2);

                // Reserva 3 - Evento Corporativo
                var reserva3 = new Reserva(cliente3.Id, evento3.Id, evento3.FechaEvento.AddDays(1), "Confirmada");
                var proyector = Productos.First(p => p.Nombre == "Proyector Full HD");
                var pantalla = Productos.First(p => p.Nombre == "Pantalla LED 55 pulgadas");
                var microfono = Productos.First(p => p.Nombre == "Micrófono Inalámbrico");
                reserva3.AgregarProducto(sillaTiffany.Id, 80);
                reserva3.AgregarProducto(proyector.Id, 1);
                reserva3.AgregarProducto(pantalla.Id, 2);
                reserva3.AgregarProducto(microfono.Id, 3);
                reserva3.AgregarProducto(equipoSonido.Id, 1);
                Reservas.Add(reserva3);

                SaveChanges();
                System.Diagnostics.Debug.WriteLine($"[AppDbContext] 3 reservas creadas con productos asociados");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[AppDbContext] Error creando reservas iniciales: {ex.Message}");
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

                // Asignar TODOS los permisos al grupo Administrador
                foreach (var permiso in todosLosPermisos)
                {
                    if (!grupoAdmin.Permisos.Contains(permiso))
                    {
                        grupoAdmin.AgregarPermiso(permiso);
                    }
                }

                // Asignar permisos específicos al grupo Operador
                var permisosOperador = todosLosPermisos.Where(p =>
                            (p.Categoria == "reservas") || // Todos los permisos de reservas, es quien las lleva a cabo.
                            (p.Categoria == "reservaproducto") || // Todos los permisos de productos en reservas
                            (p.Categoria == "clientes" && p.Nombre == "leer") || // Solo leer clientes
                            (p.Categoria == "productos" && (p.Nombre == "leer" || p.Nombre == "actualizar")) || // Leer y actualizar stock de productos
                            (p.Categoria == "eventos") || // Para asignarle eventos a las reservas, pero debería poder crearlos en el momento si no existe el requerido.
                            (p.Categoria == "categorias" && p.Nombre == "leer") // Solo lectura de categorías
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