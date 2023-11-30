namespace QueryPack.DispatchProxy.Internal
{
    using System.Collections.Generic;
    using System.Reflection;

    internal class InterceptorProxy<TDependency, TTarget> : DispatchProxy
        where TDependency : class
        where TTarget : class
    {
        public TTarget Target { get; set; }
        public TDependency Context { get; set; }
        public IEnumerable<IInterceptorProxy<TDependency, TTarget>> Interceptors { get; set; }

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