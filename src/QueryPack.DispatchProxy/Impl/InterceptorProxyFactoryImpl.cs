namespace QueryPack.DispatchProxy.Impl
{
    using System;
    using System.Linq.Expressions;
    using System.Reflection;
    using Internal;

    internal class InterceptorProxyFactoryImpl<TContext, TTarget> :
        IInterceptorProxyFactory<TContext, TTarget>
        where TContext : class
        where TTarget : class
    {
        private readonly MethodInfo _method;
        private readonly Delegate _interceptor;
        private readonly IMethodInvokerFactory<TTarget> _invokerFactory;

        public InterceptorProxyFactoryImpl(Expression method, Delegate interceptor, IMethodInvokerFactory<TTarget> invokerFactory)
        {
            _method = MethodInfoResolver.Resolve<TTarget>(method);
            _interceptor = interceptor;
            _invokerFactory = invokerFactory;
        }

        public IInterceptorProxy<TContext, TTarget> Create() 
            => new InterceptMethodOnExecutingProxy<TContext, TTarget>(_method, _interceptor, _invokerFactory);
    }
}