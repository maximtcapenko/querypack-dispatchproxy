namespace QueryPack.DispatchProxy
{
    /// <summary>
    /// Builds interceptor's factory
    /// </summary>
    /// <typeparam name="TDependency"></typeparam>
    /// <typeparam name="TTarget"></typeparam>
    public interface IInterceptorProxyFactoryBuilder<TDependency, TTarget>
     where TDependency : class
     where TTarget : class
    {
        /// <summary>
        /// Adds interceptor builder
        /// </summary>
        /// <param name="interceptorBuilder"><see cref="IInterceptorBuilder{TContext, TTarget}"/></param>
        void AddInterceptor(IInterceptorBuilder<TDependency, TTarget> interceptorBuilder);
    }
}