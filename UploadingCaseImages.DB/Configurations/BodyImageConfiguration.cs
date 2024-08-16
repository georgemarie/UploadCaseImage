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
internal class BodyImageConfiguration : IEntityTypeConfiguration<BodyImageModel>
{
	public void Configure(EntityTypeBuilder<BodyImageModel> builder)
	{
		builder.Property(image => image.Id)
			   .IsRequired();

		builder.Property(image => image.ImagePath)
			   .IsRequired();

		builder.Property(image => image.BodyPart)
			   .IsRequired();
	}
}
