using Microsoft.EntityFrameworkCore;
using RepositoryPatternWithUnitOfWork.Core.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryPatternWithUnitOfWork.EF.Repository;
public class GenericRepository<T> : IGenericRepository<T> where T : class
{
	protected ApplicationDbContext _dbContext;

	public GenericRepository(ApplicationDbContext dbContext)
	{
		_dbContext = dbContext;
	}
	public async Task<T> GetByIdAsync(int id) =>
		await _dbContext.Set<T>().FindAsync(id);

	public async Task<IEnumerable<T>> GetAllAsync() =>
		await _dbContext.Set<T>().ToListAsync();

	public async Task<T> FindAsync(Expression<Func<T, bool>> criteria, string[] includes = null)
	{
		IQueryable<T> values = _dbContext.Set<T>();

		if (includes is not null)
			foreach (var include in includes)
				values = values.Include(include);

		return await values.SingleOrDefaultAsync(criteria);
	}

	public async Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> criteria, string[] includes = null)
	{
		IQueryable<T> values = _dbContext.Set<T>();

		if (includes is not null)
			foreach (var include in includes)
				values = values.Include(include);

		return await values.Where(criteria).ToListAsync();
	}

	public async Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> criteria, int? skip, int? take,
		Expression<Func<T, object>> orderBy = null, string orderByDirection = "ASC")
	{
		IQueryable<T> values = _dbContext.Set<T>().Where(criteria);

		if (take.HasValue)
			values.Take(take.Value);

		if (skip.HasValue)
			values.Skip(skip.Value);

		if (orderBy is not null)
		{
			if (string.Equals(orderByDirection, "ASC"))
			{
				values = values.OrderBy(orderBy);
			}
			else
			{
				values = values.OrderByDescending(orderBy);
			}
		}

		return await values.ToListAsync();
	}

	public async Task<T> AddAsync(T entity)
	{
		await _dbContext.Set<T>().AddAsync(entity);
		return entity;
	}

	public async Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities)
	{
		await _dbContext.Set<T>().AddRangeAsync(entities);
		return entities;
	}

	public T Update(T entity)
	{
		_dbContext.Set<T>().Update(entity);
		return entity;
	}

	public void Delete(T entity)
	{
		_dbContext.Set<T>().Remove(entity);
	}
	public void DeleteRange(IEnumerable<T> entities)
	{
		_dbContext.Set<T>().RemoveRange(entities);
	}

	public void Attach(T entity)
	{
		_dbContext.Set<T>().Attach(entity);
	}

	public int Count() => _dbContext.Set<T>().Count();


	public int Count(Expression<Func<T, bool>> criteria) => _dbContext.Set<T>().Count(criteria);
}
