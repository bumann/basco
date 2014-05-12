namespace Basco.Test
{
    using System.Collections.Generic;
    using FluentAssertions;
    using Xunit;

    public class TransitionPoolTest
    {
        private readonly TransitionPool<TestTrigger> testee;

        public TransitionPoolTest()
        {
            this.testee = new TransitionPool<TestTrigger>();
        }

        [Fact]
        public void Transitions_WhenTriggerNotAdded_MustNotContainTrigger()
        {
            bool result = this.testee.Transitions.ContainsKey(TestTrigger.TransitionOne);

            result.Should().BeFalse();
        }

        [Fact]
        public void Transitions_WhenStateNotAdded_MustNotContainState()
        {
            Dictionary<TestTrigger, IState<TestTrigger>>.ValueCollection result = this.testee.Transitions.Values;

            result.Count.Should().Be(0);
        }

        [Fact]
        public void Add_WhenTriggerAdded_MustAddTrigger()
        {
            this.testee.Add(TestTrigger.TransitionOne, new TestableState());

            var result = this.testee.Transitions.ContainsKey(TestTrigger.TransitionOne);
            result.Should().BeTrue();
        }

        [Fact]
        public void Add_WhenStateAdded_MustAddState()
        {
            var state = new TestableState();
            this.testee.Add(TestTrigger.TransitionTwo, state);

            var result = this.testee.Transitions.ContainsValue(state);
            result.Should().BeTrue();
        }
    }
}