using Microsoft.AspNetCore.Http;

namespace UploadingCaseImages.Service.DTOs;
public class ImageUploadModel
{
	public List<IFormFile> Files { get; set; }
}
