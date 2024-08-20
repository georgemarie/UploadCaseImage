namespace UploadingCaseImages.DB.Model;
public class CaseImage
{
	public int Id { get; set; }
	public string ImageName { get; set; }
	public string ImagePath { get; set; }
	public DateTime CreatedAt { get; set; } = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time"));
	public int PatientCaseId { get; set; }
	public PatientCase PatientCase { get; set; }
}
