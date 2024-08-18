[ApiController]
[Route("api/[controller]")]
public class CaseController : ControllerBase
{
	private readonly ICaseService _caseService;

	public CaseController(ICaseService caseService)
	{
		_caseService = caseService;
	}

	// Save Case API
	[HttpPost("save")]
	public async Task<IActionResult> SaveCase([FromBody] Case caseData)
	{
		if (caseData == null)
		{
			return BadRequest("Invalid data.");
		}

		try
		{
			await _caseService.SaveCaseAsync(caseData);
			return Ok("Case saved successfully.");
		}
		catch (Exception ex)
		{
			return StatusCode(500, $"Internal server error: {ex.Message}");
		}
	}

	// Get Body Part API
	[HttpGet("bodypart/{id}")]
	public async Task<IActionResult> GetBodyPart(int id)
	{
		try
		{
			var bodyPart = await _caseService.GetBodyPartAsync(id);
			if (bodyPart == null)
			{
				return NotFound("Anatomy not found.");
			}
			return Ok(bodyPart);
		}
		catch (Exception ex)
		{
			return StatusCode(500, $"Internal server error: {ex.Message}");
		}
	}
}
