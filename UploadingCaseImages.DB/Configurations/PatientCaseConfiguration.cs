using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UploadingCaseImages.DB.Model;

namespace UploadingCaseImages.DB.Configurations;
public class PatientCaseConfiguration : IEntityTypeConfiguration<PatientCase>
{
	public void Configure(EntityTypeBuilder<PatientCase> builder)
	{
		builder.HasKey(x => x.PatientCaseId);
		builder.HasMany(a => a.CaseImages)
			.WithOne(a => a.PatientCase)
			.HasForeignKey(a => a.CaseImageId);
	}
}
