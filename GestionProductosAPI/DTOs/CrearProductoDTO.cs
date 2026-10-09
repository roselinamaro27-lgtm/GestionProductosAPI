
using System.ComponentModel.DataAnnotations;

namespace GestionProductosAPI.DTOs
{
	public class CrearProductoDTO
	{
		[Required(ErrorMessage = "El nombre es obligatorio.")]
		[StringLength(100, MinimumLength = 2, ErrorMessage = "El nombre debe tener entre 2 y 100 caracteres.")]
		public string Nombre { get; set; } = string.Empty;

		[Range(typeof(decimal), "0.01", "999999999.99", ErrorMessage = "El precio debe ser mayor que cero.")]
		public decimal Precio { get; set; }

		[Range(0, int.MaxValue, ErrorMessage = "Las existencias no pueden ser negativas.")]
		public int Existencias { get; set; }

		[Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar una categoría válida.")]
		public int CategoriaId { get; set; }
	}
}

