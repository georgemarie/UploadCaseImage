using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UploadingCaseImages.DB.Model;

namespace UploadingCaseImages.DB.Configurations;
public class CaseImageConfiguration : IEntityTypeConfiguration<CaseImage>
{
	public void Configure(EntityTypeBuilder<CaseImage> builder)
	{
		builder.HasKey(x => x.Id);
	}
}
