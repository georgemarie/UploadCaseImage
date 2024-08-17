using System.Collections;
using System.Data;
using Microsoft.EntityFrameworkCore;
using UploadingCaseImages.DB;
using UploadingCaseImages.Repository;

namespace UploadingCaseImages.UnitOfWorks;

public class UnitOfWork : IUnitOfWork
{
	private readonly UploadingCaseImagesContext _context;
	private Hashtable _repositories;

	public UnitOfWork(UploadingCaseImagesContext context)
	{
		_context = context;
	}

	public async Task<int> SaveChanges()
	{
		return await _context.SaveChangesAsync();
	}

	public void Dispose()
	{
		_context.Dispose();
	}

	public IGenericRepository<TEntity> Repository<TEntity>()
		where TEntity : class
	{
		_repositories ??= [];

		string typeName = typeof(TEntity).Name;

		if (!_repositories.ContainsKey(typeName))
		{
			Type repoType = typeof(GenericRepository<>);
			object repoInstance = Activator.CreateInstance(repoType.MakeGenericType(typeof(TEntity)), _context);
			_repositories.Add(typeName, repoInstance);
		}

		return (IGenericRepository<TEntity>)_repositories[typeName];
	}

	public async Task BeginTransactionAsync(IsolationLevel isolationLevel)
	{
		await _context.Database.BeginTransactionAsync(isolationLevel);
	}

	public async Task CommitTransactionAsync()
	{
		await _context.Database.CommitTransactionAsync();
	}

	public async Task RollbackTransactionAsync()
	{
		await _context.Database.RollbackTransactionAsync();
	}
}
