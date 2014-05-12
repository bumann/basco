namespace Basco.Test
{
    using FluentAssertions;
    using Xunit;

    public class TransitionPoolExtensionsTest
    {
        [Fact]
        public void GetNextState_WhenTransitionIsNotDefined_MustReturnNull()
        {
            IState<TestTrigger> result = (new TransitionPool<TestTrigger>()).GetNextState(TestTrigger.TransitionOne);

            result.Should().BeNull();
        }

        [Fact]
        public void GetNextState_WhenTransitionIsDefined_MustReturnCorrectState()
        {
            var transitionPool = new TransitionPool<TestTrigger>();
            var expectedState = new TestableState();
            transitionPool.Add(TestTrigger.TransitionOne, expectedState);

            IState<TestTrigger> result = transitionPool.GetNextState(TestTrigger.TransitionOne);

            result.Should().Be(expectedState);
        }
    }
}