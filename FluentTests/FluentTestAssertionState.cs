using NUnit.Framework;
using NUnit.Framework.Internal;

namespace FluentTests;

public class FluentTestAssertionState<TInitialValue, TContextIn, TContextOut> : TestCaseData where TInitialValue : class
    where TContextIn : class
    where TContextOut : class
{
    public FluentTestAssertionState(string testName, Func<TContextIn, TContextOut>? executeTestStep,
        TInitialValue initialValue)
    {
        TestName = testName;
        ExecuteTestStep = executeTestStep;
        InitialValue = initialValue;

        if (typeof(TContextIn) == typeof(TInitialValue))
        {
            Properties = new PropertyBag();
            Properties.Add("FluentTestAction", new FluentTestAction { ExecuteTest = () => ExecuteTestStep(InitialValue as TContextIn) });
        }
    }

    public Func<TContextIn, TContextOut>? ExecuteTestStep { get; set; }
    public TInitialValue InitialValue { get; set; }
}