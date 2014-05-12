namespace Basco.Test
{
    using Basco;
    using FluentAssertions;
    using Xunit;

    public class TransitionsBuilderTest
    {
        private readonly TransitionsBuilder<TestTrigger> testee;

        public TransitionsBuilderTest()
        {
            this.testee = new TransitionsBuilder<TestTrigger>(new TestableState(), () => new TransitionPool<TestTrigger>());
        }

        [Fact]
        public void AddTransition_WhenWrongTransition_MustReturnNull()
        {
            this.testee.CurrentTrigger = TestTrigger.TransitionOne;

            this.testee.AddTransition();

            IState<TestTrigger> result = this.testee.State.Transitions.GetNextState(TestTrigger.TransitionTwo);
            result.Should().BeNull();
        }

        [Fact]
        public void AddTransition_MustAddTransitionWithCurrentState()
        {
            this.testee.CurrentTrigger = TestTrigger.TransitionOne;

            this.testee.AddTransition();

            IState<TestTrigger> result = this.testee.State.Transitions.GetNextState<TestTrigger>(TestTrigger.TransitionOne);
            result.Should().Be(this.testee.State);
        }

        [Fact]
        public void AddTransition_WhenNewStateGiven_MustAddTransitionWithCorrectTrigger()
        {
            this.testee.CurrentTrigger = TestTrigger.TransitionOne;

            this.testee.AddTransition(new AnotherTestableState());

            IState<TestTrigger> result = this.testee.State.Transitions.GetNextState(TestTrigger.TransitionTwo);
            result.Should().BeNull();
        }

        [Fact]
        public void AddTransition_WhenNewStateGiven_MustAddTransitionWithNewState()
        {
            var expectedState = new AnotherTestableState();
            this.testee.CurrentTrigger = TestTrigger.TransitionOne;

            this.testee.AddTransition(expectedState);

            IState<TestTrigger> result = this.testee.State.Transitions.GetNextState<TestTrigger>(TestTrigger.TransitionOne);
            result.Should().Be(expectedState);
        }
    }
}