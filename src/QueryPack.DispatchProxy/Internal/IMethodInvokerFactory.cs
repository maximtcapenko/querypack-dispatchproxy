namespace QueryPack.DispatchProxy.Internal
{
    using System.Reflection;

    internal interface IMethodInvokerFactory<TTarget> where TTarget : class
    {
        IMethodInvoker Create(TTarget target, MethodInfo method, object[] args);
    }
}