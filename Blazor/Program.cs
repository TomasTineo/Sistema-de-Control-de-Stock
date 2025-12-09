using Blazor.Components;
using Blazor.Services;

var builder = WebApplication.CreateBuilder(args);

// Agregar servicios
builder.Services.AddRazorComponents()  // ← NUEVA forma en .NET 8+
    .AddInteractiveServerComponents();  // ← Para Blazor Server

// Configurar HttpClient para tu API
builder.Services.AddHttpClient("AuthAPI", client =>
{
    client.BaseAddress = new Uri("http://localhost:5239/");
});

// Servicios 
builder.Services.AddScoped<IProductoService, ProductoService>();
builder.Services.AddScoped<ICategoriaService, CategoriaService>();
builder.Services.AddScoped<IEventoService, EventoService>();
builder.Services.AddScoped<IClienteService, ClienteService>();
builder.Services.AddScoped<IReservaService, ReservaService>();


builder.Services.AddScoped<ITokenStorage, ServerTokenStorage>();
builder.Services.AddScoped<IAuthService, AuthService>();

// Configurar autorización si usas JWT
builder.Services.AddAuthorizationCore();
// Si usas autenticación, agrega también:
// builder.Services.AddAuthentication();

var app = builder.Build();

// Configurar pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAntiforgery();

// Configurar routing de Blazor 
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();  

app.Run();