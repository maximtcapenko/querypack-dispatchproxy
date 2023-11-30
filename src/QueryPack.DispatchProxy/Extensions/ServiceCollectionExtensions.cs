namespace QueryPack.DispatchProxy.Extensions
{
    using Microsoft.Extensions.DependencyInjection;
    using Impl;
    using System.Linq;
    using System;
    using Internal;
    using System.Reflection;

    /// <summary>
    /// Service Collection Extensions
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Auto adding and configuring interceptor builders in given assemblies
        /// </summary>
        /// <param name="self"></param>
        /// <param name="assemblies"></param>
        /// <returns></returns>
        public static IServiceCollection AddInterceptors(this IServiceCollection self, params Assembly[] assemblies)
        {
            var method = typeof(ServiceCollectionExtensions).GetMethod(nameof(AddInterceptorFor));

            foreach (var serviceType in assemblies.SelectMany(e => e.GetTypes()))
            {
                ReflectionUtils.DoWithGenericInterfaceImpls(serviceType, typeof(IInterceptorProxyFactoryBuilder<,>), (@interface, implementation, name) =>
                {
                    var genericMethod = method.MakeGenericMethod(@interface.GetGenericArguments());
                    var instance = Activator.CreateInstance(implementation);
                    genericMethod.Invoke(null, new object[] { self, instance });
                });
            }

            return self;
        }

        /// <summary>
        /// Adds and configures interceptor builder
        /// </summary>
        /// <typeparam name="TDependency"></typeparam>
        /// <typeparam name="TTarget"></typeparam>
        /// <param name="self"></param>
        /// <param name="factoryBuilder"></param>
        /// <returns></returns>
        public static IServiceCollection AddInterceptorFor<TDependency, TTarget>(this IServiceCollection self,
            IInterceptorProxyFactoryBuilder<TDependency, TTarget> factoryBuilder)
            where TDependency : class
            where TTarget : class
        {
            var builder = new InterceptorBuilderImpl<TDependency, TTarget>(self);
            factoryBuilder.AddInterceptor(builder);

            var registries = self.Where(e => e.ServiceType == typeof(TTarget)).ToList();

            var addImplMethod = typeof(ServiceCollectionExtensions).GetMethod(nameof(AddTypeImplementationTransient), BindingFlags.Static | BindingFlags.NonPublic);
            var addFactoryMethod = typeof(ServiceCollectionExtensions).GetMethod(nameof(AddTypeFactoryTransient), BindingFlags.Static | BindingFlags.NonPublic);
            var addInstanceMethod = typeof(ServiceCollectionExtensions).GetMethod(nameof(AddInstanceTransient), BindingFlags.Static | BindingFlags.NonPublic);

            foreach (var registry in registries)
            {
                self.Remove(registry);
                if (registry.ImplementationType != null)
                {
                    self.Add(new ServiceDescriptor(registry.ImplementationType, registry.ImplementationType, registry.Lifetime));

                    var addMethodGeneric = addImplMethod.MakeGenericMethod(typeof(TDependency), typeof(TTarget), registry.ImplementationType);
                    addMethodGeneric.Invoke(null, new[] { self });
                }
                if (registry.ImplementationFactory != null)
                {
                    var addMethodGeneric = addFactoryMethod.MakeGenericMethod(typeof(TDependency), typeof(TTarget));
                    addMethodGeneric.Invoke(null, new object[] { self, registry.ImplementationFactory });
                }
                if (registry.ImplementationInstance != null)
                {
                    var addMethodGeneric = addInstanceMethod.MakeGenericMethod(typeof(TDependency), typeof(TTarget));
                    addMethodGeneric.Invoke(null, new object[] { self, registry.ImplementationInstance });
                }
            }

            return self;
        }

        private static void AddTypeImplementationTransient<TDependency, TInterface, TImplementation>(IServiceCollection services)
            where TDependency : class
            where TInterface : class
            where TImplementation : class, TInterface 
                => services.AddTransient(s => 
                InterceptorProxyFactory.Create(s.GetRequiredService<TDependency>(), 
                s.GetServices<IInterceptorProxyFactory<TDependency, TInterface>>(),
                s.GetRequiredService<TImplementation>()));
        

        private static void AddTypeFactoryTransient<TDependency, TInterface>(IServiceCollection services, Func<IServiceProvider, object> factory)
            where TDependency : class
            where TInterface : class
                => services.AddTransient(s => 
                InterceptorProxyFactory.Create(s.GetRequiredService<TDependency>(), 
                s.GetServices<IInterceptorProxyFactory<TDependency, TInterface>>(), 
                (TInterface)factory(s)));
        

        private static void AddInstanceTransient<TDependency, TInterface>(IServiceCollection services, object instance)
            where TDependency : class
            where TInterface : class 
                => services.AddTransient(s => 
                InterceptorProxyFactory.Create(s.GetRequiredService<TDependency>(), 
                s.GetServices<IInterceptorProxyFactory<TDependency, TInterface>>(), 
                (TInterface)instance));
    }
}