namespace UploadingCaseImages.Service.DTOs;
public class GetPatientCaseDto
{
	public int PageNumber { get; set; } = 1;
	public int PageSize { get; set; } = 10;
	public string? AnatomyId { get; set; }
	public DateTime? VisitDate { get; set; }
}
