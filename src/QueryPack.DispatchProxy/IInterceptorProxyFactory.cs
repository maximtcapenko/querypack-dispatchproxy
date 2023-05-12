namespace QueryPack.DispatchProxy
{
    /// <summary>
    /// Interceptor Proxy Factory
    /// </summary>
    /// <typeparam name="TContext"></typeparam>
    /// <typeparam name="TTarget"></typeparam>
    public interface IInterceptorProxyFactory<TContext, TTarget> 
        where TContext :class
        where TTarget : class
    {
        /// <summary>
        /// Creates instance of <see cref="IInterceptorProxy{TContext, T}"/>
        /// </summary>
        IInterceptorProxy<TContext, TTarget> Create();
    }
}