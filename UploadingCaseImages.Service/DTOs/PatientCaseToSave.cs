using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UploadingCaseImages.Service.DTOs;
public class PatientCaseToSave
{
	public string Note { get; set; }
	public DateTime VisitDate { get; set; }
	public DateTime CreatedAt { get; set; }
	public int AnatomyId { get; set; }
	public List<CaseImageToSaveDto> CaseImages { get; set; }
}
