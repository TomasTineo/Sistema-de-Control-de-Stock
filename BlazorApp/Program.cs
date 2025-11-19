using Application.Services.Implementations;
using Application.Services.Interfaces;
using BlazorApp.Components;
using Data;
using Data.Repositories;
using Data.Repositories.Implementations;
using Microsoft.EntityFrameworkCore;



var builder = WebApplication.CreateBuilder(args);



// Configurar HttpClient para consumir la Web API
builder.Services.AddHttpClient("WebAPI", client =>
{
    client.BaseAddress = new Uri("https://localhost:7001/ ");
});



// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();


var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
