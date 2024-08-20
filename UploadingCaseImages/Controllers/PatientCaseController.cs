using Microsoft.AspNetCore.Mvc;
using UploadingCaseImages.Service;
using UploadingCaseImages.Service.DTOs;

namespace UploadingCaseImages.Controllers;
[ApiController]
[Route("api/[controller]")]
public class PatientCaseController : Controller
{
	private readonly IPatientCaseService _patientCaseService;

	public PatientCaseController(IPatientCaseService patientCaseService)
	{
		_patientCaseService = patientCaseService;
	}

	[HttpGet("GetPatientCase")]
	public async Task<IActionResult> GetPatientCase([FromQuery] GetPatientCaseDto dto)
	{
		return Ok(await _patientCaseService.GetPatientCaseAsync(dto));
	}

	[HttpPost("add")]
	public async Task<IActionResult> AddPatientCase([FromBody] PatientCaseToSave dto)
	{
		if (dto == null)
		{
			return BadRequest("Patient case data is null.");
		}

		var response = await _patientCaseService.AddPatientCaseAsync(dto);

		return response.ErrorList.Count != 0 ? BadRequest(response) : Ok(response);
	}

	[HttpGet("{id}")]
	public async Task<IActionResult> GetCaseById([FromRoute] int id)
	{
		var response = await _patientCaseService.GetPatientCaseByIdAsync(id);
		return response.ErrorList.Count != 0 ? BadRequest(response) : Ok(response);
	}
}
