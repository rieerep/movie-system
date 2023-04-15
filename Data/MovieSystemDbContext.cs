using filmsystemet.Models;
using Microsoft.EntityFrameworkCore;

namespace filmsystemet.Data
{
	internal class MovieSystemDbContext : DbContext // DbContext
	{
		public DbSet<FavouriteGenre> FavouriteGenres { get; set; } = null!;
		public DbSet<Genre> Genres { get; set; } = null!;
		public DbSet<Person> Persons { get; set; } = null!;
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			// optionsBuilder.UseSqlServer(@"Data Source=(localdb)\Jupiter;Initial Catalog=ContosoPizza-Part1;Integrated Security=True;");
			optionsBuilder.UseSqlServer(@"Data Source=Jupiter;Initial Catalog=MovieSystemDb;Integrated Security=True;");
		}
	}
}