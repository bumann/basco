namespace Basco.Test
{
    using Basco.Execution;
    using FluentAssertions;
    using Moq;
    using Scyano;
    using Xunit;

    public class BascoExtensionsTest
    {
        [Fact]
        public void In_MustReturnStateTransitionsBuilder()
        {
            var basco = new Basco<TestTrigger>(
                Mock.Of<IScyano>(), 
                Mock.Of<IBascoTransitionCache<TestTrigger>>(),
                Mock.Of<IBascoConfigurator<TestTrigger>>(), 
                Mock.Of<IBascoExecutor<TestTrigger>>());

            IStateTransitionsBuilder<TestTrigger> result = basco.In<SimpleTestState, TestTrigger>();

            result.Should().BeOfType<StateTransitionsBuilder<TestTrigger>>();
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

            basco.In<SimpleTestState, TestTrigger>();

            transitionCache.Verify(x => x.Add(typeof(SimpleTestState), It.IsAny<StateTransitions<TestTrigger>>()));
        }
    }
}