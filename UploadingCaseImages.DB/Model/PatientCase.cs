using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UploadingCaseImages.DB.Model;
public class PatientCase
{
	public int PatientCaseId { get; set; }
	public string Note { get; set; }
	public DateTime VisitDate { get; set; }
	public DateTime CreatedAt { get; set; }
	public int AnatomyId { get; set; }
	public ICollection<CaseImage> CaseImages { get; set; }
}
