
namespace GestionProductosAPI.DTOs
{
	public class ProductoDTO
	{
		public int Id { get; set; }

		public string Nombre { get; set; } = string.Empty;

		public decimal Precio { get; set; }

		public int Existencias { get; set; }

		public int CategoriaId { get; set; }

		public string NombreCategoria { get; set; } = string.Empty;
	}
}

