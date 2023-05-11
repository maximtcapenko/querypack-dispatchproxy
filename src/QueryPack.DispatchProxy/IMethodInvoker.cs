namespace QueryPack.DispatchProxy
{
    public interface IMethodInvoker { }
    public interface IMethodInvoker<TResult> : IMethodInvoker
    {
        TResult Invoke();
        string MethodName { get; }
    }
}