using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UploadingCaseImages.DB.Model;

namespace UploadingCaseImages.Service.DTOs;
public class GetPatientCaseDto
{
	public int PatientCaseId { get; set; }
	public string Note { get; set; }
	public DateTime VisitDate { get; set; }
	public DateTime CreatedAt { get; set; }
	public int AnatomyId { get; set; }
	public ICollection<CaseImage> CaseImages { get; set; }
	public string AnatomyName { get; set; }
}
