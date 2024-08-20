namespace UploadingCaseImages.DB.Model;
public class Anatomy
{
	public int Id { get; set; }
	public string Name { get; set; }
	public string ImagePath { get; set; }
	public DateTime CreatedAt { get; set; } = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time"));
}
