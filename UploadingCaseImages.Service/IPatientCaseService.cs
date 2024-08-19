using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UploadingCaseImages.Service.DTOs;

namespace UploadingCaseImages.Service;
public interface IPatientCaseService
{
	Task<GenericResponseModel<IEnumerable<GetPatientCaseDto>>> GetPatientCaseAsync(GetPatientCaseDto dto);
	Task<GenericResponseModel<bool>> AddPatientCaseAsync(GetPatientCaseDto dto);
}
