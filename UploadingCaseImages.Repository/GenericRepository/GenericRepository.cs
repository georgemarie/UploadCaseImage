using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using UploadingCaseImages.DB;
using UploadingCaseImages.DB.Model;

namespace UploadingCaseImages.Repository
{
	public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
	{
		protected readonly UploadingCaseImagesContext _context;
		private readonly DbSet<TEntity> _dbSet;

		public GenericRepository(UploadingCaseImagesContext context)
		{
			_context = context;
			_dbSet = _context.Set<TEntity>();
		}

		public async Task<IEnumerable<TEntity>> GetAll()
		{
			return await _dbSet.ToListAsync(); // Ensure this is async
		}

		public async Task<TEntity> GetById(int id)
		{
			return await _dbSet.FindAsync(id); // Ensure this is async and returns Task<TEntity>
		}

		public async Task<List<TEntity>> GetByIds(Expression<Func<TEntity, bool>> wherePredicate, int[] ids)
		{
			var idName = _context.Model.FindEntityType(typeof(TEntity))
				.FindPrimaryKey().Properties.Single().Name;
			return await _dbSet
				.Where(x => ids.Contains(EF.Property<int>(x, idName)))
				.Where(wherePredicate)
				.ToListAsync(); // Ensure this is async
		}

		public async Task<List<TEntity>> GetByIds(Expression<Func<TEntity, bool>> wherePredicate, int[] ids, string columnName)
		{
			return await _dbSet
				.Where(x => ids.Contains(EF.Property<int>(x, columnName)))
				.Where(wherePredicate)
				.ToListAsync(); // Ensure this is async
		}

		public async Task<List<TEntity>> GetByIds(Expression<Func<TEntity, bool>> wherePredicate, int[] ids, string columnName, List<string> include)
		{
			var queryable = _context.Set<TEntity>().AsQueryable();
			foreach (var item in include)
				queryable = queryable.Include(item);
			return await queryable
				.Where(x => ids.Contains(EF.Property<int>(x, columnName)))
				.Where(wherePredicate)
				.ToListAsync(); // Ensure this is async
		}

		public async Task<List<TEntity>> GetByIds(Expression<Func<TEntity, bool>> wherePredicate, int[] ids, List<string> include)
		{
			var idName = _context.Model.FindEntityType(typeof(TEntity))
				.FindPrimaryKey().Properties.Single().Name;
			var queryable = _context.Set<TEntity>().AsQueryable();
			foreach (var item in include)
				queryable = queryable.Include(item);
			return await queryable
				.Where(x => ids.Contains(EF.Property<int>(x, idName)))
				.Where(wherePredicate)
				.ToListAsync(); // Ensure this is async
		}

		public TEntity Add(TEntity entity)
		{
			return _dbSet.Add(entity).Entity;
		}

		public async Task AddRange(IEnumerable<TEntity> entityList)
		{
			await _dbSet.AddRangeAsync(entityList); // Ensure this is async
		}

		public async Task<TEntity> Update(TEntity entity)
		{
			_dbSet.Update(entity);
			await SaveChangesAsync(); // Save changes asynchronously
			return entity;
		}

		public async Task UpdateRange(IEnumerable<TEntity> entityList)
		{
			_dbSet.UpdateRange(entityList);
			await SaveChangesAsync(); // Save changes asynchronously
		}

		public TEntity Delete(int id)
		{
			var entity = _dbSet.Find(id);
			if (entity != null)
				_dbSet.Remove(entity);
			return entity;
		}

		public async Task<IEnumerable<AnatomyModel>> GetData(Expression<Func<AnatomyModel, bool>> predicate)
		{
			return await _context.CaseImage.Where(predicate).ToListAsync();
		}


		public async Task<IQueryable<TEntity>> GetQueryableData(Expression<Func<TEntity, bool>> predicate)
		{
			return _dbSet.Where(predicate).AsQueryable();
		}

		public async Task<TEntity> SingleOrDefault(Expression<Func<TEntity, bool>> predicate)
		{
			return await _dbSet.SingleOrDefaultAsync(predicate); // Ensure this is async
		}

		public async Task<TEntity> FirstOrDefault(Expression<Func<TEntity, bool>> predicate)
		{
			return await _dbSet.FirstOrDefaultAsync(predicate); // Ensure this is async
		}

		public async Task<TEntity> FirstOrDefault()
		{
			return await _dbSet.FirstOrDefaultAsync(); // Ensure this is async
		}

		public bool ExistsById(int id)
		{
			return _dbSet.Any(e => e == _dbSet.Find(id));
		}

		public bool ExistsByName(Expression<Func<TEntity, bool>> predicate)
		{
			return _dbSet.Any(predicate);
		}

		public TEntity GetById(int id, List<string> include)
		{
			var _dbSetQueryable = _context.Set<TEntity>().AsQueryable();
			foreach (var item in include)
				_dbSetQueryable = _dbSetQueryable.Include(item);

			// Perform the query to get the entity
			var result = _context.Set<TEntity>().Find(id); // Use DbSet<TEntity>.Find here

			return result;
		}


		public async Task<IEnumerable<TEntity>> GetData(Expression<Func<TEntity, bool>> predicate, List<string> include)
		{
			var queryable = _context.Set<TEntity>().AsQueryable();
			foreach (var item in include)
				queryable = queryable.Include(item);

			return await queryable.Where(predicate).ToListAsync(); // Ensure this is async
		}

		public async Task<IQueryable<TEntity>> GetQueryableData(Expression<Func<TEntity, bool>> predicate, List<string> include)
		{
			var queryable = _context.Set<TEntity>().AsQueryable();
			foreach (var item in include)
				queryable = queryable.Include(item);

			return queryable.Where(predicate).AsQueryable();
		}

		public async Task<IEnumerable<TEntity>> GroupBy(Expression<Func<TEntity, TEntity>> predicate, List<string> include)
		{
			var queryable = _context.Set<TEntity>().AsQueryable();
			foreach (var item in include)
				queryable = queryable.Include(item);

			return await queryable.GroupBy(predicate).Cast<TEntity>().ToListAsync(); // Ensure this is async
		}

		public async Task<IEnumerable<TEntity>> GroupBy(Expression<Func<TEntity, TEntity>> predicate)
		{
			var queryable = _context.Set<TEntity>().AsQueryable();
			return await queryable.GroupBy(predicate).Cast<TEntity>().ToListAsync(); // Ensure this is async
		}

		public async Task<TEntity> SingleOrDefault(Expression<Func<TEntity, bool>> predicate, List<string> include)
		{
			var queryable = _context.Set<TEntity>().AsQueryable();
			foreach (var item in include)
				queryable = queryable.Include(item);

			return await queryable.SingleOrDefaultAsync(predicate); // Ensure this is async
		}

		public async Task<IEnumerable<TEntity>> GetAll(List<string> include)
		{
			var queryable = _context.Set<TEntity>().AsQueryable();
			foreach (var item in include)
				queryable = queryable.Include(item);

			return await queryable.ToListAsync(); // Ensure this is async
		}

		public async Task RemoveRange(IEnumerable<TEntity> myObject)
		{
			_dbSet.RemoveRange(myObject);
			await SaveChangesAsync(); // Save changes asynchronously
		}

		public async Task<object> FirstOrDefault(Expression<Func<TEntity, bool>> wherePredicate, Expression<Func<TEntity, object>> selectPredicate, List<string> include)
		{
			var queryable = _context.Set<TEntity>().AsQueryable();
			foreach (var item in include)
				queryable = queryable.Include(item);

			return await queryable.Where(wherePredicate).Select(selectPredicate).FirstOrDefaultAsync(); // Ensure this is async
		}

		public async Task<object> SingleOrDefault(Expression<Func<TEntity, bool>> wherePredicate, Expression<Func<TEntity, object>> selectPredicate, List<string> include)
		{
			var queryable = _context.Set<TEntity>().AsQueryable();
			foreach (var item in include)
				queryable = queryable.Include(item);

			return await queryable.Where(wherePredicate).Select(selectPredicate).SingleOrDefaultAsync(); // Ensure this is async
		}

		public IQueryable<TEntity> FindBy(Expression<Func<TEntity, bool>> predicate, bool disableTracking = true)
		{
			return disableTracking
				? _context.Set<TEntity>().Where(predicate).AsNoTracking()
				: _context.Set<TEntity>().Where(predicate);
		}

		public async Task SaveChangesAsync()
		{
			await _context.SaveChangesAsync(); // Ensure this is async
		}
		public async Task<IEnumerable<object>> GetData(Expression<Func<TEntity, bool>> wherePredicate, Expression<Func<TEntity, object>> selectPredicate, List<string> include)
		{
			var queryable = _context.Set<TEntity>().AsQueryable();

			// Apply includes
			foreach (var item in include)
			{
				queryable = queryable.Include(item);
			}

			// Apply where predicate and select predicate
			var result = await queryable
				.Where(wherePredicate)
				.Select(selectPredicate)
				.ToListAsync(); // Ensure this is async

			return result;
		}

	}
}
