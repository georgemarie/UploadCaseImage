using System.Data;
using UploadingCaseImages.DB.Model;
using UploadingCaseImages.Repository;

namespace UploadingCaseImages.UnitOfWorks;

public interface IUnitOfWork : IDisposable
{
	Task<int> SaveChanges();

	IGenericRepository<TEntity> Repository<TEntity>()
		where TEntity : BaseEntity;

	Task BeginTransactionAsync(IsolationLevel isolationLevel);

	Task CommitTransactionAsync();

	Task RollbackTransactionAsync();
}
