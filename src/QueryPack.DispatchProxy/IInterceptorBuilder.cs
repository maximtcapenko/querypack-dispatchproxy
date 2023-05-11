namespace QueryPack.DispatchProxy
{
    using System;
    using System.Linq.Expressions;

    /// <summary>
    /// Registers interceptors for a target type
    /// </summary>
    /// <typeparam name="TContext">Interception context</typeparam>
    /// <typeparam name="TTarget">Type of intercepted target</typeparam>
    public interface IInterceptorBuilder<TContext, TTarget> where TContext : class
       where TTarget : class
    {
        /// <summary>
        /// Registers an interceptor for the method that is executed when the method is executed
        /// </summary>
        /// <typeparam name="TIn"></typeparam>
        /// <typeparam name="TOut"></typeparam>
        /// <param name="method">Intercepted method</param>
        /// <param name="interceptor">Interceptor method</param>
        IInterceptorBuilder<TContext, TTarget> OnMethodExecuting<TIn, TOut>(Expression<Func<TTarget, Func<TIn, TOut>>> method,
            Func<TContext, TTarget, TIn, TOut, TOut> interceptor);
        /// <summary>
        /// Registers an interceptor for the method that is executed when the method is executed
        /// </summary>
        /// <typeparam name="TIn1"></typeparam>
        /// <typeparam name="TIn2"></typeparam>
        /// <typeparam name="TOut"></typeparam>
        /// <param name="method">Intercepted method</param>
        /// <param name="interceptor">Interceptor method</param>
        IInterceptorBuilder<TContext, TTarget> OnMethodExecuting<TIn1, TIn2, TOut>(Expression<Func<TTarget, Func<TIn1, TIn2, TOut>>> method,
            Func<TContext, TTarget, TIn1, TIn2, TOut, TOut> interceptor);
        /// <summary>
        /// Registers an interceptor for the method that is executed when the method is executed
        /// </summary>
        /// <typeparam name="TIn1"></typeparam>
        /// <typeparam name="TIn2"></typeparam>
        /// <typeparam name="TIn3"></typeparam>
        /// <typeparam name="TOut"></typeparam>
        /// <param name="method">Intercepted method</param>
        /// <param name="interceptor">Interceptor method</param>
        IInterceptorBuilder<TContext, TTarget> OnMethodExecuting<TIn1, TIn2, TIn3, TOut>(Expression<Func<TTarget, Func<TIn1, TIn2, TIn3, TOut>>> method,
            Func<TContext, TTarget, TIn1, TIn2, TIn3, IMethodInvoker<TOut>, TOut> interceptor);
        /// <summary>
        /// Registers an interceptor for the method that is executed when the method is executed
        /// </summary>
        /// <typeparam name="TIn1"></typeparam>
        /// <typeparam name="TIn2"></typeparam>
        /// <typeparam name="TIn3"></typeparam>
        /// <typeparam name="TIn4"></typeparam>
        /// <typeparam name="TOut"></typeparam>
        /// <param name="method">Intercepted method</param>
        /// <param name="interceptor">Interceptor method</param>
        IInterceptorBuilder<TContext, TTarget> OnMethodExecuting<TIn1, TIn2, TIn3, TIn4, TOut>(Expression<Func<TTarget, Func<TIn1, TIn2, TIn3, TIn4, TOut>>> method,
            Func<TContext, TTarget, TIn1, TIn2, TIn3, TIn4, IMethodInvoker<TOut>, TOut> interceptor);
        /// <summary>
        /// Registers an interceptor for the method that is executed when the method is executed
        /// </summary>
        /// <typeparam name="TIn1"></typeparam>
        /// <typeparam name="TIn2"></typeparam>
        /// <typeparam name="TIn3"></typeparam>
        /// <typeparam name="TIn4"></typeparam>
        /// <typeparam name="TIn5"></typeparam>
        /// <typeparam name="TOut"></typeparam>
        /// <param name="method">Intercepted method</param>
        /// <param name="interceptor">Interceptor method</param>
        IInterceptorBuilder<TContext, TTarget> OnMethodExecuting<TIn1, TIn2, TIn3, TIn4, TIn5, TOut>(Expression<Func<TTarget, Func<TIn1, TIn2, TIn3, TIn4, TIn5, TOut>>> method,
            Func<TContext, TTarget, TIn1, TIn2, TIn3, TIn4, TIn5, IMethodInvoker<TOut>, TOut> interceptor);
        /// <summary>
        /// Registers an interceptor for the method that is executed when the method is executed
        /// </summary>
        /// <typeparam name="TIn1"></typeparam>
        /// <typeparam name="TIn2"></typeparam>
        /// <typeparam name="TIn3"></typeparam>
        /// <typeparam name="TIn4"></typeparam>
        /// <typeparam name="TIn5"></typeparam>
        /// <typeparam name="TIn6"></typeparam>
        /// <typeparam name="TOut"></typeparam>
        /// <param name="method">Intercepted method</param>
        /// <param name="interceptor">Interceptor method</param>
        IInterceptorBuilder<TContext, TTarget> OnMethodExecuting<TIn1, TIn2, TIn3, TIn4, TIn5, TIn6, TOut>(Expression<Func<TTarget, Func<TIn1, TIn2, TIn3, TIn4, TIn5, TIn6, TOut>>> method,
            Func<TContext, TTarget, TIn1, TIn2, TIn3, TIn4, TIn5, TIn6, IMethodInvoker<TOut>, TOut> interceptor);
        /// <summary>
        /// Registers an interceptor for the method that is executed when the method is executed
        /// </summary>
        /// <typeparam name="TIn1"></typeparam>
        /// <typeparam name="TIn2"></typeparam>
        /// <typeparam name="TIn3"></typeparam>
        /// <typeparam name="TIn4"></typeparam>
        /// <typeparam name="TIn5"></typeparam>
        /// <typeparam name="TIn6"></typeparam>
        /// <typeparam name="TIn7"></typeparam>
        /// <typeparam name="TOut"></typeparam>
        /// <param name="method">Intercepted method</param>
        /// <param name="interceptor">Interceptor method</param>
        IInterceptorBuilder<TContext, TTarget> OnMethodExecuting<TIn1, TIn2, TIn3, TIn4, TIn5, TIn6, TIn7, TOut>(Expression<Func<TTarget, Func<TIn1, TIn2, TIn3, TIn4, TIn5, TIn6, TIn7, TOut>>> method,
            Func<TContext, TTarget, TIn1, TIn2, TIn3, TIn4, TIn5, TIn6, TIn7, IMethodInvoker<TOut>, TOut> interceptor);
        /// <summary>
        /// Registers an interceptor for the method that is executed when the method is executed
        /// </summary>
        /// <typeparam name="TIn1"></typeparam>
        /// <typeparam name="TIn2"></typeparam>
        /// <typeparam name="TIn3"></typeparam>
        /// <typeparam name="TIn4"></typeparam>
        /// <typeparam name="TIn5"></typeparam>
        /// <typeparam name="TIn6"></typeparam>
        /// <typeparam name="TIn7"></typeparam>
        /// <typeparam name="TIn8"></typeparam>
        /// <typeparam name="TOut"></typeparam>
        /// <param name="method">Intercepted method</param>
        /// <param name="interceptor">Interceptor method</param>
        IInterceptorBuilder<TContext, TTarget> OnMethodExecuting<TIn1, TIn2, TIn3, TIn4, TIn5, TIn6, TIn7, TIn8, TOut>(Expression<Func<TTarget, Func<TIn1, TIn2, TIn3, TIn4, TIn5, TIn6, TIn7, TIn8, TOut>>> method,
            Func<TContext, TTarget, TIn1, TIn2, TIn3, TIn4, TIn5, TIn6, TIn7, TIn8, IMethodInvoker<TOut>, TOut> interceptor);
        /// <summary>
        /// Registers an interceptor for the method that is executed when the method is executed
        /// </summary>
        /// <typeparam name="TIn1"></typeparam>
        /// <typeparam name="TIn2"></typeparam>
        /// <typeparam name="TIn3"></typeparam>
        /// <typeparam name="TIn4"></typeparam>
        /// <typeparam name="TIn5"></typeparam>
        /// <typeparam name="TIn6"></typeparam>
        /// <typeparam name="TIn7"></typeparam>
        /// <typeparam name="TIn8"></typeparam>
        /// <typeparam name="TIn9"></typeparam>
        /// <typeparam name="TOut"></typeparam>
        /// <param name="method">Intercepted method</param>
        /// <param name="interceptor">Interceptor method</param>
        IInterceptorBuilder<TContext, TTarget> OnMethodExecuting<TIn1, TIn2, TIn3, TIn4, TIn5, TIn6, TIn7, TIn8, TIn9, TOut>(Expression<Func<TTarget, Func<TIn1, TIn2, TIn3, TIn4, TIn5, TIn6, TIn7, TIn8, TIn9, TOut>>> method,
            Func<TContext, TTarget, TIn1, TIn2, TIn3, TIn4, TIn5, TIn6, TIn7, TIn8, TIn9, IMethodInvoker<TOut>, TOut> interceptor);
        /// <summary>
        /// Registers an interceptor for the method that is executed when the method is executed
        /// </summary>
        /// <typeparam name="TIn1"></typeparam>
        /// <typeparam name="TIn2"></typeparam>
        /// <typeparam name="TIn3"></typeparam>
        /// <typeparam name="TIn4"></typeparam>
        /// <typeparam name="TIn5"></typeparam>
        /// <typeparam name="TIn6"></typeparam>
        /// <typeparam name="TIn7"></typeparam>
        /// <typeparam name="TIn8"></typeparam>
        /// <typeparam name="TIn9"></typeparam>
        /// <typeparam name="TIn10"></typeparam>
        /// <typeparam name="TOut"></typeparam>
        /// <param name="method">Intercepted method</param>
        /// <param name="interceptor">Interceptor method</param>
        IInterceptorBuilder<TContext, TTarget> OnMethodExecuting<TIn1, TIn2, TIn3, TIn4, TIn5, TIn6, TIn7, TIn8, TIn9, TIn10, TOut>(Expression<Func<TTarget, Func<TIn1, TIn2, TIn3, TIn4, TIn5, TIn6, TIn7, TIn8, TIn9, TIn10, TOut>>> method,
            Func<TContext, TTarget, TIn1, TIn2, TIn3, TIn4, TIn5, TIn6, TIn7, TIn8, TIn9, TIn10, IMethodInvoker<TOut>, TOut> interceptor);
        /// <summary>
        /// Registers an interceptor for the method that is executed when the method is executed
        /// </summary>
        /// <typeparam name="TIn1"></typeparam>
        /// <typeparam name="TIn2"></typeparam>
        /// <typeparam name="TIn3"></typeparam>
        /// <typeparam name="TIn4"></typeparam>
        /// <typeparam name="TIn5"></typeparam>
        /// <typeparam name="TIn6"></typeparam>
        /// <typeparam name="TIn7"></typeparam>
        /// <typeparam name="TIn8"></typeparam>
        /// <typeparam name="TIn9"></typeparam>
        /// <typeparam name="TIn10"></typeparam>
        /// <typeparam name="TIn11"></typeparam>
        /// <typeparam name="TOut"></typeparam>
        /// <param name="method">Intercepted method</param>
        /// <param name="interceptor">Interceptor method</param>
        IInterceptorBuilder<TContext, TTarget> OnMethodExecuting<TIn1, TIn2, TIn3, TIn4, TIn5, TIn6, TIn7, TIn8, TIn9, TIn10, TIn11, TOut>(Expression<Func<TTarget, Func<TIn1, TIn2, TIn3, TIn4, TIn5, TIn6, TIn7, TIn8, TIn9, TIn10, TIn11, TOut>>> method,
            Func<TContext, TTarget, TIn1, TIn2, TIn3, TIn4, TIn5, TIn6, TIn7, TIn8, TIn9, TIn10, TIn11, IMethodInvoker<TOut>, TOut> interceptor);
        /// <summary>
        /// Registers an interceptor for the method that is executed when the method is executed
        /// </summary>
        /// <typeparam name="TIn1"></typeparam>
        /// <typeparam name="TIn2"></typeparam>
        /// <typeparam name="TIn3"></typeparam>
        /// <typeparam name="TIn4"></typeparam>
        /// <typeparam name="TIn5"></typeparam>
        /// <typeparam name="TIn6"></typeparam>
        /// <typeparam name="TIn7"></typeparam>
        /// <typeparam name="TIn8"></typeparam>
        /// <typeparam name="TIn9"></typeparam>
        /// <typeparam name="TIn10"></typeparam>
        /// <typeparam name="TIn11"></typeparam>
        /// <typeparam name="TIn12"></typeparam>
        /// <typeparam name="TOut"></typeparam>
        /// <param name="method">Intercepted method</param>
        /// <param name="interceptor">Interceptor method</param>
        IInterceptorBuilder<TContext, TTarget> OnMethodExecuting<TIn1, TIn2, TIn3, TIn4, TIn5, TIn6, TIn7, TIn8, TIn9, TIn10, TIn11, TIn12, TOut>(Expression<Func<TTarget, Func<TIn1, TIn2, TIn3, TIn4, TIn5, TIn6, TIn7, TIn8, TIn9, TIn10, TIn11, TIn12, TOut>>> method,
            Func<TContext, TTarget, TIn1, TIn2, TIn3, TIn4, TIn5, TIn6, TIn7, TIn8, TIn9, TIn10, TIn11, TIn12, IMethodInvoker<TOut>, TOut> interceptor);
                /// <summary>
        /// Registers an interceptor for the method that is executed when the method is executed
        /// </summary>
        /// <typeparam name="TIn1"></typeparam>
        /// <typeparam name="TIn2"></typeparam>
        /// <typeparam name="TIn3"></typeparam>
        /// <typeparam name="TIn4"></typeparam>
        /// <typeparam name="TIn5"></typeparam>
        /// <typeparam name="TIn6"></typeparam>
        /// <typeparam name="TIn7"></typeparam>
        /// <typeparam name="TIn8"></typeparam>
        /// <typeparam name="TIn9"></typeparam>
        /// <typeparam name="TIn10"></typeparam>
        /// <typeparam name="TIn11"></typeparam>
        /// <typeparam name="TIn12"></typeparam>
        /// <typeparam name="TIn13"></typeparam>
        /// <typeparam name="TOut"></typeparam>
        /// <param name="method">Intercepted method</param>
        /// <param name="interceptor">Interceptor method</param>
        IInterceptorBuilder<TContext, TTarget> OnMethodExecuting<TIn1, TIn2, TIn3, TIn4, TIn5, TIn6, TIn7, TIn8, TIn9, TIn10, TIn11, TIn12, TIn13, TOut>(Expression<Func<TTarget, Func<TIn1, TIn2, TIn3, TIn4, TIn5, TIn6, TIn7, TIn8, TIn9, TIn10, TIn11, TIn12, TIn13, TOut>>> method,
            Func<TContext, TTarget, TIn1, TIn2, TIn3, TIn4, TIn5, TIn6, TIn7, TIn8, TIn9, TIn10, TIn11, TIn12, TIn13, IMethodInvoker<TOut>, TOut> interceptor);
    }
}