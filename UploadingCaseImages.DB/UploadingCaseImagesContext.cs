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
		modelBuilder.Entity<PatientCase>()
				.HasMany(pc => pc.CaseImages)
				.WithOne(ci => ci.PatientCase)
				.HasForeignKey(ci => ci.PatientCaseId)
				.OnDelete(DeleteBehavior.Cascade);

		modelBuilder.Entity<PatientCase>()
				.HasOne(pc => pc.Anatomy)
				.WithMany()
				.HasForeignKey(pc => pc.AnatomyId)
				.OnDelete(DeleteBehavior.Restrict);

		modelBuilder.Entity<PatientCase>()
		.	HasKey(pc => pc.PatientCaseId);

		modelBuilder.Entity<CaseImage>()
			.HasKey(ci => ci.CaseImageId);

		base.OnModelCreating(modelBuilder);
		modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
	}
	public static void ConfigureDbContextOptions(DbContextOptionsBuilder optionsBuilder)
	{
		optionsBuilder.UseSqlServer("Server=TERY\\SQLEXPRESS;Database=UploadingCaseImages;Trusted_Connection=True;MultipleActiveResultSets=true", b => b.MigrationsAssembly("UploadingCaseImages.DB"));
	}
}
