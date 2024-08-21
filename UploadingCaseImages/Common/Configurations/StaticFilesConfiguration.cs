using Microsoft.Extensions.FileProviders;

namespace UploadingCaseImages.Common.Configurations;

public static class StaticFilesConfiguration
{
	public static void ConfigureStaticFiles(WebApplication app)
	{
		var caseImagesPath = Path.Combine(Directory.GetCurrentDirectory(), "CaseImages");

		// Ensure the directory exists
		if (!Directory.Exists(caseImagesPath))
		{
			Directory.CreateDirectory(caseImagesPath);
		}

		app.UseStaticFiles(new StaticFileOptions
		{
			FileProvider = new PhysicalFileProvider(caseImagesPath),
			RequestPath = "/CaseImages"
		});
	}
}
