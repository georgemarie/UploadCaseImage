namespace UploadingCaseImages.DB.Model;
public class Patient
{
	public int Id { get; set; }
	public string Name { get; set; }
	public string Phone { get; set; }
	public string AccessToken { get; set; }

	public virtual ICollection<PatientCase> PatientCases { get; set; }
}
