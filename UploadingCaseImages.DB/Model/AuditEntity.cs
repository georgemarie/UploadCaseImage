namespace UploadingCaseImages.DB.Model;

public class AuditEntity : BaseEntity
{
	public DateTime CreatedOn { get; set; } = DateTime.UtcNow.AddHours(2);

	public string CreatedBy { get; set; }

	public DateTime? ModifiedOn { get; set; }

	public string ModifiedBy { get; set; }
}
