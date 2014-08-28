namespace Basco.Test.Configuration
{
    using System;
    using System.Linq;
    using Basco.Configuration;
    using Moq;
    using Xunit;

    public class StateConfiguratorTest
    {
        private readonly Mock<IBascoInternal<TestTrigger>> basco;
        private readonly StateConfigurator<TestTrigger> testee;

        public StateConfiguratorTest()
        {
            this.basco = new Mock<IBascoInternal<TestTrigger>>();
            this.testee = new StateConfigurator<TestTrigger>(this.basco.Object);
        }

        [Fact]
        public void ForState_MustAddTransitionCache()
        {
            var expectedType = typeof(SimpleTestState);

            this.testee.ForState<SimpleTestState>();

            this.basco.Verify(x => x.AddTransitionCache(expectedType, It.IsAny<StateTransitions<TestTrigger>>()));
        }

        [Fact]
        public void StateConfigurator_MustConfigureTransitionCache()
        {
            this.testee.ForState<SimpleTestState>();
            this.testee.When(TestTrigger.TransitionTwo);
            this.testee.Goto<ExtendedTestState>();

            this.basco.Verify(x => x.AddTransitionCache(
                It.IsAny<Type>(), 
                It.Is<StateTransitions<TestTrigger>>(t => 
                    t.Transitions.Keys.Single() == TestTrigger.TransitionTwo 
                    && t.Transitions.Values.Single() == typeof(ExtendedTestState))));
        }
    }
}