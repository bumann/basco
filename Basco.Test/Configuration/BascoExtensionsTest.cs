namespace Basco.Test.Configuration
{
    using Basco.Configuration;
    using Basco.Execution;
    using FluentAssertions;
    using Moq;
    using Scyano;
    using Xunit;

    public class BascoExtensionsTest
    {
        [Fact]
        public void In_MustReturnBasco()
        {
            var basco = new Basco<TestTrigger>(
                Mock.Of<IScyano>(), 
                Mock.Of<IBascoTransitionCache<TestTrigger>>(),
                Mock.Of<IBascoConfigurator<TestTrigger>>(), 
                Mock.Of<IBascoExecutor<TestTrigger>>());

            IBasco<TestTrigger> result = basco.In<SimpleTestState, TestTrigger>(configurator => { });

            result.Should().BeOfType<Basco<TestTrigger>>();
        }

        [Fact]
        public void In_MustAddStateTransitionToExecutor()
        {
            var transitionCache = new Mock<IBascoTransitionCache<TestTrigger>>();
            var basco = new Basco<TestTrigger>(
                Mock.Of<IScyano>(),
                transitionCache.Object,
                Mock.Of<IBascoConfigurator<TestTrigger>>(),
                Mock.Of<IBascoExecutor<TestTrigger>>());

            basco.In<SimpleTestState, TestTrigger>(configurator => { });

            transitionCache.Verify(x => x.Add(typeof(SimpleTestState), It.IsAny<StateTransitions<TestTrigger>>()));
        }
    }
}