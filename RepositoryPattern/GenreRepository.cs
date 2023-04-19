using filmsystemet.Data;
using filmsystemet.Models;
using static filmsystemet.RepositoryPattern.GenreRepository;

namespace filmsystemet.RepositoryPattern
{

		public class GenreRepository : RepositoryBase<Genre>, IGenreRepository
		{
			public GenreRepository(MovieSystemDbContext movieSystemDbContext)
				: base(movieSystemDbContext)
			{
			}
		}
}
