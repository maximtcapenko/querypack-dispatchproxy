namespace QueryPack.DispatchProxy.Impl
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Reflection;
    using Internal;

    internal class InterceptMethodOnExecutingProxy<TContext, TTarget> : IInterceptorProxy<TContext, TTarget>
        where TTarget : class
        where TContext : class
    {
        private readonly Delegate _delegate;
        private readonly MethodInfo _method;
        private readonly IMethodInvokerFactory<TTarget> _invokerFactory;
        private static ConcurrentDictionary<MethodInfo, Func<object, object[], object>> _interceptors = new ConcurrentDictionary<MethodInfo, Func<object, object[], object>>();

        public InterceptMethodOnExecutingProxy(MethodInfo method, Delegate @delegate, IMethodInvokerFactory<TTarget> invokerFactory)
        {
            _method = method;
            _delegate = @delegate;
            _invokerFactory = invokerFactory;
        }

        public bool CanIntercept(MethodInfo method) => _method == method;

        public object Intercept(TContext context, TTarget target, MethodInfo targetMethod, object[] args)
        {
            if (targetMethod == _method)
            {
                var invoker = _invokerFactory.Create(target, targetMethod, args);
                var method = _interceptors.GetOrAdd(targetMethod, (method) => MethodFactory.CreateGenericMethod<object>(_delegate.GetMethodInfo()));
                var list = new List<object> { context, target };
                list.AddRange(args);
                list.Add(invoker);

                return method(_delegate.Target, list.ToArray());
            }

            return targetMethod.Invoke(target, args);
        }
    }
}