namespace UploadingCaseImages.Service.DTOs;
public class CaseImageToReturnDto
{
	public int CaseImageId { get; set; }
	public string CaseName { get; set; }
	public string CasePath { get; set; }
	public DateTime CreatedAt { get; set; }
}
