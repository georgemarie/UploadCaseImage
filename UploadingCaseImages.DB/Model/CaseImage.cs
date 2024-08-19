using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UploadingCaseImages.DB.Model;
public class CaseImage
{
	public int CaseImageId { get; set; }
	public string CaseName { get; set; }
	public string CasePath { get; set; }
	public DateTime CreatedAt { get; set; }
	public int PatientCaseId { get; set; }
	public PatientCase PatientCase { get; set; }
}
