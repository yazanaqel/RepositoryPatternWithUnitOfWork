using RepositoryPatternWithUnitOfWork.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryPatternWithUnitOfWork.Core.Repository;
public interface IUnitOfWork : IDisposable
{
	IGenericRepository<Author> Authors { get; }
	IBooksRepository Books { get; }

	Task<int> Complete();
}
