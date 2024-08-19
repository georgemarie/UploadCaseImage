using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UploadingCaseImages.DB.Model;
using static System.Net.Mime.MediaTypeNames;

namespace UploadingCaseImages.DB.Configurations;
public class AnatomyConfiguration : IEntityTypeConfiguration<Anatomy>
{
	public void Configure(EntityTypeBuilder<Anatomy> builder)
	{
		builder.HasKey(x => x.AnatomyId);
	}
}
