using Blazor.Components;
using Blazor.Services;

var builder = WebApplication.CreateBuilder(args);

// 1. Agregar servicios
builder.Services.AddRazorComponents()  // ← NUEVA forma en .NET 8+
    .AddInteractiveServerComponents();  // ← Para Blazor Server

// 2. Configurar HttpClient para tu API
builder.Services.AddHttpClient("AuthAPI", client =>
{
    client.BaseAddress = new Uri("https://localhost:7001/");
});

// 3. Tus servicios personalizados
builder.Services.AddScoped<ITokenStorage, ServerTokenStorage>();
builder.Services.AddScoped<IAuthService, AuthService>();

var app = builder.Build();

// 4. Configurar pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAntiforgery();

// 5. Configurar routing de Blazor (¡CORRECTO!)
app.MapRazorComponents<App>()  // ← App.razor es tu componente raíz
    .AddInteractiveServerRenderMode();  // ← Modo Server

app.Run();