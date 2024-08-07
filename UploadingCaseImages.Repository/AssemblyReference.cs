using System.Reflection;

namespace UploadingCaseImages.Repository;

public static class AssemblyReference
{
    public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;
}
