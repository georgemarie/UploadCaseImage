using UploadingCaseImages.Service.DTOs;

namespace UploadingCaseImages.Service;
public interface IAnatomyService
{
	Task<GenericResponseModel<List<AnatomyDto>>> GetAllAnatomiesAsync();
}
