namespace QueryPack.Reflection.Impl
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Reflection;

    internal class InterceptMethodOnExecutedProxy<TContext, TTarget> : IInterceptorProxy<TContext, TTarget>
        where TTarget : class
        where TContext : class
    {
        private readonly Delegate _delegate;
        private readonly MethodInfo _method;
        private static ConcurrentDictionary<MethodInfo, Func<object, object[], object>> _interceptors = new ConcurrentDictionary<MethodInfo, Func<object, object[], object>>();

        public InterceptMethodOnExecutedProxy(MethodInfo method, Delegate @delegate)
        {
            _method = method;
            _delegate = @delegate;
        }

        public bool CanIntercept(MethodInfo method) => _method == method;

        public object Intercept(TContext context, TTarget target, MethodInfo targetMethod, object[] args)
        {
            var result = targetMethod.Invoke(target, args);
            if (targetMethod == _method)
            {
                var method = _interceptors.GetOrAdd(targetMethod, (method) => MethodFactory.CreateGenericMethod<object>(_delegate.GetMethodInfo()));
                var list = new List<object> { context, target };
                list.AddRange(args);
                list.Add(result);

                return method(_delegate.Target, list.ToArray());
            }

            return result;
        }
    }
}