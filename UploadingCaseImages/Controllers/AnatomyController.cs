[ApiController]
[Route("api/[controller]")]
public class AnatomyController : ControllerBase
{
	private readonly IAnatomyService _anatomyService;

	public AnatomyController(IAnatomyService anatomyService)
	{
		_anatomyService = anatomyService;
	}

	[HttpGet("bodypart/{bodyPartName}")]
	public async Task<IActionResult> GetBodyPartByName(string bodyPartName)
	{
		var anatomy = await _anatomyService.GetBodyPartByNameAsync(bodyPartName);

		if (anatomy == null)
		{
			return NotFound($"No anatomy found for the body part '{bodyPartName}'.");
		}

		return Ok(anatomy);
	}
}
