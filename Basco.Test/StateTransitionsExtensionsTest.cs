namespace Basco.Test
{
    using System;
    using FluentAssertions;
    using Xunit;

    public class StateTransitionsExtensionsTest
    {
        [Fact]
        public void GetNextStateType_WhenTransitionIsNotDefined_MustReturnNull()
        {
            var stateTransitions = new StateTransitions<TestTrigger>();

            Type result = stateTransitions.GetNextStateType(TestTrigger.TransitionOne);

            result.Should().BeNull();
        }

        [Fact]
        public void GetNextStateType_WhenTransitionIsDefined_MustReturnCorrectType()
        {
            Type expectedType = this.GetType();
            var transitions = new StateTransitions<TestTrigger>();
            transitions.Add(TestTrigger.TransitionOne, expectedType);

            Type result = transitions.GetNextStateType(TestTrigger.TransitionOne);

            result.Should().Be(expectedType);
        }
    }
}