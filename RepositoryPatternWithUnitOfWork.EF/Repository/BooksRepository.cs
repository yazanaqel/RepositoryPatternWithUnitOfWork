using Microsoft.EntityFrameworkCore;
using RepositoryPatternWithUnitOfWork.Core.Models;
using RepositoryPatternWithUnitOfWork.Core.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryPatternWithUnitOfWork.EF.Repository;
public class BooksRepository : GenericRepository<Book>, IBooksRepository
{
	private readonly ApplicationDbContext _context;

	public BooksRepository(ApplicationDbContext context) : base(context)
	{
	}

	public async Task<IEnumerable<Book>> GetAllBooks() =>
	await _dbContext.Books.ToListAsync();
}
