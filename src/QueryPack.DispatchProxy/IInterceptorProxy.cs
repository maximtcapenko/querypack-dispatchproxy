namespace QueryPack.DispatchProxy
{
    using System.Reflection;

    /// <summary>
    /// Interceptor Proxy
    /// </summary>
    /// <typeparam name="TContext"></typeparam>
    /// <typeparam name="T"></typeparam>
    public interface IInterceptorProxy<TContext, T> 
        where TContext : class
        where T : class
    {
        /// <summary>
        /// Checks if current interceptor is able to intercept target method
        /// </summary>
        /// <param name="method"></param>
        bool CanIntercept(MethodInfo method);
        /// <summary>
        /// Intercepts target method
        /// </summary>
        /// <param name="context"></param>
        /// <param name="target"></param>
        /// <param name="targetMethod"></param>
        /// <param name="args"></param>
        object Intercept(TContext context, T target, MethodInfo targetMethod, object[] args);
    }
}