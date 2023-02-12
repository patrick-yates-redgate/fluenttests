namespace FluentTests.Context;

public interface IFluentTestContextAction<out TContext> : IFluentTestContext<TContext>
{
}