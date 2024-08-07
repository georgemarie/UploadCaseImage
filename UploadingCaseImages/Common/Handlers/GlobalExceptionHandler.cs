using Microsoft.AspNetCore.Diagnostics;
using UploadingCaseImages.Service;
using UploadingCaseImages.Service.Resources;
using UploadingCaseImages.Service.Utilities;
using System.Net;

namespace UploadingCaseImages.Common.Handlers;

public class GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger, IWebHostEnvironment webHostEnvironment) : IExceptionHandler
{
	public async ValueTask<bool> TryHandleAsync(
		HttpContext httpContext,
		Exception exception,
		CancellationToken cancellationToken)
	{
		logger.LogError(
			"Exception Message: {Message}, StackTrace: {StackTrace}",
			exception.Message,
			exception.StackTrace ?? string.Empty);

		httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

		var errorMessage = EnvironmentsChecker.IsInDevelopmentMode(webHostEnvironment)
			? $"Exception Message: {exception.Message}, \n " +
				$"Inner Exception Message: {exception.InnerException?.Message}, \n" +
				$"Stack Trace: {exception.StackTrace}"
			: Shared.TechnicalFailure;

		var exceptionFailure = GenericResponseModel<string>.Failure(exception.Message, new List<ErrorResponseModel>
		{
			new()
			{
				Message = errorMessage,
				PropertyName = "Exception"
			}
		});

		await httpContext.Response.WriteAsJsonAsync(exceptionFailure);

		return true;
	}
}
