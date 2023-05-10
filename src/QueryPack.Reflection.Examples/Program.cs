using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using QueryPack.Reflection.Extensions;

namespace QueryPack.Reflection.Examples
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var services = new ServiceCollection();
            services.AddTransient<IEntityService, EntityService>();
            services.AddSingleton<Context>();
            services.AddInterceptorFor(new EntityIntecepterProxyFactoryBuilder());

            var provider = services.BuildServiceProvider();
            var entitySerice = provider.GetRequiredService<IEntityService>();
            var result = await entitySerice.CreateAsync("1", new EntityArg(), CancellationToken.None);

            Console.WriteLine("Hello, World!");
        }

        static Task<EntityResult> CreateAsync(string id, EntityArg arg, CancellationToken token)
        {
            return Task.FromResult(new EntityResult { });
        }
    }



    interface IEntityService
    {
        Task<EntityResult> CreateAsync(string id, EntityArg arg, CancellationToken token);
    }

    class EntityService : IEntityService
    {
        public Task<EntityResult> CreateAsync(string id, EntityArg arg, CancellationToken token)
        {
            return Task.FromResult(new EntityResult() { Id = 1 });
        }
    }

    class EntityResult
    {
        public int Id { get; set; }
    }

    class EntityArg { }
    class Context { }

    class EntityIntecepterProxyFactoryBuilder : InterceptorProxyFactoryBuilder<Context, IEntityService>
    {
        public void AddInterceptor(IInterceptorBuilder<Context, IEntityService> interceptorBuilder)
        {
            interceptorBuilder
            .InterceptMethodOnExecuted<string, EntityArg, CancellationToken, Task<EntityResult>>(e => e.CreateAsync,
                async (ctx, service, id, arg, token, result) =>
            {
                var r = await result;
                Console.WriteLine($"{r.Id}");
                return r;
            });
        }
    }
}
