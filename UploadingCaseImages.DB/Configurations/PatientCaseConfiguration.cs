using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UploadingCaseImages.DB.Model;

namespace UploadingCaseImages.DB.Configurations;
public class PatientCaseConfiguration : IEntityTypeConfiguration<PatientCase>
{
	public void Configure(EntityTypeBuilder<PatientCase> builder)
	{
		builder.HasKey(x => x.Id);

		builder.HasOne(a => a.Anatomy);

		builder.HasOne(c => c.Patient)
			.WithMany(a => a.PatientCases)
			.HasForeignKey(c => c.PatientId);

		builder.HasOne(c => c.Doctor)
			.WithMany(a => a.PatientCases)
			.HasForeignKey(c => c.DoctorId);

		builder
			.HasMany(a => a.CaseImages)
			.WithOne(a => a.PatientCase)
			.HasForeignKey(a => a.PatientCaseId);
	}
}
