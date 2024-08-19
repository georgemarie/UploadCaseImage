using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UploadingCaseImages.DB.Model;

namespace UploadingCaseImages.DB.Configurations;
public class PatientCaseConfiguration : IEntityTypeConfiguration<PatientCase>
{
	public void Configure(EntityTypeBuilder<PatientCase> builder)
	{
		builder.HasKey(x => x.PatientCaseId);

		builder.HasOne(a => a.Anatomy);

		builder.HasMany(a => a.CaseImages)
			.WithOne(a => a.PatientCase)
			.HasForeignKey(a => a.CaseImageId);
	}
}
