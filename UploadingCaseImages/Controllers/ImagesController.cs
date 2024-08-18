using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static System.Net.Mime.MediaTypeNames;
using UploadingCaseImages.UnitOfWorks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using UploadingCaseImages.Service.DTOs;
using UploadingCaseImages.Service;

namespace UploadingCaseImages.Controllers;
[ApiController]
[Route("api/[controller]")]
public class ImageUploadController : ControllerBase
{
	
	private readonly IWebHostEnvironment _environment;

	public ImageUploadController(IWebHostEnvironment environment)
	{
		
		_environment = environment;
	}

	[HttpPost("upload")]
	public async Task<IActionResult> UploadImages([FromForm] ImageUploadModel model)
	{
		List<ImageModel> images = new List<ImageModel>();

		var uploadsFolder = Path.Combine(_environment.WebRootPath, "uploads");
		if (!Directory.Exists(uploadsFolder))
		{
			Directory.CreateDirectory(uploadsFolder);
		}

		foreach (var file in model.Files)
		{
			if (file.Length > 0)
			{
				var filePath = Path.Combine(uploadsFolder, file.FileName);
				using (var stream = new FileStream(filePath, FileMode.Create))
				{
					await file.CopyToAsync(stream);
				}

				var image = new ImageModel
				{
					FileName = file.FileName,
					FilePath = Path.Combine("uploads", file.FileName), // Save relative path
				};

				images.Add(image);
				
			}

		}

		return Ok(GenericResponseModel<List<ImageModel>>.Success(images));
	}
}

