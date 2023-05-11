namespace QueryPack.DispatchProxy.Internal
{
    using System.Collections.Generic;
    using System.Reflection;

    internal class InterceptorProxy<TContext, TTarget> : DispatchProxy
        where TContext : class
        where TTarget : class
    {
        public TTarget Target { get; set; }
        public TContext Context { get; set; }
        public IEnumerable<IInterceptorProxy<TContext, TTarget>> Interceptors { get; set; }

        protected override object Invoke(MethodInfo targetMethod, object[] args)
        {
            foreach (var interceptor in Interceptors)
            {
                if (interceptor.CanIntercept(targetMethod))
                {
                    return interceptor.Intercept(Context, Target, targetMethod, args);
                }
            }

            return targetMethod.Invoke(Target, args);
        }
    }
}