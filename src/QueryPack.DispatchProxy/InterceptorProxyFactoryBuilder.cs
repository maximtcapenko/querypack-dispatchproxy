namespace QueryPack.DispatchProxy
{
    /// <summary>
    /// Builds interceptor's factory
    /// </summary>
    /// <typeparam name="TContext"></typeparam>
    /// <typeparam name="TTarget"></typeparam>
    public interface InterceptorProxyFactoryBuilder<TContext, TTarget>
     where TContext : class
     where TTarget : class
    {
        /// <summary>
        /// Adds interceptor builder
        /// </summary>
        /// <param name="interceptorBuilder"><see cref="IInterceptorBuilder{TContext, TTarget}"/></param>
        void AddInterceptor(IInterceptorBuilder<TContext, TTarget> interceptorBuilder);
    }
}