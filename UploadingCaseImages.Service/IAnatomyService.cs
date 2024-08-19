using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UploadingCaseImages.DB.Model;
using UploadingCaseImages.Service.DTOs;

namespace UploadingCaseImages.Service;
public interface IAnatomyService
{
	Task<GenericResponseModel<List<Anatomy>>> GetAllAnatomiesAsync();
}
