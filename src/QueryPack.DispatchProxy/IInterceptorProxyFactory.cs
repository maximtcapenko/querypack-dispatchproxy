namespace QueryPack.DispatchProxy
{
    public interface IInterceptorProxyFactory<TContext, TTarget> 
        where TContext :class
        where TTarget : class
    {
        IInterceptorProxy<TContext, TTarget> Create();
    }
}