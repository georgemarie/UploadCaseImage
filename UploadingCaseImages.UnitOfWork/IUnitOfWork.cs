using System.Data;
using UploadingCaseImages.Repository;

namespace UploadingCaseImages.UnitOfWorks;

public interface IUnitOfWork : IDisposable
{
	Task<int> SaveChanges();

	IGenericRepository<TEntity> Repository<TEntity>()
		where TEntity : class;

	Task BeginTransactionAsync(IsolationLevel isolationLevel);

	Task CommitTransactionAsync();

	Task RollbackTransactionAsync();
}
