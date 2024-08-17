using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UploadingCaseImages.DB.Enums;
using UploadingCaseImages.DB.Model;
using UploadingCaseImages.Service.DTOs;
using static System.Net.Mime.MediaTypeNames;

namespace UploadingCaseImages.Service
{
	public interface IBodyImageService
	{
		Task<IEnumerable<BodyImageDto>> GetAllImagesAsync();
		Task<BodyImageDto> GetImageByIdAsync(int id);
		Task AddImageAsync(UpdateBodyImageDto dto);
		Task UpdateImageAsync(int id, UpdateBodyImageDto dto);
		Task DeleteImageAsync(int id);
		Task<IEnumerable<BodyImageDto>> FilterImagesByBodyPartAsync(BodyPart bodyPart);
	}
}


