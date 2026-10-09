using GestionProductosAPI.Data;
using GestionProductosAPI.DTOs;
using GestionProductosAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GestionProductosAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ProductosController : ControllerBase
	{
		private readonly AppDbContext _context;

		public ProductosController(AppDbContext context)
		{
			_context = context;
		}

		[HttpGet]
		public async Task<ActionResult<IEnumerable<ProductoDTO>>> ObtenerProductos(
			[FromQuery] string? nombre,
			[FromQuery] int? categoriaId)
		{
			var consulta = _context.Productos.AsQueryable();

			if (!string.IsNullOrWhiteSpace(nombre))
			{
				consulta = consulta.Where(p => p.Nombre.Contains(nombre));
			}

			if (categoriaId.HasValue)
			{
				consulta = consulta.Where(p => p.CategoriaId == categoriaId.Value);
			}

			var productos = await consulta
				.Select(p => new ProductoDTO
				{
					Id = p.Id,
					Nombre = p.Nombre,
					Precio = p.Precio,
					Existencias = p.Existencias,
					CategoriaId = p.CategoriaId,
					NombreCategoria = p.Categoria!.Nombre
				})
				.ToListAsync();

			return Ok(productos);
		}

		[HttpGet("{id:int}")]
		public async Task<ActionResult<ProductoDTO>> ObtenerProducto(int id)
		{
			var producto = await _context.Productos
				.Where(p => p.Id == id)
				.Select(p => new ProductoDTO
				{
					Id = p.Id,
					Nombre = p.Nombre,
					Precio = p.Precio,
					Existencias = p.Existencias,
					CategoriaId = p.CategoriaId,
					NombreCategoria = p.Categoria!.Nombre
				})
				.FirstOrDefaultAsync();

			if (producto == null)
			{
				return NotFound();
			}

			return Ok(producto);
		}

		[HttpGet("pocas-existencias")]
		public async Task<ActionResult<IEnumerable<ProductoDTO>>> ObtenerProductosConPocasExistencias()
		{
			var productos = await _context.Productos
				.Where(p => p.Existencias <= 5)
				.Select(p => new ProductoDTO
				{
					Id = p.Id,
					Nombre = p.Nombre,
					Precio = p.Precio,
					Existencias = p.Existencias,
					CategoriaId = p.CategoriaId,
					NombreCategoria = p.Categoria!.Nombre
				})
				.ToListAsync();

			return Ok(productos);
		}

		[HttpPost]
		public async Task<ActionResult<ProductoDTO>> CrearProducto(CrearProductoDTO dto)
		{
			var categoriaExiste = await _context.Categorias
				.AnyAsync(c => c.Id == dto.CategoriaId);

			if (!categoriaExiste)
			{
				return BadRequest("La categoría indicada no existe.");
			}

			var producto = new Producto
			{
				Nombre = dto.Nombre,
				Precio = dto.Precio,
				Existencias = dto.Existencias,
				CategoriaId = dto.CategoriaId
			};

			_context.Productos.Add(producto);
			await _context.SaveChangesAsync();

			var productoDTO = await _context.Productos
				.Where(p => p.Id == producto.Id)
				.Select(p => new ProductoDTO
				{
					Id = p.Id,
					Nombre = p.Nombre,
					Precio = p.Precio,
					Existencias = p.Existencias,
					CategoriaId = p.CategoriaId,
					NombreCategoria = p.Categoria!.Nombre
				})
				.FirstAsync();

			return CreatedAtAction(
				nameof(ObtenerProducto),
				new { id = producto.Id },
				productoDTO);
		}

		[HttpPut("{id:int}")]
		public async Task<IActionResult> ActualizarProducto(int id, CrearProductoDTO dto)
		{
			var producto = await _context.Productos.FindAsync(id);

			if (producto == null)
			{
				return NotFound();
			}

			var categoriaExiste = await _context.Categorias
				.AnyAsync(c => c.Id == dto.CategoriaId);

			if (!categoriaExiste)
			{
				return BadRequest("La categoría indicada no existe.");
			}

			producto.Nombre = dto.Nombre;
			producto.Precio = dto.Precio;
			producto.Existencias = dto.Existencias;
			producto.CategoriaId = dto.CategoriaId;

			await _context.SaveChangesAsync();

			return NoContent();
		}

		[HttpDelete("{id:int}")]
		public async Task<IActionResult> EliminarProducto(int id)
		{
			var producto = await _context.Productos.FindAsync(id);

			if (producto == null)
			{
				return NotFound();
			}

			_context.Productos.Remove(producto);
			await _context.SaveChangesAsync();

			return NoContent();
		}
	}
}