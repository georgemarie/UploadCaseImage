namespace UploadingCaseImages.Common.Extensions;

public static class ApplicationBuilderExtensions
{
	public static void UseSwaggerAndUI(this IApplicationBuilder app)
	{
		 app.UseSwagger();
		 app.UseSwaggerUI(c =>
		{
			c.SwaggerEndpoint("/swagger/v1/swagger.json", "UploadingCaseImages API V1");
			c.RoutePrefix = string.Empty;
		});
	}
}
