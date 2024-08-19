using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UploadingCaseImages.Service;

[ApiController]
[Route("[controller]")]
public class CaseController : ControllerBase
{
	private readonly CaseService _caseService;
	public CaseController(CaseService caseService)
    {
		_caseService = caseService;
    }
	[HttpGet("{id}")]
	public async Task<IActionResult> GetCaseById(int id)
    {
		var caseDetails = await _caseService.GetCaseByIdAsync(id);
		if (caseDetails == null)
		{
			return NotFound();
		}
		return Ok(caseDetails);
	}
}
