using Application.Services.Implementations;
using DTOs.Auth;
using System.Security.Claims;

namespace WebAPI.Endpoints
{
    public static class AuthEndpoints
    {
        public static void MapAuthEndpoints(this WebApplication app)
        {
            app.MapPost("/auth/login", async (AuthService authService, LoginRequest request) =>
            {
                try
                {
                    var response = await authService.LoginAsync(request);

                    if (response == null)
                    {
                        return Results.Unauthorized();
                    }

                    return Results.Ok(response);
                }
                catch (Exception ex)
                {
                    return Results.Problem($"Error durante el login: {ex.Message}");
                }
            })
            .WithName("Login")
            .Produces<LoginResponse>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status500InternalServerError)
            .AllowAnonymous();

            // Endpoint de prueba para verificar autenticación
            app.MapGet("/auth/test", (ClaimsPrincipal user) =>
            {
                if (!user.Identity?.IsAuthenticated ?? true)
                {
                    return Results.Json(new { 
                        authenticated = false, 
                        message = "Usuario no autenticado" 
                    });
                }

                var claims = user.Claims.Select(c => new { 
                    type = c.Type, 
                    value = c.Value 
                }).ToList();

                var permissions = user.Claims
                    .Where(c => c.Type == "permission")
                    .Select(c => c.Value)
                    .ToList();

                return Results.Json(new
                {
                    authenticated = true,
                    username = user.Identity.Name,
                    userId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value,
                    email = user.FindFirst(ClaimTypes.Email)?.Value,
                    group = user.FindFirst("group")?.Value,
                    permissionsCount = permissions.Count,
                    permissions = permissions,
                    allClaims = claims
                });
            })
            .WithName("TestAuth")
            .RequireAuthorization()
            .Produces<object>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status401Unauthorized);
        }
    }
}
