namespace QueryPack.DispatchProxy
{
    using System.Reflection;

    public interface IMethodInvokerFactory<TTarget> where TTarget : class
    {
        IMethodInvoker Create(TTarget target, MethodInfo method, object[] args);
    }
}