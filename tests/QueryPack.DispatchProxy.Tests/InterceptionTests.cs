namespace QueryPack.DispatchProxy.Tests
{
    using Microsoft.Extensions.DependencyInjection;
    using Setup;
    using Extensions;
    using Moq;
    using Xunit;
    using FluentAssertions;
    using AutoFixture.Xunit2;

    public class InterceptionTests
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly Mock<ITestService> _testService;

        public InterceptionTests()
        {
            var services = new ServiceCollection();
            _testService = new Mock<ITestService>();

            services.AddTransient(s => _testService.Object);
            services.AddSingleton<Dependency>();
            services.AddInterceptorFor(new TestInterceptorProxyFactoryBuilder());

            _serviceProvider = services.BuildServiceProvider();
        }

        [Theory, AutoData]
        public async Task When_configuration_set_interceptor_should_be_invoked(string firstArgument, int secondArgument, DateTime thirdArgument, int result)
        {
            _testService.Setup(e => e.DoAsync(firstArgument, secondArgument, thirdArgument))
                        .Returns(Task.FromResult(result));

            var testService = _serviceProvider.GetRequiredService<ITestService>();
            var func = () => testService.DoAsync(firstArgument, secondArgument, thirdArgument);

            await func.Should().NotThrowAsync();
        }

        public class Dependency { }

        private sealed class TestInterceptorProxyFactoryBuilder : IInterceptorProxyFactoryBuilder<Dependency, ITestService>
        {
            public void AddInterceptor(IInterceptorBuilder<Dependency, ITestService> interceptorBuilder)
            {
                interceptorBuilder.OnMethodExecuting<string, int, DateTime?, Task<int>>(e => e.DoAsync,
                 async (dependency, service, firstArgument, secondArgument, thirdArgument, invoker) =>
                 {
                     var result = await invoker.Invoke();

                     invoker.MethodName.Should().Be(nameof(ITestService.DoAsync));
                     dependency.Should().NotBeNull();
                     service.Should().NotBeNull();
                     (service is ITestService).Should().BeTrue();

                     return result;
                 });
            }
        }
    }
}
