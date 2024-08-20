namespace UploadingCaseImages.Service.DTOs;
public class PatientCaseToSave
{
	public string Note { get; set; }
	public DateTime VisitDate { get; set; }
	public int AnatomyId { get; set; }
	public List<CaseImageToSaveDto> CaseImages { get; set; }
}
