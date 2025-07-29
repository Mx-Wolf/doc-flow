using System.Reflection;

namespace DocFlow.Application;

public static class AssemblyReference
{
    public static readonly Assembly Application = typeof(AssemblyReference).Assembly;
}