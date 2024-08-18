using System.Threading.Tasks;
using UploadingCaseImages.DB;
using UploadingCaseImages.DB.Model;
using UploadingCaseImages.Repository;

namespace UploadingCaseImages.UnitOfWorks
{
	public class UnitOfWork : IUnitOfWork
	{
		private readonly UploadingCaseImagesContext _context;
		private IGenericRepository<AnatomyModel> _anatomyRepository;

		public UnitOfWork(UploadingCaseImagesContext context)
		{
			_context = context;
		}

		public IGenericRepository<AnatomyModel> AnatomyRepository
		{
			get
			{
				return _anatomyRepository ??= new GenericRepository<AnatomyModel>(_context);
			}
		}

		public async Task SaveChangesAsync()
		{
			await _context.SaveChangesAsync();
		}
	}
}
