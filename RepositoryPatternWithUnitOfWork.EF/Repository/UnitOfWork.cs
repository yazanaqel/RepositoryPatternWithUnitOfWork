using RepositoryPatternWithUnitOfWork.Core.Models;
using RepositoryPatternWithUnitOfWork.Core.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryPatternWithUnitOfWork.EF.Repository;
public class UnitOfWork : IUnitOfWork
{
	private readonly ApplicationDbContext _dbContext;

	public UnitOfWork(ApplicationDbContext dbContext)
    {
		_dbContext = dbContext;

		Authors = new GenericRepository<Author>(_dbContext);

		Books = new BooksRepository(_dbContext);
	}
    public IGenericRepository<Author> Authors { get; private set; }

	public IBooksRepository Books { get; private set; }

	public async Task<int> Complete()
	{
		return await _dbContext.SaveChangesAsync();
	}

	public void Dispose()
	{
		_dbContext.Dispose();
	}
}
