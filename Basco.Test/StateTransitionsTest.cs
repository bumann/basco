namespace Basco.Test
{
    using System;
    using System.Collections.Generic;
    using FluentAssertions;
    using Xunit;

    public class StateTransitionsTest
    {
        private readonly StateTransitions<TestTrigger> testee;

        public StateTransitionsTest()
        {
            this.testee = new StateTransitions<TestTrigger>();
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
            Dictionary<TestTrigger, Type>.ValueCollection result = this.testee.Transitions.Values;

            result.Count.Should().Be(0);
        }

        [Fact]
        public void Add_WhenTriggerAdded_MustContainTrigger()
        {
            this.testee.Add(TestTrigger.TransitionOne, this.GetType());

            bool result = this.testee.Transitions.ContainsKey(TestTrigger.TransitionOne);

            result.Should().BeTrue();
        }

        [Fact]
        public void Add_WhenTypeAdded_MustContainType()
        {
            Type type = this.GetType();
            this.testee.Add(TestTrigger.TransitionTwo, type);

            bool result = this.testee.Transitions.ContainsValue(type);

            result.Should().BeTrue();
        }
    }
}