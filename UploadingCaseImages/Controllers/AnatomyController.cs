using Microsoft.AspNetCore.Mvc;
using UploadingCaseImages.Service;

namespace UploadingCaseImages.Controllers;
[ApiController]
[Route("api/[controller]")]
public class AnatomyController : ControllerBase
{
	private readonly IAnatomyService _anatomyService;

	public AnatomyController(IAnatomyService anatomyService)
	{
		_anatomyService = anatomyService;
	}

	[HttpGet("All")]
	public async Task<IActionResult> GetAllAnatomies()
	{
		return Ok(await _anatomyService.GetAllAnatomiesAsync());
	}
}
