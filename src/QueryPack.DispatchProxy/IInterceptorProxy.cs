namespace QueryPack.DispatchProxy
{
    using System.Reflection;

    public interface IInterceptorProxy<TContext, T> 
        where TContext : class
        where T : class
    {
        bool CanIntercept(MethodInfo method);
        object Intercept(TContext context, T target, MethodInfo targetMethod, object[] args);
    }
}