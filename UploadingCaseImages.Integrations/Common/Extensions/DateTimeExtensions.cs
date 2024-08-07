namespace UploadingCaseImages.Integrations.Common.Extensions;

public static class DateTimeExtensions
{
	public static DateTime GetCurrentLocalTime(this DateTime dateTime)
	{
		return dateTime.AddHours(2);
	}
}
