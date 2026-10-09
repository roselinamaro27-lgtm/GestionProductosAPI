using GestionProductosAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace GestionProductosAPI.Data
{
	public class AppDbContext : DbContext
	{
		public AppDbContext(DbContextOptions<AppDbContext> options)
		: base(options)
		{
		}


    public DbSet<Producto> Productos { get; set; }

		public DbSet<Categoria> Categorias { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.Entity<Producto>()
				.Property(p => p.Precio)
				.HasPrecision(18, 2);
		}
	}


}
