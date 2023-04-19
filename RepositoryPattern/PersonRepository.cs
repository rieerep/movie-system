using filmsystemet.Data;
using filmsystemet.Models;

namespace filmsystemet.RepositoryPattern
{
	public class PersonRepository : RepositoryBase<Person>, IPersonRepository
	{
		public PersonRepository(MovieSystemDbContext repositoryContext)
			: base(repositoryContext)
		{
		}
	}
}
