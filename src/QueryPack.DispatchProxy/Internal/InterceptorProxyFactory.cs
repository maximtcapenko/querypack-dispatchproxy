namespace QueryPack.DispatchProxy.Internal
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    
     internal class InterceptorProxyFactory
    {
        public static TInterface Create<TDependency, TInterface, TImplementation>(TDependency dependency, 
            IEnumerable<IInterceptorProxyFactory<TDependency, TInterface>> factories, TImplementation implementation) 
            where TDependency : class
            where TInterface : class
            where TImplementation : class, TInterface
        {
            var proxy = DispatchProxy.Create<TInterface, InterceptorProxy<TDependency, TInterface>>() as InterceptorProxy<TDependency, TInterface>;
            proxy.Target = implementation;
            proxy.Context = dependency;
            proxy.Interceptors = factories.Select(e => e.Create());

            return proxy as TInterface;
        }
    }
}