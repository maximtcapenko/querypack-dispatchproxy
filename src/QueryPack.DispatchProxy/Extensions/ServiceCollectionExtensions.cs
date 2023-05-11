namespace QueryPack.DispatchProxy.Extensions
{
    using Microsoft.Extensions.DependencyInjection;
    using Impl;
    using System.Linq;
    using System;
    using Internal;
    using System.Reflection;

    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInterceptorFor<TContext, TTarget>(this IServiceCollection self,
            InterceptorProxyFactoryBuilder<TContext, TTarget> factoryBuilder)
            where TContext : class
            where TTarget : class
        {
            var builder = new InterceptorBuilderImpl<TContext, TTarget>(self);
            factoryBuilder.AddInterceptor(builder);

            var regestry = self.FirstOrDefault(e => e.ServiceType == typeof(TTarget));
            self.Remove(regestry);
            if (regestry.ImplementationType != null)
            {
                var addMethod = typeof(ServiceCollectionExtensions).GetMethod(nameof(AddTypeImplementationTransient), BindingFlags.Static | BindingFlags.NonPublic);
                self.Add(new ServiceDescriptor(regestry.ImplementationType, regestry.ImplementationType, regestry.Lifetime));

                var addMethodGeneric = addMethod.MakeGenericMethod(typeof(TContext), typeof(TTarget), regestry.ImplementationType);
                addMethodGeneric.Invoke(null, new[] { self });
            }
            if (regestry.ImplementationFactory != null)
            {
                var addMethod = typeof(ServiceCollectionExtensions).GetMethod(nameof(AddTypeFactoryTransient), BindingFlags.Static | BindingFlags.NonPublic);
                var addMethodGeneric = addMethod.MakeGenericMethod(typeof(TContext), typeof(TTarget));
                addMethodGeneric.Invoke(null, new object[] { self, regestry.ImplementationFactory });
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
    }
}