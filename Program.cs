using ProductosApi;
using ProductosApi.Data;
using ProductosApi.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<SqlConnectionFactory>();
builder.Services.AddScoped<IProductoRepository, ProductoRepository>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("/", () => Results.Ok("API de productos funcionando"));

var productos = app.MapGroup("/api/productos").WithTags("Productos");

productos.MapGet("/", (IProductoRepository repo) =>
    Results.Ok(repo.ObtenerTodos()));

productos.MapGet("/{id:int}", (int id, IProductoRepository repo) =>
{
    var producto = repo.ObtenerPorId(id);
    return producto is null ? Results.NotFound(new { mensaje = "Producto no encontrado" }) : Results.Ok(producto);
});

productos.MapPost("/", (Producto producto, IProductoRepository repo) =>
{
    if (string.IsNullOrWhiteSpace(producto.Nombre) || producto.Precio < 0)
        return Results.BadRequest(new { mensaje = "El nombre es obligatorio y el precio no puede ser negativo." });

    var id = repo.Insertar(producto);
    return Results.Created($"/api/productos/{id}", new { id, mensaje = "Producto creado correctamente" });
});

productos.MapPut("/{id:int}", (int id, Producto producto, IProductoRepository repo) =>
{
    producto.Id = id;
    if (string.IsNullOrWhiteSpace(producto.Nombre) || producto.Precio < 0)
        return Results.BadRequest(new { mensaje = "El nombre es obligatorio y el precio no puede ser negativo." });

    return repo.Actualizar(producto)
        ? Results.Ok(new { mensaje = "Producto actualizado correctamente" })
        : Results.NotFound(new { mensaje = "Producto no encontrado" });
});

productos.MapDelete("/{id:int}", (int id, IProductoRepository repo) =>
    repo.Eliminar(id)
        ? Results.Ok(new { mensaje = "Producto eliminado correctamente" })
        : Results.NotFound(new { mensaje = "Producto no encontrado" }));

app.Run();
