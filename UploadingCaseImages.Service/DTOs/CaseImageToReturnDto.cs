namespace UploadingCaseImages.Service.DTOs;
public class CaseImageToReturnDto
{
	public int Id { get; set; }
	public string ImageName { get; set; }
	public string ImagePath { get; set; }
	public DateTime CreatedAt { get; set; }
}
