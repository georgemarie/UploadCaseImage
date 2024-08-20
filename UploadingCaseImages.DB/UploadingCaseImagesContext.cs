using System.Reflection;
using Microsoft.EntityFrameworkCore;
using UploadingCaseImages.DB.Model;
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
	}
	public static void ConfigureDbContextOptions(DbContextOptionsBuilder optionsBuilder)
	{
		optionsBuilder.UseSqlServer("Server=TERY\\SQLEXPRESS;Database=UploadingCaseImages;Trusted_Connection=True;MultipleActiveResultSets=true", b => b.MigrationsAssembly("UploadingCaseImages.DB"));
	}
}
