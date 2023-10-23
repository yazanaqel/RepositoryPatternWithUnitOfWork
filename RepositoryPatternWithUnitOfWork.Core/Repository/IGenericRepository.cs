using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryPatternWithUnitOfWork.Core.Repository;
public interface IGenericRepository<T> where T : class
{
	Task<T> AddAsync(T entity);
	Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities);
	T Update(T entity);
	void Delete(T entity);
	void DeleteRange(IEnumerable<T> entities);
	void Attach(T entity);
	int Count();
	int Count(Expression<Func<T, bool>> criteria);
	Task<T> GetByIdAsync(int id);
	Task<IEnumerable<T>> GetAllAsync();
	Task<T> FindAsync(Expression<Func<T, bool>> criteria, string[] includes = null);
	Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> criteria, string[] includes = null);
	Task<IEnumerable<T>> FindAllAsync
		(Expression<Func<T, bool>> criteria, int? skip, int? take,
		Expression<Func<T, object>> orderBy = null, string orderByDirection = "ASC");
}
