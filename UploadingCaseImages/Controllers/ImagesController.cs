using Microsoft.AspNetCore.Mvc;
using UploadingCaseImages.Service;
using UploadingCaseImages.Service.DTOs;

namespace UploadingCaseImages.Controllers;
[ApiController]
[Route("api/[controller]")]
public class ImagesController : ControllerBase
{
	public ImagesController()
	{
	}

	[HttpPost("upload")]
	public async Task<IActionResult> UploadImages([FromForm] ImageUploadModel model)
	{
		var images = new List<ImageModel>();
		var currentDirectory = Directory.GetCurrentDirectory();
		var uploadsFolder = Path.Combine(currentDirectory, "CaseImages");
		if (!Directory.Exists(uploadsFolder))
		{
			Directory.CreateDirectory(uploadsFolder);
		}

		foreach (var file in model.Files)
		{
			if (file.Length > 0)
			{
				var randomNumber = new Random().Next(100000, 999999);
				var uniqueFileName = $"{Path.GetFileNameWithoutExtension(file.FileName)}_{randomNumber}{Path.GetExtension(file.FileName)}";
				var filePath = Path.Combine(uploadsFolder, uniqueFileName);
				using (var stream = new FileStream(filePath, FileMode.Create))
				{
					await file.CopyToAsync(stream);
				}

				var image = new ImageModel
				{
					FileName = uniqueFileName,
					FilePath = Path.Combine("uploads", uniqueFileName)
				};

				images.Add(image);

			}
		}

		return Ok(GenericResponseModel<List<ImageModel>>.Success(images));
	}
}

