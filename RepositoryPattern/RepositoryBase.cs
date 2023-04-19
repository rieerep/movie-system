using filmsystemet.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace filmsystemet.RepositoryPattern
{
	public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
	{
		protected MovieSystemDbContext MovieSystemDbContext { get; set; }
		public RepositoryBase(MovieSystemDbContext movieSystemDbContext)
		{
			MovieSystemDbContext = movieSystemDbContext;
		}
		public IQueryable<T> GetAll() => MovieSystemDbContext.Set<T>().AsNoTracking();
		public IQueryable<T> GetByCondition(Expression<Func<T, bool>> expression) =>
			MovieSystemDbContext.Set<T>().Where(expression).AsNoTracking();
		public void Create(T entity) => MovieSystemDbContext.Set<T>().Add(entity);
		public void Update(T entity) => MovieSystemDbContext.Set<T>().Update(entity);
		public void Delete(T entity) => MovieSystemDbContext.Set<T>().Remove(entity);
	}
}
