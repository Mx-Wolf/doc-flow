using System.Reflection;

namespace DocFlow.Infrastructure;

public static class AssemblyReference
{
    public static Assembly Infrastructure => typeof(AssemblyReference).Assembly;
}
