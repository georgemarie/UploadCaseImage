using System.Security.Cryptography;
using Microsoft.AspNetCore.Mvc;
using UploadingCaseImages.Service;
using UploadingCaseImages.Service.DTOs;
using UploadingCaseImages.Service.Resources;

namespace UploadingCaseImages.Controllers;
[ApiController]
[Route("api/[controller]")]
public class ImagesController : ControllerBase
{
	private static List<string> SupportedTypes = ["jpg", "jpeg", "png", "bmp", "webp"];
	private static long MaxFileSize = 3145728;

	[HttpPost("upload")]
	public async Task<IActionResult> UploadImages([FromForm] ImageUploadModel model)
	{
		var errors = new List<ErrorResponseModel>();

		foreach (var file in model.Files)
		{
			if (!SupportedTypes.Contains(file.ContentType.Split("/")[1]))
			{
				errors.Add(new() { PropertyName = file.FileName, Message = "File format is not supported. Only JPG, JPEG, PNG, BMP, and WebP formats are allowed." });
				continue;
			}

			if (file.Length <= 0 || file.Length > MaxFileSize)
				errors.Add(new() { PropertyName = file.FileName, Message = "File too large. The maximum file size is 5 MB." });
		}

		if (errors.Count > 0)
			return this.BadRequest(GenericResponseModel<List<UploadedImageToReturnDto>>.Failure(Shared.IncorrectData, errors));

		var images = new List<UploadedImageToReturnDto>();
		var currentDirectory = Directory.GetCurrentDirectory();
		var uploadsFolder = Path.Combine(currentDirectory, "CaseImages");
		if (!Directory.Exists(uploadsFolder))
		{
			Directory.CreateDirectory(uploadsFolder);
		}

		foreach (var file in model.Files)
		{
			var randomNumber = RandomNumberGenerator.GetInt32(100000, 999999);
			var uniqueFileName = $"{Path.GetFileNameWithoutExtension(file.FileName)}_{randomNumber}{Path.GetExtension(file.FileName)}";
			var filePath = Path.Combine(uploadsFolder, uniqueFileName);
			await using (var stream = new FileStream(filePath, FileMode.Create))
			{
				await file.CopyToAsync(stream);
			}

			var image = new UploadedImageToReturnDto
			{
				ImageName = uniqueFileName,
				ImagePath = Path.Combine("CaseImages", uniqueFileName)
			};

			images.Add(image);
		}

		return this.Ok(GenericResponseModel<List<UploadedImageToReturnDto>>.Success(images));
	}
}

