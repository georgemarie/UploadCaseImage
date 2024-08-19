using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UploadingCaseImages.DB.Model;

namespace UploadingCaseImages.Service;
public interface IAnatomyService
{
	Task<IEnumerable<Anatomy>> GetAllAnatomiesAsync();
}
