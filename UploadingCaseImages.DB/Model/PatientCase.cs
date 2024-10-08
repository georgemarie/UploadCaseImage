namespace UploadingCaseImages.DB.Model;
public class PatientCase
{
	public int Id { get; set; }
	public string Note { get; set; }
	public DateTime VisitDate { get; set; }
	public DateTime CreatedAt { get; set; } = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time"));
	public int AnatomyId { get; set; }
	public Anatomy Anatomy { get; set; }

	public int PatientId { get; set; }
	public Patient Patient { get; set; }

	public int DoctorId { get; set; }
	public Doctor Doctor { get; set; }
	public ICollection<CaseImage> CaseImages { get; set; }
}
