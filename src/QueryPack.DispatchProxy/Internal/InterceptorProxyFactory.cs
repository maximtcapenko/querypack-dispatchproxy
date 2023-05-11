namespace QueryPack.DispatchProxy.Internal
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    
     internal class InterceptorProxyFactory
    {
        public static TInterface Create<TContext, TInterface, TImplementation>(TContext context, 
            IEnumerable<IInterceptorProxyFactory<TContext, TInterface>> factories, TImplementation implementation) 
            where TContext : class
            where TInterface : class
            where TImplementation : class, TInterface
        {
            var proxy = DispatchProxy.Create<TInterface, InterceptorProxy<TContext, TInterface>>() as InterceptorProxy<TContext, TInterface>;
            proxy.Target = implementation;
            proxy.Context = context;
            proxy.Interceptors = factories.Select(e => e.Create());

            return proxy as TInterface;
        }
    }
}