namespace FluentTests.Context;

public interface IFluentTestContext<out TContext>
{
    TContext GetThis();
}