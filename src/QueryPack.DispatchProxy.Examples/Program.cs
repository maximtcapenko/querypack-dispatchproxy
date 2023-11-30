namespace QueryPack.DispatchProxy.Examples
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.Extensions.DependencyInjection;
    using Extensions;

    class Program
    {
        static async Task Main(string[] args)
        {
            var services = new ServiceCollection();
            services.AddTransient<IEntityService, EntityService>();
            services.AddSingleton<Dependency>();
            services.AddInterceptors(typeof(Program).Assembly);

            var provider = services.BuildServiceProvider();
            var entitySerice = provider.GetRequiredService<IEntityService>();
            var result = await entitySerice.CreateAsync("1", new EntityArg(), CancellationToken.None);
            await entitySerice.UpdateAsync("2", new EntityArg(), CancellationToken.None);
        }

        static Task<EntityResult> CreateAsync(string id, EntityArg arg, CancellationToken token)
        {
            return Task.FromResult(new EntityResult { });
        }
    }

    interface IEntityService
    {
        Task<EntityResult> CreateAsync(string id, EntityArg arg, CancellationToken token);
        Task UpdateAsync(string id, EntityArg arg, CancellationToken token);
    }

    class EntityService : IEntityService
    {
        public Task<EntityResult> CreateAsync(string id, EntityArg arg, CancellationToken token)
        {
            Console.WriteLine("Executing method CreateAsync");
            return Task.FromResult(new EntityResult() { Id = id });
        }

        public Task UpdateAsync(string id, EntityArg arg, CancellationToken token)
        {
            Console.WriteLine("Executing method UpdateAsync");
            return Task.CompletedTask;
        }
    }

    class EntityResult
    {
        public string Id { get; set; }
    }

    class EntityArg { }
    class Dependency { }

    class EntityInterceptorProxyFactoryBuilder : IInterceptorProxyFactoryBuilder<Dependency, IEntityService>
    {
        public void AddInterceptor(IInterceptorBuilder<Dependency, IEntityService> interceptorBuilder)
        {
            interceptorBuilder
            .OnMethodExecuting<string, EntityArg, CancellationToken, Task<EntityResult>>(e => e.CreateAsync,
                async (ctx, service, id, arg, token, invoker) =>
            {
                Console.WriteLine($"Before method call {invoker.MethodName}");
                var result = await invoker.Invoke();

                Console.WriteLine("After method is executed");
                Console.WriteLine($"{result.Id}");
                return result;
            })
            .OnMethodExecuting<string, EntityArg, CancellationToken, Task>(e => e.UpdateAsync,
             async (ctx, service, id, arg, token, invoker) =>
             {
                 Console.WriteLine($"Before method call {invoker.MethodName}");
                 await invoker.Invoke();
                 Console.WriteLine("After method is executed");
             });
        }
    }
}