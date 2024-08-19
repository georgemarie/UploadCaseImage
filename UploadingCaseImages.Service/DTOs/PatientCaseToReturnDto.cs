namespace UploadingCaseImages.Service.DTOs;
public class PatientCaseToReturnDto
{
	public int PatientCaseId { get; set; }
	public string Note { get; set; }
	public DateTime VisitDate { get; set; }
	public DateTime CreatedAt { get; set; }
	public int AnatomyId { get; set; }
	public string AnatomyName { get; set; }
	public List<CaseImageToReturnDto> CaseImages { get; set; }
}
