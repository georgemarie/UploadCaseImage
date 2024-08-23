using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using UploadingCaseImages.DB.Model;
using UploadingCaseImages.Service.DTOs;
using UploadingCaseImages.Service.Utilities;
using UploadingCaseImages.UnitOfWorks;

namespace UploadingCaseImages.Service;
public class AuthService : IAuthService
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly IConfiguration _configuration;

	public AuthService(IUnitOfWork unitOfWork, IConfiguration configuration)
	{
		_unitOfWork = unitOfWork;
		_configuration = configuration;
	}

	public async Task<GenericResponseModel<string>> AuthenticateDoctorAsync(LoginRequestDTO model)
	{
		var doctor = await _unitOfWork
			.Repository<Doctor>()
			.FindBy(p => p.Phone == model.Phone, false)
			.FirstOrDefaultAsync();

		if (doctor == null)
		{
			return GenericResponseModel<string>.Failure(
				Constants.FailureMessage,
				new List<ErrorResponseModel> { ErrorResponseModel.Create(nameof(model.Phone), "Doctor not found.") });
		}

		var token = GenerateToken(doctor.Id, "Doctor");
		return GenericResponseModel<string>.Success(token);
	}

	public async Task<GenericResponseModel<string>> AuthenticatePatientAsync(LoginRequestDTO model)
	{
		var patient = await _unitOfWork
			.Repository<Patient>()
			.FindBy(p => p.Phone == model.Phone, false)
			.FirstOrDefaultAsync();

		if (patient == null)
		{
			return GenericResponseModel<string>.Failure(
				Constants.FailureMessage,
				new List<ErrorResponseModel> { ErrorResponseModel.Create(nameof(model.Phone), "Patient not found.") });
		}

		var token = GenerateToken(patient.Id, "Patient");
		return GenericResponseModel<string>.Success(token);
	}

	public string GenerateToken(int id, string userType)
	{
		var claims = new List<Claim>
			{
				new Claim("UserId", id.ToString()),
				new Claim("UserType", userType)
			};

		var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]));
		var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

		var tokenDescriptor = new SecurityTokenDescriptor
		{
			Subject = new ClaimsIdentity(claims),
			Expires = DateTime.UtcNow.AddYears(1),
			SigningCredentials = creds,
			Audience = _configuration["Jwt:Audience"],
			Issuer = _configuration["Jwt:Issuer"]
		};

		var tokenHandler = new JwtSecurityTokenHandler();
		var token = tokenHandler.CreateToken(tokenDescriptor);
		return tokenHandler.WriteToken(token);
	}
}
