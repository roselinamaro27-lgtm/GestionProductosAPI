using GestionProductosAPI.Data;
using GestionProductosAPI.DTOs;
using GestionProductosAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GestionProductosAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CategoriasController : ControllerBase
	{
		private readonly AppDbContext _context;

		public CategoriasController(AppDbContext context)
		{
			_context = context;
		}

		[HttpGet]
		public async Task<ActionResult<IEnumerable<CategoriaDTO>>> ObtenerCategorias()
		{
			var categorias = await _context.Categorias
				.Select(c => new CategoriaDTO
				{
					Id = c.Id,
					Nombre = c.Nombre,
					Descripcion = c.Descripcion
				})
				.ToListAsync();

			return Ok(categorias);
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<CategoriaDTO>> ObtenerCategoria(int id)
		{
			var categoria = await _context.Categorias
				.Where(c => c.Id == id)
				.Select(c => new CategoriaDTO
				{
					Id = c.Id,
					Nombre = c.Nombre,
					Descripcion = c.Descripcion
				})
				.FirstOrDefaultAsync();

			if (categoria == null)
			{
				return NotFound();
			}

			return Ok(categoria);
		}

		[HttpPost]
		public async Task<ActionResult<CategoriaDTO>> CrearCategoria(CrearCategoriaDTO dto)
		{
			var categoria = new Categoria
			{
				Nombre = dto.Nombre,
				Descripcion = dto.Descripcion
			};

			_context.Categorias.Add(categoria);

			await _context.SaveChangesAsync();

			var categoriaDTO = new CategoriaDTO
			{
				Id = categoria.Id,
				Nombre = categoria.Nombre,
				Descripcion = categoria.Descripcion
			};

			return CreatedAtAction(
				nameof(ObtenerCategoria),
				new { id = categoria.Id },
				categoriaDTO);
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> ActualizarCategoria(int id, CrearCategoriaDTO dto)
		{
			var categoria = await _context.Categorias.FindAsync(id);

			if (categoria == null)
			{
				return NotFound();
			}

			categoria.Nombre = dto.Nombre;
			categoria.Descripcion = dto.Descripcion;

			await _context.SaveChangesAsync();

			return NoContent();
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> EliminarCategoria(int id)
		{
			var categoria = await _context.Categorias.FindAsync(id);

			if (categoria == null)
			{
				return NotFound();
			}

			_context.Categorias.Remove(categoria);

			await _context.SaveChangesAsync();

			return NoContent();
		}
	}
}
