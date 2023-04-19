using filmsystemet.Data;
using filmsystemet.Models;

namespace filmsystemet.RepositoryPattern
{
		public class FavouriteGenreRepository : RepositoryBase<FavouriteGenre>, IFavouriteGenreRepository
		{
			public FavouriteGenreRepository(MovieSystemDbContext movieSystemDbContext)
				: base(movieSystemDbContext)
			{
			}
		}
	}
