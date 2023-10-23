using RepositoryPatternWithUnitOfWork.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryPatternWithUnitOfWork.Core.Repository;
public interface IBooksRepository : IGenericRepository<Book>
{
	Task<IEnumerable<Book>> GetAllBooks();
}
