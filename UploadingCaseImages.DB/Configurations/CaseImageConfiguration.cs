using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using UploadingCaseImages.DB.Model;

namespace UploadingCaseImages.DB.Configurations;
public class CaseImageConfiguration : IEntityTypeConfiguration<CaseImage>
{
	public void Configure(EntityTypeBuilder<CaseImage> builder)
	{
		builder.HasKey(x => x.CaseImageId);
		builder.HasOne(a => a.PatientCase)
			.WithMany(a => a.CaseImages)
			.HasForeignKey(a => a.PatientCaseId);
	}
}
