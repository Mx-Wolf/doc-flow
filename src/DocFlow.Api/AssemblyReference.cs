using System.Reflection;

namespace DocFlow.Api;

public static class AssemblyReference
{
    public static Assembly Api => typeof(AssemblyReference).Assembly;
}
