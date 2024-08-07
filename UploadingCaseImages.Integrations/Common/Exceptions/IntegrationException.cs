namespace UploadingCaseImages.Integrations.Common.Exceptions;
public class IntegrationException : Exception
{
	public IntegrationException()
	{
	}

	public IntegrationException(string message)
		: base(message)
	{
	}

	public IntegrationException(string message, Exception inner)
		: base(message, inner)
	{
	}
}
