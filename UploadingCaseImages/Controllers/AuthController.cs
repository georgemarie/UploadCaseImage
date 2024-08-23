using Microsoft.AspNetCore.Mvc;
using UploadingCaseImages.Service;
using UploadingCaseImages.Service.DTOs;

namespace UploadingCaseImages.Controllers;
[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
	private readonly IAuthService _authService;

	public AuthController(IAuthService authService)
	{
		_authService = authService;
	}

	[HttpPost("login/patient")]
	public async Task<IActionResult> PatientLogin([FromBody] LoginRequestDTO model)
	{
		var result = await _authService.AuthenticatePatientAsync(model);

		return result.ErrorList.Count > 0 ? BadRequest(result) : Ok(result);
	}

	[HttpPost("login/doctor")]
	public async Task<IActionResult> DoctorLogin([FromBody] LoginRequestDTO model)
	{
		var result = await _authService.AuthenticateDoctorAsync(model);

		return result.ErrorList.Count > 0 ? BadRequest(result) : Ok(result);
	}
}
