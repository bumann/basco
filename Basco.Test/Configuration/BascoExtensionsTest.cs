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
        public void For_MustReturnBasco()
        {
            var basco = new Basco<TestTrigger>(
                Mock.Of<IScyano<TestTrigger>>(), 
                Mock.Of<IBascoStateCache<TestTrigger>>(),
                Mock.Of<IBascoTransitionCache<TestTrigger>>(),
                Mock.Of<IBascoExecutor<TestTrigger>>(), 
                Mock.Of<IBascoConfigurator<TestTrigger>>());

            IBasco<TestTrigger> result = basco.For<SimpleTestState, TestTrigger>(configurator => { });

            result.Should().BeOfType<Basco<TestTrigger>>();
        }

        [Fact]
        public void For_MustAddStateTransitionToExecutor()
        {
            var transitionCache = new Mock<IBascoTransitionCache<TestTrigger>>();
            var basco = new Basco<TestTrigger>(
                Mock.Of<IScyano<TestTrigger>>(),
                Mock.Of<IBascoStateCache<TestTrigger>>(),
                transitionCache.Object,
                Mock.Of<IBascoExecutor<TestTrigger>>(),
                Mock.Of<IBascoConfigurator<TestTrigger>>());

            basco.For<SimpleTestState, TestTrigger>(configurator => { });

            transitionCache.Verify(x => x.Add(typeof(SimpleTestState), It.IsAny<StateTransitions<TestTrigger>>()));
        }

        [Fact]
        public void ForComposite_MustReturnBasco()
        {
            var basco = new Basco<TestTrigger>(
                Mock.Of<IScyano<TestTrigger>>(),
                Mock.Of<IBascoStateCache<TestTrigger>>(),
                Mock.Of<IBascoTransitionCache<TestTrigger>>(),
                Mock.Of<IBascoExecutor<TestTrigger>>(),
                Mock.Of<IBascoConfigurator<TestTrigger>>());

            IBasco<TestTrigger> result = basco.ForComposite<SimpleTestState, TestTrigger>(configurator => { });

            result.Should().BeOfType<Basco<TestTrigger>>();
        }

        [Fact]
        public void ForComposite_MustAddStateTransitionToExecutor()
        {
            var transitionCache = new Mock<IBascoTransitionCache<TestTrigger>>();
            var basco = new Basco<TestTrigger>(
                Mock.Of<IScyano<TestTrigger>>(),
                Mock.Of<IBascoStateCache<TestTrigger>>(),
                transitionCache.Object,
                Mock.Of<IBascoExecutor<TestTrigger>>(),
                Mock.Of<IBascoConfigurator<TestTrigger>>());

            basco.ForComposite<SimpleTestState, TestTrigger>(configurator => { });

            transitionCache.Verify(x => x.Add(typeof(SimpleTestState), It.IsAny<StateTransitions<TestTrigger>>()));
        }

        [Fact]
        public void AlwaysStartInInitialState_MustReturnBasco()
        {
            var basco = new Basco<TestTrigger>(
                Mock.Of<IScyano<TestTrigger>>(),
                Mock.Of<IBascoStateCache<TestTrigger>>(),
                Mock.Of<IBascoTransitionCache<TestTrigger>>(),
                Mock.Of<IBascoExecutor<TestTrigger>>(),
                Mock.Of<IBascoConfigurator<TestTrigger>>());

            IBasco<TestTrigger> result = basco.AlwaysStartInInitialState();

            result.Should().BeOfType<Basco<TestTrigger>>();
        }

        [Fact]
        public void AlwaysStartInInitialState_MustSetAlwaysStartWithInitialState()
        {
            var bascoExecutor = new Mock<IBascoExecutor<TestTrigger>>();
            var basco = new Basco<TestTrigger>(
                Mock.Of<IScyano<TestTrigger>>(),
                Mock.Of<IBascoStateCache<TestTrigger>>(),
                Mock.Of<IBascoTransitionCache<TestTrigger>>(),
                bascoExecutor.Object,
                Mock.Of<IBascoConfigurator<TestTrigger>>());

            basco.AlwaysStartInInitialState();

            bascoExecutor.VerifySet(x => x.AlwaysStartWithInitialState = true, Times.Once);
        }
    }
}