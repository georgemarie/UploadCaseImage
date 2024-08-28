using UploadingCaseImages.Service.Common;
using UploadingCaseImages.Service.DTOs;

namespace UploadingCaseImages.Service;
public interface IPatientCaseService
{
	Task<PageResponse<PatientCaseToReturnDto>> GetPatientCaseAsync(GetPatientCaseDto dto);

	Task<GenericResponseModel<int>> AddPatientCaseAsync(PatientCaseToSave dto);

	Task<GenericResponseModel<PatientCaseToReturnDto>> GetPatientCaseByIdAsync(int id);
}
