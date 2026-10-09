using System.ComponentModel.DataAnnotations;

namespace GestionProductosAPI.DTOs
{
	public class CrearCategoriaDTO
	{
		[Required(ErrorMessage = "El nombre de la categoría es obligatorio.")]
		[StringLength(100, MinimumLength = 2, ErrorMessage = "El nombre debe tener entre 2 y 100 caracteres.")]
		public string Nombre { get; set; } = string.Empty;

		[StringLength(250, ErrorMessage = "La descripción no puede superar los 250 caracteres.")]
		public string Descripcion { get; set; } = string.Empty;
	}
}