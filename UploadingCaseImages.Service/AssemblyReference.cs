using System.Reflection;

namespace UploadingCaseImages.Service;

public static class AssemblyReference
{
	public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;
}
