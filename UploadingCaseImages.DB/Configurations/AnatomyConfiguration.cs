using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UploadingCaseImages.DB.Model;
using static System.Net.Mime.MediaTypeNames;

namespace UploadingCaseImages.DB.Configurations;
public class AnatomyConfiguration : IEntityTypeConfiguration<AnatomyModel>
{
	public void Configure(EntityTypeBuilder<AnatomyModel> builder)
	{
		builder.Property(image => image.AnatomyId)
			   .IsRequired();

		builder.Property(image => image.ImagePath)
			   .IsRequired()
			   .HasMaxLength(256);

		builder.Property(image => image.BodyPart)
			   .IsRequired();
	}
}
