namespace QueryPack.DispatchProxy.Internal
{
    using System;
    using System.Linq;

    internal static class ReflectionUtils
    {
        internal static void DoWithGenericInterfaceImpls(Type serviceType, Type interfaceType, Action<Type, Type, string> action)
        {
            if (serviceType.GetInterfaces().Any(x => x.IsGenericType && x.GetGenericTypeDefinition() == interfaceType))
            {
                var types = serviceType.GetInterfaces().Where(t => t.IsGenericType && t.GetGenericTypeDefinition() == interfaceType);

                foreach (var type in types)
                {
                    action(type, serviceType, null);
                }
            }
        }
    }
}