# Sistema de Gestión de Productos y Categorías

## Descripción

GestionProductosAPI es una API REST desarrollada con ASP.NET Core Web API que permite administrar productos y categorías. El sistema utiliza una base de datos SQL Server para almacenar la información y Entity Framework Core para realizar las operaciones sobre la base de datos.

## Tecnologías utilizadas

* C#
* ASP.NET Core Web API (.NET 8)
* Entity Framework Core
* SQL Server
* Swagger

## Funcionalidades

### Productos

* Registrar productos.
* Consultar todos los productos.
* Consultar un producto por su ID.
* Actualizar productos.
* Eliminar productos.
* Buscar productos por nombre.
* Filtrar productos por categoría.
* Consultar productos con pocas existencias (5 unidades o menos).

### Categorías

* Registrar categorías.
* Consultar todas las categorías.
* Consultar una categoría por su ID.
* Actualizar categorías.
* Eliminar categorías.

## Base de datos

El sistema utiliza SQL Server y cuenta con dos entidades relacionadas:

* **Producto:** almacena el nombre, el precio, las existencias y la categoría a la que pertenece.
* **Categoria:** almacena el nombre y la descripción de cada categoría.

La relación entre ambas entidades permite asociar varios productos a una categoría.

## Estructura del proyecto

```text
GestionProductosAPI/
├── Controllers/
│   ├── CategoriasController.cs
│   └── ProductosController.cs
├── Data/
│   └── AppDbContext.cs
├── DTOs/
│   ├── CategoriaDTO.cs
│   ├── CrearCategoriaDTO.cs
│   ├── ProductoDTO.cs
│   └── CrearProductoDTO.cs
├── Models/
│   ├── Categoria.cs
│   └── Producto.cs
├── Migrations/
├── appsettings.json
├── Program.cs
└── GestionProductosAPI.csproj
```

## Cómo ejecutar el proyecto

1. Clonar o descargar el repositorio.
2. Abrir el proyecto en Visual Studio.
3. Verificar que SQL Server LocalDB esté instalado y disponible.
4. Revisar la cadena de conexión configurada en `appsettings.json`.
5. Aplicar las migraciones de Entity Framework Core para crear o actualizar la base de datos.
6. Ejecutar el proyecto desde Visual Studio.
7. Utilizar Swagger para consultar y probar los endpoints de la API.


Proyecto académico desarrollado como práctica de ASP.NET Core Web API, operaciones CRUD, Entity Framework Core y conexión a SQL Server.
Proyecto creado por Reselin Amaro Acevedo
