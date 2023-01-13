using FluentAssertions.Primitives;
using NUnit.Framework;

namespace FluentTests;

public class FluentTestShouldState<TContextIn> : TestCaseData where TContextIn : class
{
    public Func<TContextIn, ObjectAssertions>? ExecuteTestStep { get; set; }
    public TContextIn InitialValue { get; set; }
}