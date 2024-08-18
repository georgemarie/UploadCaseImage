using System.Threading.Tasks;
using UploadingCaseImages.DB.Model;
using UploadingCaseImages.Repository;

namespace UploadingCaseImages.UnitOfWorks
{
	public interface IUnitOfWork
	{
		IGenericRepository<AnatomyModel> AnatomyRepository { get; }
		Task SaveChangesAsync();
	}
}
