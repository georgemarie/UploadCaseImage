using System.Net;
using Microsoft.AspNetCore.Mvc;
using UploadingCaseImages.Service;
using UploadingCaseImages.Service.DTOs;

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
	[ProducesResponseType(typeof(GenericResponseModel<List<AnatomyDto>>), (int)HttpStatusCode.OK)]
	public async Task<IActionResult> GetAllAnatomies()
	{
		return Ok(await _anatomyService.GetAllAnatomiesAsync());
	}
}
