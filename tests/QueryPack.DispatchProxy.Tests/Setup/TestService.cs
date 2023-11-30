namespace QueryPack.DispatchProxy.Tests.Setup
{
    public interface ITestService
    {
        Task<int> DoAsync(string firstArgument, int secondArgument, DateTime? thirdArgument);
    }
}