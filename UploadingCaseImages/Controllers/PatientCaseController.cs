using Microsoft.AspNetCore.Mvc;
using UploadingCaseImages.Service.DTOs;
using UploadingCaseImages.Service;

namespace UploadingCaseImages.Controllers;
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
		var response = await _patientCaseService.GetPatientCaseAsync(dto);
		return Ok(response);
	}

	[HttpPost("SaveInfo")]
	public async Task<IActionResult> AddPatientCase([FromBody] GetPatientCaseDto dto)
	{
		if (dto == null)
		{
			return BadRequest("Patient case data is null.");
		}

		var response = await _patientCaseService.AddPatientCaseAsync(dto);

		if (response.IsSuccess)
		{
			return Ok(response.Data);
		}

		return StatusCode(500, "Internal server error.");
	}
}
