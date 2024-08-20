using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UploadingCaseImages.DB.Model;
using UploadingCaseImages.Service.DTOs;

namespace UploadingCaseImages.Service;
public interface ICaseService
{
	Task<PatientCaseToReturnDto> GetCaseByIdAsync(int id);
}
