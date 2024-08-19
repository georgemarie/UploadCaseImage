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
		modelBuilder.Entity<Case>()
                .HasMany(c => c.ImageUrls)
                .WithOne(a => a.Case)
                .HasForeignKey(a => a.CaseId);

		base.OnModelCreating(modelBuilder);
		modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
	}
	public static void ConfigureDbContextOptions(DbContextOptionsBuilder optionsBuilder)
	{
		optionsBuilder.UseSqlServer("Server=TERY/terevenareda;Database=UploadCase;Trusted_Connection=True;MultipleActiveResultSets=true", b => b.MigrationsAssembly("UploadingCaseImages.DB"));
	}
}
