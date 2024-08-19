//using Microsoft.AspNetCore.Mvc;
//using UploadingCaseImages.Service;
//using UploadingCaseImages.Service.DTOs;
//using System.Threading.Tasks;

//namespace UploadingCaseImages.Controllers
//{
//	[ApiController]
//	[Route("api/[controller]")]
//	public class ReturnImageHistoryController : ControllerBase
//	{
//		private readonly IBodyImageService _imageService;

//		public ReturnImageHistoryController(IBodyImageService imageService)
//		{
//			_imageService = imageService;
//		}

//		[HttpGet]
//		public async Task<IActionResult> GetAllImages()
//		{
//			var images = await _imageService.GetAllImageCaseAsync(); 
//			return Ok(images);
//		}

//		[HttpGet("{id}")]
//		public async Task<IActionResult> GetImageById(int id)
//		{
//			var image = await _imageService.GetImageByIdAsync(id);
//			if (image == null)
//				return NotFound();
//			return Ok(image);
//		}

//		[HttpPost]
//		public async Task<IActionResult> AddImage([FromBody] BodyImageDto dto)
//		{
//			await _imageService.AddImageAsync(dto);
//			return CreatedAtAction(nameof(GetImageById), new { id = dto.Id }, dto);
//		}

//		[HttpPut("{id}")]
//		public async Task<IActionResult> UpdateImage(int id, [FromBody] UpdateBodyImageDto dto)
//		{
//			await _imageService.UpdateImageAsync(id, dto);
//			return NoContent();
//		}

//		[HttpDelete("{id}")]
//		public async Task<IActionResult> DeleteImage(int id)
//		{
//			await _imageService.DeleteImageAsync(id);
//			return NoContent();
//		}

//		//[HttpGet("filter/{bodyPart}")]
//		//public async Task<IActionResult> FilterImagesByBodyPart(AnatomyEnum bodyPart)
//		//{
//		//	var images = await _imageService.FilterImagesByBodyPartAsync(bodyPart);
//		//	return Ok(images);
//		//}
//	}
//}
