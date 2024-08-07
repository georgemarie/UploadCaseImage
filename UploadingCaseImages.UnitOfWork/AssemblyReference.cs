using System.Reflection;

namespace UploadingCaseImages.UnitOfWorks;

public static class AssemblyReference
{
    public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;
}
