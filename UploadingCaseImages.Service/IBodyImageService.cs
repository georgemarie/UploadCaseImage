using System.Collections.Generic;
using System.Threading.Tasks;
using UploadingCaseImages.Service.DTOs;
using UploadingCaseImages.DB.Enums;
using UploadingCaseImages.Service.Utilities;

namespace UploadingCaseImages.Service
{
	public interface IBodyImageService
	{
		Task<GenericResponseModel<IEnumerable<BodyImageDto>>> GetAllImageCaseAsync();
		Task<GenericResponseModel<BodyImageDto>> GetImageByIdAsync(int id);
		Task<GenericResponseModel<bool>> AddImageAsync(BodyImageDto dto);
		Task<GenericResponseModel<bool>> UpdateImageAsync(int id, UpdateBodyImageDto dto);
		Task<GenericResponseModel<bool>> DeleteImageAsync(int id);
		Task<GenericResponseModel<IEnumerable<BodyImageDto>>> FilterImagesByBodyPartAsync(AnatomyEnum bodyPart);
	}
}
