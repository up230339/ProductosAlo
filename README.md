# API de Productos (.NET 8 + SQL Server)

## Requisitos
- .NET 8 SDK
- SQL Server (en este proyecto se usa la instancia `SQLEXPRESS`)
- Visual Studio o VS Code

## 1. Crear la base de datos
Abre SQL Server Management Studio (SSMS), conéctate a `DESKTOP-B9LI1J5\\SQLEXPRESS` y ejecuta el archivo `Database/crear_base_datos.sql`.

Si utilizas otra instancia, cambia `ConnectionStrings:DefaultConnection` en `appsettings.json`.

## 2. Ejecutar la API
Desde la carpeta del proyecto:

```bash
dotnet restore
dotnet run
```

Abre la dirección HTTPS/HTTP que aparezca en la terminal y agrega `/swagger`.

## 3. Operaciones disponibles
- `GET /api/productos` - Obtener todos los productos.
- `GET /api/productos/{id}` - Buscar un producto por ID.
- `POST /api/productos` - Insertar un producto.
- `PUT /api/productos/{id}` - Actualizar un producto.
- `DELETE /api/productos/{id}` - Eliminar un producto.

Ejemplo de JSON para POST y PUT:

```json
{
  "nombre": "Audífonos",
  "descripcion": "Audífonos inalámbricos",
  "precio": 499.90
}
```

## Evidencias sugeridas
Toma capturas de:
1. La tabla `Productos` en SQL Server con los registros de prueba.
2. Swagger mostrando todos los endpoints.
3. Una prueba exitosa de POST, GET por ID, PUT y DELETE en Swagger.
4. GET general después de insertar/actualizar/eliminar para comprobar los cambios.

## Nota
El repositorio utiliza consultas SQL parametrizadas para evitar concatenar directamente los datos recibidos en las instrucciones SQL.
