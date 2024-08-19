using UploadingCaseImages.Service.DTOs;

namespace UploadingCaseImages.Service;
public interface IPatientCaseService
{
	Task<GenericResponseModel<IEnumerable<PatientCaseToReturnDto>>> GetPatientCaseAsync(GetPatientCaseDto dto);

	Task<GenericResponseModel<bool>> AddPatientCaseAsync(PatientCaseToSave dto);
}
