using Microsoft.AspNetCore.Mvc;
using UploadingCaseImages.Service;
using UploadingCaseImages.Service.DTOs;
using System.Threading.Tasks;
using UploadingCaseImages.DB.Enums;

namespace UploadingCaseImages.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class BodyImageController : ControllerBase
	{
		private readonly IBodyImageService _imageService;

		public BodyImageController(IBodyImageService imageService)
		{
			_imageService = imageService;
		}

		[HttpGet]
		public async Task<IActionResult> GetAllImages()
		{
			var images = await _imageService.GetAllImagesAsync();
			return Ok(images);
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetImageById(int id)
		{
			var image = await _imageService.GetImageByIdAsync(id);
			if (image == null)
				return NotFound();
			return Ok(image);
		}

		[HttpPost]
		public async Task<IActionResult> AddImage([FromBody] UpdateBodyImageDto dto)
		{
			await _imageService.AddImageAsync(dto);
			return CreatedAtAction(nameof(GetImageById), new { id = dto.Id }, dto);
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> UpdateImage(int id, [FromBody] UpdateBodyImageDto dto)
		{
			await _imageService.UpdateImageAsync(id, dto);
			return NoContent();
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteImage(int id)
		{
			await _imageService.DeleteImageAsync(id);
			return NoContent();
		}

		[HttpGet("filter/{bodyPart}")]
		public async Task<IActionResult> FilterImagesByBodyPart(BodyPart bodyPart)
		{
			var images = await _imageService.FilterImagesByBodyPartAsync(bodyPart);
			return Ok(images);
		}
	}
}
