namespace QueryPack.Reflection.Impl
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
        private Delegate _interceptor;

        public InterceptorProxyFactoryImpl(Expression method, Delegate interceptor)
        {
            var methodResolver = new MethodInfoResolver();
            _method = methodResolver.Resolve(method);
            _interceptor = interceptor;
        }

        public IInterceptorProxy<TContext, TTarget> Create()
        {
            return new InterceptMethodOnExecutedProxy<TContext, TTarget>(_method, _interceptor);
        }
    }
}