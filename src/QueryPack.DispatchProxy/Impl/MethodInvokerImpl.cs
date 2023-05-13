namespace QueryPack.DispatchProxy.Impl
{
    using System;
    using System.Collections.Concurrent;
    using System.Reflection;
    using Internal;

    internal class MethodInvokerImpl<TTarget, TResult> : IMethodInvoker<TResult>
        where TTarget : class
    {
        private readonly TTarget _target;
        private readonly object[] _args;
        private readonly MethodInfo _method;

        public string MethodName => _method.Name;
        private static ConcurrentDictionary<MethodInfo, Delegate> _interceptors = new ConcurrentDictionary<MethodInfo, Delegate>();

        public MethodInvokerImpl(TTarget target, MethodInfo method, params object[] args)
        {
            _method = method;
            _target = target;
            _args = args;
        }

        public TResult Invoke()
        {
            var method = (Func<object, object[], TResult>)_interceptors.GetOrAdd(_method, method => MethodFactory.CreateGenericMethod<TResult>(_method));
            return method(_target, _args);
        }
    }
}