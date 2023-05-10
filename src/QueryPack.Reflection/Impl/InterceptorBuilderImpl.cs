namespace QueryPack.Reflection.Impl
{
    using System;
    using System.Linq.Expressions;
    using Microsoft.Extensions.DependencyInjection;

    internal class InterceptorBuilderImpl<TContext, TTarget> : IInterceptorBuilder<TContext, TTarget>
        where TContext : class
        where TTarget : class
    {
        private readonly IServiceCollection _services;

        public InterceptorBuilderImpl(IServiceCollection services)
        {
            _services = services;
        }

        public IInterceptorBuilder<TContext, TTarget> InterceptMethodOnExecuted<TIn, TOut>(Expression<Func<TTarget, Func<TIn, TOut>>> method, Func<TContext, TTarget, TIn, TOut, TOut> interceptor)
        {
            _services.AddSingleton<IInterceptorProxyFactory<TContext, TTarget>>(new InterceptorProxyFactoryImpl<TContext, TTarget>(method, interceptor));
            return this;
        }

        public IInterceptorBuilder<TContext, TTarget> InterceptMethodOnExecuted<TIn1, TIn2, TOut>(Expression<Func<TTarget, Func<TIn1, TIn2, TOut>>> method, Func<TContext, TTarget, TIn1, TIn2, TOut, TOut> interceptor)
        {
            _services.AddSingleton<IInterceptorProxyFactory<TContext, TTarget>>(new InterceptorProxyFactoryImpl<TContext, TTarget>(method, interceptor));
            return this;
        }

        public IInterceptorBuilder<TContext, TTarget> InterceptMethodOnExecuted<TIn1, TIn2, TIn3, TOut>(Expression<Func<TTarget, Func<TIn1, TIn2, TIn3, TOut>>> method, Func<TContext, TTarget, TIn1, TIn2, TIn3, TOut, TOut> interceptor)
        {
            _services.AddSingleton<IInterceptorProxyFactory<TContext, TTarget>>(new InterceptorProxyFactoryImpl<TContext, TTarget>(method, interceptor));
            return this;
        }

        public IInterceptorBuilder<TContext, TTarget> InterceptMethodOnExecuted<TIn1, TIn2, TIn3, TIn4, TOut>(Expression<Func<TTarget, Func<TIn1, TIn2, TIn3, TIn4, TOut>>> method, Func<TContext, TTarget, TIn1, TIn2, TIn3, TIn4, TOut, TOut> interceptor)
        {
            _services.AddSingleton<IInterceptorProxyFactory<TContext, TTarget>>(new InterceptorProxyFactoryImpl<TContext, TTarget>(method, interceptor));
            return this;
        }

        public IInterceptorBuilder<TContext, TTarget> InterceptMethodOnExecuted<TIn1, TIn2, TIn3, TIn4, TIn5, TOut>(Expression<Func<TTarget, Func<TIn1, TIn2, TIn3, TIn4, TIn5, TOut>>> method, Func<TContext, TTarget, TIn1, TIn2, TIn3, TIn4, TIn5, TOut, TOut> interceptor)
        {
            _services.AddSingleton<IInterceptorProxyFactory<TContext, TTarget>>(new InterceptorProxyFactoryImpl<TContext, TTarget>(method, interceptor));
            return this;
        }

        public IInterceptorBuilder<TContext, TTarget> InterceptMethodOnExecuted<TIn1, TIn2, TIn3, TIn4, TIn5, TIn6, TOut>(Expression<Func<TTarget, Func<TIn1, TIn2, TIn3, TIn4, TIn5, TIn6, TOut>>> method, Func<TContext, TTarget, TIn1, TIn2, TIn3, TIn4, TIn5, TIn6, TOut, TOut> interceptor)
        {
            throw new NotImplementedException();
        }

        public IInterceptorBuilder<TContext, TTarget> InterceptMethodOnExecuted<TIn1, TIn2, TIn3, TIn4, TIn5, TIn6, TIn7, TOut>(Expression<Func<TTarget, Func<TIn1, TIn2, TIn3, TIn4, TIn5, TIn6, TIn7, TOut>>> method, Func<TContext, TTarget, TIn1, TIn2, TIn3, TIn4, TIn5, TIn6, TIn7, TOut, TOut> interceptor)
        {
            _services.AddSingleton<IInterceptorProxyFactory<TContext, TTarget>>(new InterceptorProxyFactoryImpl<TContext, TTarget>(method, interceptor));
            return this;
        }

        public IInterceptorBuilder<TContext, TTarget> InterceptMethodOnExecuted<TIn1, TIn2, TIn3, TIn4, TIn5, TIn6, TIn7, TIn8, TOut>(Expression<Func<TTarget, Func<TIn1, TIn2, TIn3, TIn4, TIn5, TIn6, TIn7, TIn8, TOut>>> method, Func<TContext, TTarget, TIn1, TIn2, TIn3, TIn4, TIn5, TIn6, TIn7, TIn8, TOut, TOut> interceptor)
        {
            _services.AddSingleton<IInterceptorProxyFactory<TContext, TTarget>>(new InterceptorProxyFactoryImpl<TContext, TTarget>(method, interceptor));
            return this;
        }

        public IInterceptorBuilder<TContext, TTarget> InterceptMethodOnExecuted<TIn1, TIn2, TIn3, TIn4, TIn5, TIn6, TIn7, TIn8, TIn9, TOut>(Expression<Func<TTarget, Func<TIn1, TIn2, TIn3, TIn4, TIn5, TIn6, TIn7, TIn8, TIn9, TOut>>> method, Func<TContext, TTarget, TIn1, TIn2, TIn3, TIn4, TIn5, TIn6, TIn7, TIn8, TIn9, TOut, TOut> interceptor)
        {
            _services.AddSingleton<IInterceptorProxyFactory<TContext, TTarget>>(new InterceptorProxyFactoryImpl<TContext, TTarget>(method, interceptor));
            return this;
        }

        public IInterceptorBuilder<TContext, TTarget> InterceptMethodOnExecuted<TIn1, TIn2, TIn3, TIn4, TIn5, TIn6, TIn7, TIn8, TIn9, TIn10, TOut>(Expression<Func<TTarget, Func<TIn1, TIn2, TIn3, TIn4, TIn5, TIn6, TIn7, TIn8, TIn9, TIn10, TOut>>> method, Func<TContext, TTarget, TIn1, TIn2, TIn3, TIn4, TIn5, TIn6, TIn7, TIn8, TIn9, TIn10, TOut, TOut> interceptor)
        {
            _services.AddSingleton<IInterceptorProxyFactory<TContext, TTarget>>(new InterceptorProxyFactoryImpl<TContext, TTarget>(method, interceptor));
            return this;
        }

        public IInterceptorBuilder<TContext, TTarget> InterceptMethodOnExecuted<TIn1, TIn2, TIn3, TIn4, TIn5, TIn6, TIn7, TIn8, TIn9, TIn10, TIn11, TOut>(Expression<Func<TTarget, Func<TIn1, TIn2, TIn3, TIn4, TIn5, TIn6, TIn7, TIn8, TIn9, TIn10, TIn11, TOut>>> method, Func<TContext, TTarget, TIn1, TIn2, TIn3, TIn4, TIn5, TIn6, TIn7, TIn8, TIn9, TIn10, TIn11, TOut, TOut> interceptor)
        {
            _services.AddSingleton<IInterceptorProxyFactory<TContext, TTarget>>(new InterceptorProxyFactoryImpl<TContext, TTarget>(method, interceptor));
            return this;
        }

        public IInterceptorBuilder<TContext, TTarget> InterceptMethodOnExecuted<TIn1, TIn2, TIn3, TIn4, TIn5, TIn6, TIn7, TIn8, TIn9, TIn10, TIn11, TIn12, TOut>(Expression<Func<TTarget, Func<TIn1, TIn2, TIn3, TIn4, TIn5, TIn6, TIn7, TIn8, TIn9, TIn10, TIn11, TIn12, TOut>>> method, Func<TContext, TTarget, TIn1, TIn2, TIn3, TIn4, TIn5, TIn6, TIn7, TIn8, TIn9, TIn10, TIn11, TIn12, TOut, TOut> interceptor)
        {
            _services.AddSingleton<IInterceptorProxyFactory<TContext, TTarget>>(new InterceptorProxyFactoryImpl<TContext, TTarget>(method, interceptor));
            return this;
        }
    }
}