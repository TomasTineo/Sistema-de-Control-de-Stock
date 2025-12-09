using Application.Services.Interfaces;
using Application.Services.Implementations;
using Data;
using Data.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer; 
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens; 
using Microsoft.OpenApi.Models;
using System.Text;
using WebAPI.Endpoints;

var builder = WebApplication.CreateBuilder(args);

// Entity Framework
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Dependency Injection - Repository Pattern
builder.Services.AddScoped<UsuarioRepository>();
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<IProductoRepository, ProductoRepository>();
builder.Services.AddScoped<ICategoriaRepository, CategoriaRepository>(); 
builder.Services.AddScoped<IEventoRepository, EventoRepository>();
builder.Services.AddScoped<IClienteRepository, ClienteRepository>();
builder.Services.AddScoped<IReservaRepository, ReservaRepository>();
builder.Services.AddScoped<IReportesRepository, ReporteRepository>();


// Dependency Injection - Application Services
builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<IUsuarioService, UsuarioService>();
builder.Services.AddScoped<IProductoService, ProductoService>();
builder.Services.AddScoped<ICategoriaService, CategoriaService>(); 
builder.Services.AddScoped<IEventoService, EventoService>();
builder.Services.AddScoped<IReservaService, ReservaService>();
builder.Services.AddScoped<IClienteService, ClienteService>();
builder.Services.AddScoped<IReportesService, ReporteService>();



// CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowWinForms", policy =>
    {
        policy.WithOrigins("*")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo 
    { 
        Title = "Sistema Control Stock API", 
        Version = "v1",
        Description = "API para gestión de stock, eventos y usuarios con Minimal API"
    });

    // Configurar JWT en Swagger
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header usando el esquema Bearer. Ejemplo: \"Bearer {token}\"",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

builder.Services.AddHttpLogging(o => { });
builder.Services.AddHealthChecks();

#region Configuración JWT

// 1. Agrega el servicio de Autenticación
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        // 2. Configura las opciones de validación del token
        options.TokenValidationParameters = new TokenValidationParameters
        {
            // Lee la configuración del appsettings.json
            ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
            ValidAudience = builder.Configuration["JwtSettings:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:SecretKey"]!)
            ),

            // 3. Le decimos que valide el Emisor, la Audiencia y la Clave
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true
        };
    });

// 4. Agrega el servicio de Autorización
builder.Services.AddAuthorization(options =>
{
    // Políticas para Eventos
    options.AddPolicy("EventosLeer", policy => policy.RequireClaim("permission", "eventos.leer"));
    options.AddPolicy("EventosAgregar", policy => policy.RequireClaim("permission", "eventos.agregar"));
    options.AddPolicy("EventosActualizar", policy => policy.RequireClaim("permission", "eventos.actualizar"));
    options.AddPolicy("EventosEliminar", policy => policy.RequireClaim("permission", "eventos.eliminar"));

    // Políticas para Categorías
    options.AddPolicy("CategoriasLeer", policy => policy.RequireClaim("permission", "categorias.leer"));
    options.AddPolicy("CategoriasAgregar", policy => policy.RequireClaim("permission", "categorias.agregar"));
    options.AddPolicy("CategoriasActualizar", policy => policy.RequireClaim("permission", "categorias.actualizar"));
    options.AddPolicy("CategoriasEliminar", policy => policy.RequireClaim("permission", "categorias.eliminar"));

    // Políticas para Clientes
    options.AddPolicy("ClientesLeer", policy => policy.RequireClaim("permission", "clientes.leer"));
    options.AddPolicy("ClientesAgregar", policy => policy.RequireClaim("permission", "clientes.agregar"));
    options.AddPolicy("ClientesActualizar", policy => policy.RequireClaim("permission", "clientes.actualizar"));
    options.AddPolicy("ClientesEliminar", policy => policy.RequireClaim("permission", "clientes.eliminar"));

    // Políticas para Reservas
    options.AddPolicy("ReservasLeer", policy => policy.RequireClaim("permission", "reservas.leer"));
    options.AddPolicy("ReservasAgregar", policy => policy.RequireClaim("permission", "reservas.agregar"));
    options.AddPolicy("ReservasActualizar", policy => policy.RequireClaim("permission", "reservas.actualizar"));
    options.AddPolicy("ReservasEliminar", policy => policy.RequireClaim("permission", "reservas.eliminar"));

    // Políticas para Usuarios
    options.AddPolicy("UsuariosLeer", policy => policy.RequireClaim("permission", "usuarios.leer"));
    options.AddPolicy("UsuariosAgregar", policy => policy.RequireClaim("permission", "usuarios.agregar"));
    options.AddPolicy("UsuariosActualizar", policy => policy.RequireClaim("permission", "usuarios.actualizar"));
    options.AddPolicy("UsuariosEliminar", policy => policy.RequireClaim("permission", "usuarios.eliminar"));

    // Políticas para Productos
    options.AddPolicy("ProductosLeer", policy => policy.RequireClaim("permission", "productos.leer"));
    options.AddPolicy("ProductosAgregar", policy => policy.RequireClaim("permission", "productos.agregar"));
    options.AddPolicy("ProductosActualizar", policy => policy.RequireClaim("permission", "productos.actualizar"));
    options.AddPolicy("ProductosEliminar", policy => policy.RequireClaim("permission", "productos.eliminar"));

    // Politicas para ReservaProducto
    options.AddPolicy("ReservaProductoLeer", policy => policy.RequireClaim("permission", "reservaproducto.leer"));
    options.AddPolicy("ReservaProductoAgregar", policy => policy.RequireClaim("permission", "reservaproducto.agregar"));
    options.AddPolicy("ReservaProductoActualizar", policy => policy.RequireClaim("permission", "reservaproducto.actualizar"));
    options.AddPolicy("ReservaProductoEliminar", policy => policy.RequireClaim("permission", "reservaproducto.eliminar"));


    // Fallback: Requerir autenticación para endpoints no especificados
    options.FallbackPolicy = options.DefaultPolicy;




});

#endregion

var app = builder.Build();



// Ensure database is created - RECREAR SI HAY CAMBIOS EN EL MODELO
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    
    // Borrar y recrear (SOLO EN DESARROLLO) 
    // DESCOMENTAR SOLO CUANDO NECESITES RECREAR LA BD CON DATOS INICIALES
    if (app.Environment.IsDevelopment())
    {
        await context.Database.EnsureDeletedAsync();  // ?? BORRA LA BD
        await context.Database.EnsureCreatedAsync();  // ? CREA NUEVA
    }
    
    // Solo asegurar que exista (sin borrar)
    await context.Database.EnsureCreatedAsync();
}

// Configure the HTTP request pipeline.

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Sistema Control Stock API v1");
        c.RoutePrefix = "swagger";
    });
}
else
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}


app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseCors("AllowWinForms");
app.UseAuthentication();
app.UseAuthorization();



app.MapAuthEndpoints();
app.MapUsuariosEndpoints();
app.MapEventosEndpoints();
app.MapProductosEndpoints();
app.MapCategoriasEndpoints();
app.MapReservasEndpoints();
app.MapClientesEndpoints();
app.MapReportesEndpoints();

// Health Check
app.MapHealthChecks("/api/health");

app.Run();
