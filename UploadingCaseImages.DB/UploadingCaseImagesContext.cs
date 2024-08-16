using System.Reflection;
using Microsoft.EntityFrameworkCore;
using UploadingCaseImages.DB.Configurations;

namespace UploadingCaseImages.DB;

public class UploadingCaseImagesContext : DbContext
{
	public UploadingCaseImagesContext(DbContextOptions<UploadingCaseImagesContext> options)
		: base(options)
	{
	}

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		base.OnModelCreating(modelBuilder);

		modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
		modelBuilder.ApplyConfiguration(new BodyImageConfiguration());
	}
}
