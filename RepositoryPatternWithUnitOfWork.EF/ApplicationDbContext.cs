using Microsoft.EntityFrameworkCore;
using RepositoryPatternWithUnitOfWork.Core.Models;

namespace RepositoryPatternWithUnitOfWork.EF;

public class ApplicationDbContext : DbContext
{
	public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
	{
	}

	public DbSet<Author>Authors { get; set; }
	public DbSet<Book> Books { get; set; }
}
