using UploadingCaseImages.Service.DTOs;

namespace UploadingCaseImages.Service;
public interface IAuthService
{
	Task<GenericResponseModel<string>> AuthenticateDoctorAsync(LoginRequestDTO model);

	Task<GenericResponseModel<string>> AuthenticatePatientAsync(LoginRequestDTO model);
}
