namespace Basco.Test
{
    using System;
    using Moq;
    using Xunit;

    public class StateTransitionsBuilderTest
    {
        private readonly Type stateType;
        private readonly Mock<IStateTransitions<TestTrigger>> stateTransitions;
        private readonly StateTransitionsBuilder<TestTrigger> testee;

        public StateTransitionsBuilderTest()
        {
            this.stateType = typeof(ExtendedTestState);
            this.stateTransitions = new Mock<IStateTransitions<TestTrigger>>();
            this.testee = new StateTransitionsBuilder<TestTrigger>(this.stateType, this.stateTransitions.Object);
        }

        [Fact]
        public void AddTransition_WhenForCurrentStateType_MustAddTransitionForStateType()
        {
            const TestTrigger ExpectedTrigger = TestTrigger.TransitionOne;
            this.testee.CurrentTrigger = ExpectedTrigger;

            this.testee.AddTransition();

            this.stateTransitions.Verify(x => x.Add(ExpectedTrigger, this.stateType), Times.Once);
        }

        [Fact]
        public void AddTransition_WhenForNewStateType_MustAddTransitionForNewStateType()
        {
            const TestTrigger ExpectedTrigger = TestTrigger.TransitionOne;
            this.testee.CurrentTrigger = ExpectedTrigger;

            this.testee.AddTransition<SimpleTestState>();

            this.stateTransitions.Verify(x => x.Add(ExpectedTrigger, typeof(SimpleTestState)), Times.Once);
        }
    }
}