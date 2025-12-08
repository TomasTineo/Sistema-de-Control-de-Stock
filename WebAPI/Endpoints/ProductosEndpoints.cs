using Application.Services.Interfaces;
using DTOs.Productos;

namespace WebAPI.Endpoints
{
    public static class ProductosEndpoints
    {
        public static void MapProductosEndpoints(this WebApplication app)
        {
            var productos = app.MapGroup("/api/productos")
                .WithTags("Productos");

            // GET /api/productos - Requiere permiso de lectura
            productos.MapGet("/", async (IProductoService productoService) =>
            {
                var productosLista = await productoService.GetAllAsync();
                return Results.Ok(productosLista);
            })
            .WithName("GetProductos")
            .RequireAuthorization("ProductosLeer")
            .Produces<IEnumerable<ProductoDTO>>(StatusCodes.Status200OK);



            // GET /api/productos/{id} - Requiere permiso de lectura
            productos.MapGet("/{id:int}", async (int id, IProductoService productoService) =>
            {
                var producto = await productoService.GetAsync(id);
                return producto == null ? Results.NotFound() : Results.Ok(producto);
            })
            .WithName("GetProducto")
            .RequireAuthorization("ProductosLeer")
            .Produces<ProductoDTO>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound);


            // POST /api/productos - Requiere permiso de agregar
            productos.MapPost("/", async (CreateProductoRequest request, IProductoService productoService) =>
            {
                try
                {
                    var producto = await productoService.CreateAsync(request);
                    return Results.CreatedAtRoute("GetProducto", new { id = producto.Id }, producto);
                }
                catch (Exception ex)
                {
                    return Results.BadRequest(ex.Message);
                }
            })
            .WithName("CreateProducto")
            .RequireAuthorization("ProductosAgregar")
            .Produces<ProductoDTO>(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status400BadRequest);


            // PUT /api/productos/{id} - Requiere permiso de actualizar
            productos.MapPut("/{id:int}", async (int id, ProductoDTO request, IProductoService productoService) =>
            {
                if (id != request.Id)
                    return Results.BadRequest("ID mismatch");

                try
                {
                    var result = await productoService.UpdateAsync(request);
                    return result ? Results.NoContent() : Results.NotFound();
                }
                catch (Exception ex)
                {
                    return Results.BadRequest(ex.Message);
                }
            })
            .WithName("UpdateProducto")
            .RequireAuthorization("ProductosActualizar")
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status404NotFound);


            // DELETE /api/productos/{id} - Requiere permiso de eliminar
            productos.MapDelete("/{id:int}", async (int id, IProductoService productoService) =>
            {
                var result = await productoService.DeleteAsync(id);
                return result ? Results.NoContent() : Results.NotFound();
            })
            .WithName("DeleteProducto")
            .RequireAuthorization("ProductosEliminar")
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status404NotFound);


            // GET /api/productos/categoria/{categoriaId} - Requiere permiso de lectura
            productos.MapGet("/categoria/{categoriaId:int}", async (int categoriaId, IProductoService productoService) =>
            {
                var productosPorCategoria = await productoService.GetByCategoriaAsync(categoriaId);
                return Results.Ok(productosPorCategoria);
            })
            .WithName("GetProductosByCategoria")
            .RequireAuthorization("ProductosLeer")
            .Produces<IEnumerable<ProductoDTO>>(StatusCodes.Status200OK);
        }
    }
}
