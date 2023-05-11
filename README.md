# QueryPack.DispatchProxy 

## Getting Started
1. Install the package into your project
```
dotnet add package QueryPack.DispatchProxy
```
2. Add interception configuration
```c#
class EntityIntecepterProxyFactoryBuilder : InterceptorProxyFactoryBuilder<Context, IEntityService>
{
    public void AddInterceptor(IInterceptorBuilder<Context, IEntityService> interceptorBuilder)
    {
        interceptorBuilder
        .OnMethodExecuting<string, EntityArg, CancellationToken, Task<EntityResult>>(e => e.CreateAsync,
                async (ctx, service, id, arg, token, invoker) =>
            {
                // code before method call
                var result = await invoker.Invoke();
                // code after method call
                return result;
            })
    }
}
```
3. Register interception configuration in `Startup`
```c#
 services.AddInterceptorFor(new EntityIntecepterProxyFactoryBuilder());
```
4. Service method call
```c#
IEntityService entitySerice;
var result = await entitySerice.CreateAsync("some_id", new EntityArg(), CancellationToken.None);