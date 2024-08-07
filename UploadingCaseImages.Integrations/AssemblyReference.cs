using System.Reflection;

namespace UploadingCaseImages.Integrations;

public static class AssemblyReference
{
    public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;
}
