namespace QueryPack.DispatchProxy.Impl
{
    using System.Reflection;
    using Internal;

    internal class MethodInvokerFactoryImpl<TTarget, TResult> : IMethodInvokerFactory<TTarget> where TTarget : class
    {
        public IMethodInvoker Create(TTarget target, MethodInfo method, object[] args)
            => new MethodInvokerImpl<TTarget, TResult>(target, method, args);
    }
}