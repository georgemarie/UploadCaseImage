using System.Reflection;
using Microsoft.EntityFrameworkCore;
using UploadingCaseImages.DB.Configurations;
using UploadingCaseImages.DB.Model;
using static System.Net.Mime.MediaTypeNames;

namespace UploadingCaseImages.DB;

public class UploadingCaseImagesContext : DbContext
{
	public DbSet<AnatomyModel> CaseImage { get; set; }

	public UploadingCaseImagesContext(DbContextOptions<UploadingCaseImagesContext> options)
		: base(options)
	{
	}

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		base.OnModelCreating(modelBuilder);
		modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
	}
}
