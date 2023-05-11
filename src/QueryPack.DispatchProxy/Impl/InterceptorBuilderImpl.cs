namespace QueryPack.DispatchProxy.Impl
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Microsoft.Extensions.DependencyInjection;

    internal class InterceptorBuilderImpl<TContext, TTarget> : IInterceptorBuilder<TContext, TTarget>
        where TContext : class
        where TTarget : class
    {
        private readonly IServiceCollection _services;
        private List<Expression> _registeredInterceptors = new List<Expression>();

        public InterceptorBuilderImpl(IServiceCollection services)
        {
            _services = services;
        }

        private IInterceptorBuilder<TContext, TTarget> Register<TOut>(Expression method, Delegate interceptor)
        {
            if (!_registeredInterceptors.Contains(method))
            {
                _services.AddSingleton<IInterceptorProxyFactory<TContext, TTarget>>(new InterceptorProxyFactoryImpl<TContext, TTarget>(method, interceptor, new MethodInvokerFactoryImpl<TTarget, TOut>()));
                _registeredInterceptors.Add(method);
            }

            return this;
        }

        public IInterceptorBuilder<TContext, TTarget> OnMethodExecuting<TIn, TOut>(Expression<Func<TTarget, Func<TIn, TOut>>> method, Func<TContext, TTarget, TIn, TOut, TOut> interceptor)
            => Register<TOut>(method, interceptor);

        public IInterceptorBuilder<TContext, TTarget> OnMethodExecuting<TIn1, TIn2, TOut>(Expression<Func<TTarget, Func<TIn1, TIn2, TOut>>> method, Func<TContext, TTarget, TIn1, TIn2, TOut, TOut> interceptor)
            => Register<TOut>(method, interceptor);


        public IInterceptorBuilder<TContext, TTarget> OnMethodExecuting<TIn1, TIn2, TIn3, TOut>(Expression<Func<TTarget, Func<TIn1, TIn2, TIn3, TOut>>> method, Func<TContext, TTarget, TIn1, TIn2, TIn3, IMethodInvoker<TOut>, TOut> interceptor)
            => Register<TOut>(method, interceptor);


        public IInterceptorBuilder<TContext, TTarget> OnMethodExecuting<TIn1, TIn2, TIn3, TIn4, TOut>(Expression<Func<TTarget, Func<TIn1, TIn2, TIn3, TIn4, TOut>>> method, Func<TContext, TTarget, TIn1, TIn2, TIn3, TIn4, IMethodInvoker<TOut>, TOut> interceptor)
            => Register<TOut>(method, interceptor);


        public IInterceptorBuilder<TContext, TTarget> OnMethodExecuting<TIn1, TIn2, TIn3, TIn4, TIn5, TOut>(Expression<Func<TTarget, Func<TIn1, TIn2, TIn3, TIn4, TIn5, TOut>>> method, Func<TContext, TTarget, TIn1, TIn2, TIn3, TIn4, TIn5, IMethodInvoker<TOut>, TOut> interceptor)
            => Register<TOut>(method, interceptor);

        public IInterceptorBuilder<TContext, TTarget> OnMethodExecuting<TIn1, TIn2, TIn3, TIn4, TIn5, TIn6, TOut>(Expression<Func<TTarget, Func<TIn1, TIn2, TIn3, TIn4, TIn5, TIn6, TOut>>> method, Func<TContext, TTarget, TIn1, TIn2, TIn3, TIn4, TIn5, TIn6, IMethodInvoker<TOut>, TOut> interceptor)
            => Register<TOut>(method, interceptor);


        public IInterceptorBuilder<TContext, TTarget> OnMethodExecuting<TIn1, TIn2, TIn3, TIn4, TIn5, TIn6, TIn7, TOut>(Expression<Func<TTarget, Func<TIn1, TIn2, TIn3, TIn4, TIn5, TIn6, TIn7, TOut>>> method, Func<TContext, TTarget, TIn1, TIn2, TIn3, TIn4, TIn5, TIn6, TIn7, IMethodInvoker<TOut>, TOut> interceptor)
            => Register<TOut>(method, interceptor);


        public IInterceptorBuilder<TContext, TTarget> OnMethodExecuting<TIn1, TIn2, TIn3, TIn4, TIn5, TIn6, TIn7, TIn8, TOut>(Expression<Func<TTarget, Func<TIn1, TIn2, TIn3, TIn4, TIn5, TIn6, TIn7, TIn8, TOut>>> method, Func<TContext, TTarget, TIn1, TIn2, TIn3, TIn4, TIn5, TIn6, TIn7, TIn8, IMethodInvoker<TOut>, TOut> interceptor)
            => Register<TOut>(method, interceptor);


        public IInterceptorBuilder<TContext, TTarget> OnMethodExecuting<TIn1, TIn2, TIn3, TIn4, TIn5, TIn6, TIn7, TIn8, TIn9, TOut>(Expression<Func<TTarget, Func<TIn1, TIn2, TIn3, TIn4, TIn5, TIn6, TIn7, TIn8, TIn9, TOut>>> method, Func<TContext, TTarget, TIn1, TIn2, TIn3, TIn4, TIn5, TIn6, TIn7, TIn8, TIn9, IMethodInvoker<TOut>, TOut> interceptor)
            => Register<TOut>(method, interceptor);


        public IInterceptorBuilder<TContext, TTarget> OnMethodExecuting<TIn1, TIn2, TIn3, TIn4, TIn5, TIn6, TIn7, TIn8, TIn9, TIn10, TOut>(Expression<Func<TTarget, Func<TIn1, TIn2, TIn3, TIn4, TIn5, TIn6, TIn7, TIn8, TIn9, TIn10, TOut>>> method, Func<TContext, TTarget, TIn1, TIn2, TIn3, TIn4, TIn5, TIn6, TIn7, TIn8, TIn9, TIn10, IMethodInvoker<TOut>, TOut> interceptor)
            => Register<TOut>(method, interceptor);


        public IInterceptorBuilder<TContext, TTarget> OnMethodExecuting<TIn1, TIn2, TIn3, TIn4, TIn5, TIn6, TIn7, TIn8, TIn9, TIn10, TIn11, TOut>(Expression<Func<TTarget, Func<TIn1, TIn2, TIn3, TIn4, TIn5, TIn6, TIn7, TIn8, TIn9, TIn10, TIn11, TOut>>> method, Func<TContext, TTarget, TIn1, TIn2, TIn3, TIn4, TIn5, TIn6, TIn7, TIn8, TIn9, TIn10, TIn11, IMethodInvoker<TOut>, TOut> interceptor)
            => Register<TOut>(method, interceptor);


        public IInterceptorBuilder<TContext, TTarget> OnMethodExecuting<TIn1, TIn2, TIn3, TIn4, TIn5, TIn6, TIn7, TIn8, TIn9, TIn10, TIn11, TIn12, TOut>(Expression<Func<TTarget, Func<TIn1, TIn2, TIn3, TIn4, TIn5, TIn6, TIn7, TIn8, TIn9, TIn10, TIn11, TIn12, TOut>>> method, Func<TContext, TTarget, TIn1, TIn2, TIn3, TIn4, TIn5, TIn6, TIn7, TIn8, TIn9, TIn10, TIn11, TIn12, IMethodInvoker<TOut>, TOut> interceptor)
            => Register<TOut>(method, interceptor);

        public IInterceptorBuilder<TContext, TTarget> OnMethodExecuting<TIn1, TIn2, TIn3, TIn4, TIn5, TIn6, TIn7, TIn8, TIn9, TIn10, TIn11, TIn12, TIn13, TOut>(Expression<Func<TTarget, Func<TIn1, TIn2, TIn3, TIn4, TIn5, TIn6, TIn7, TIn8, TIn9, TIn10, TIn11, TIn12, TIn13, TOut>>> method, Func<TContext, TTarget, TIn1, TIn2, TIn3, TIn4, TIn5, TIn6, TIn7, TIn8, TIn9, TIn10, TIn11, TIn12, TIn13, IMethodInvoker<TOut>, TOut> interceptor)
            => Register<TOut>(method, interceptor);
    }
}