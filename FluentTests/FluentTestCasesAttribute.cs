using System.Reflection;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using NUnit.Framework.Internal;
using NUnit.Framework.Internal.Builders;

namespace FluentTests;

[AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
public class FluentTestCasesAttribute : Attribute
{
}
