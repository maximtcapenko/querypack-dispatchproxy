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
                ReflectionUtils.DoWithGenericInterfaceImpls(serviceType, typeof(InterceptorProxyFactoryBuilder<,>), (@interface, implementation, name) =>
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
        /// <typeparam name="TContext"></typeparam>
        /// <typeparam name="TTarget"></typeparam>
        /// <param name="self"></param>
        /// <param name="factoryBuilder"></param>
        /// <returns></returns>
        public static IServiceCollection AddInterceptorFor<TContext, TTarget>(this IServiceCollection self,
            InterceptorProxyFactoryBuilder<TContext, TTarget> factoryBuilder)
            where TContext : class
            where TTarget : class
        {
            var builder = new InterceptorBuilderImpl<TContext, TTarget>(self);
            factoryBuilder.AddInterceptor(builder);

            var registries = self.Where(e => e.ServiceType == typeof(TTarget)).ToList();
            foreach (var registry in registries)
            {
                self.Remove(registry);
                if (registry.ImplementationType != null)
                {
                    var addMethod = typeof(ServiceCollectionExtensions).GetMethod(nameof(AddTypeImplementationTransient), BindingFlags.Static | BindingFlags.NonPublic);
                    self.Add(new ServiceDescriptor(registry.ImplementationType, registry.ImplementationType, registry.Lifetime));

                    var addMethodGeneric = addMethod.MakeGenericMethod(typeof(TContext), typeof(TTarget), registry.ImplementationType);
                    addMethodGeneric.Invoke(null, new[] { self });
                }
                if (registry.ImplementationFactory != null)
                {
                    var addMethod = typeof(ServiceCollectionExtensions).GetMethod(nameof(AddTypeFactoryTransient), BindingFlags.Static | BindingFlags.NonPublic);
                    var addMethodGeneric = addMethod.MakeGenericMethod(typeof(TContext), typeof(TTarget));
                    addMethodGeneric.Invoke(null, new object[] { self, registry.ImplementationFactory });
                }
                if (registry.ImplementationInstance != null)
                {
                    var addMethod = typeof(ServiceCollectionExtensions).GetMethod(nameof(AddInstanceTransient), BindingFlags.Static | BindingFlags.NonPublic);
                    var addMethodGeneric = addMethod.MakeGenericMethod(typeof(TContext), typeof(TTarget));
                    addMethodGeneric.Invoke(null, new object[] { self, registry.ImplementationInstance });
                }
            }
            return self;
        }

        private static void AddTypeImplementationTransient<TContext, TInterface, TImplementation>(IServiceCollection services)
            where TContext : class
            where TInterface : class
            where TImplementation : class, TInterface
        {
            services.AddTransient(s =>
            InterceptorProxyFactory.Create(s.GetRequiredService<TContext>(), s.GetServices<IInterceptorProxyFactory<TContext, TInterface>>(), s.GetRequiredService<TImplementation>()));
        }

        private static void AddTypeFactoryTransient<TContext, TInterface>(IServiceCollection services, Func<IServiceProvider, object> factory)
            where TContext : class
            where TInterface : class
        {
            services.AddTransient(s =>
            InterceptorProxyFactory.Create(s.GetRequiredService<TContext>(), s.GetServices<IInterceptorProxyFactory<TContext, TInterface>>(), (TInterface)factory(s)));
        }

        private static void AddInstanceTransient<TContext, TInterface>(IServiceCollection services, object instance)
            where TContext : class
            where TInterface : class
        {
            services.AddTransient(s =>
            InterceptorProxyFactory.Create(s.GetRequiredService<TContext>(), s.GetServices<IInterceptorProxyFactory<TContext, TInterface>>(), (TInterface)instance));
        }
    }
}