# QueryPack.DispatchProxy 
Simple interception implementation based on `System.Reflection.DispatchProxy`. Allows you to create type-safe interceptors for individual methods. Supports standard dependency injection. Has direct access to intercepted instance.

## Getting Started
1. Install the package into your project
```
dotnet add package QueryPack.DispatchProxy
```
2. Add interception configuration
```c#
class EntityInterceptorProxyFactoryBuilder : InterceptorProxyFactoryBuilder<Context, IEntityService>
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
 services.AddInterceptorFor(new EntityInterceptorProxyFactoryBuilder());
```
4. Service method call
```c#
IEntityService entitySerice;
var result = await entitySerice.CreateAsync("some_id", new EntityArg(), CancellationToken.None);
```
5. Use cases
- `Access Control`: Verify if the current user has the necessary permissions to invoke a specific method.
- `Input Validation`: Validate the input parameters of the invoked method to ensure they meet the required format or constraints.
- `Output Validation`: Validate the output of the invoked method to ensure it meets the expected format or constraints.
- `Data Replication`: Use the result of the method execution to replicate data to other data sources.
- `Event Sourcing`: Generate an event before or after the method is executed to capture and store the changes made to the data.
