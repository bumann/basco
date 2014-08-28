namespace Basco.Test.Execution
{
    using System;
    using Basco.Configuration;
    using Basco.Execution;
    using FluentAssertions;
    using Moq;
    using Xunit;

    public class BascoTransitionCacheTest
    {
        private readonly BascoTransitionCache<TestTrigger> testee;

        public BascoTransitionCacheTest()
        {
            this.testee = new BascoTransitionCache<TestTrigger>();
        }

        [Fact]
        public void RetrieveNextStateType_WhenNoTransitionsDefined_MustReturnNull()
        {
            Type result = this.testee.RetrieveNextStateType(Mock.Of<IState>(), TestTrigger.TransitionOne);

            result.Should().BeNull();
        }

        [Fact]
        public void RetrieveNextStateType_WhenNoTransitionsForStateDefined_MustThrow()
        {
            this.testee.Add(typeof(object), new StateTransitions<TestTrigger>());

            this.testee.Invoking(x => x.RetrieveNextStateType(Mock.Of<IState>(), TestTrigger.TransitionOne))
                .ShouldThrow<BascoException>();
        }

        [Fact]
        public void RetrieveNextStateType_WhenTransitionsDefined_MustReturnNextStateType()
        {
            var state = Mock.Of<IState>();
            var nextStateType = typeof(object);
            var stateTransitions = new StateTransitions<TestTrigger>();
            stateTransitions.Add(TestTrigger.TransitionOne, nextStateType);
            this.testee.Add(state.GetType(), stateTransitions);

            Type result = this.testee.RetrieveNextStateType(state, TestTrigger.TransitionOne);

            result.Should().Be(nextStateType);
        }

        [Fact]
        public void RetrieveNextStateType_WhenCurrentStateIsComposite_MustReturnCorrectNextStateType()
        {
            var baseStateType = typeof(object);
            var nextStateType = typeof(string);
            var state = Mock.Of<IBascoCompositeState<TestTrigger>>(x => x.BaseStateType == baseStateType);
            var stateTransitions = new StateTransitions<TestTrigger>();
            stateTransitions.Add(TestTrigger.TransitionOne, nextStateType);
            this.testee.Add(baseStateType, stateTransitions);

            Type result = this.testee.RetrieveNextStateType(state, TestTrigger.TransitionOne);

            result.Should().Be(nextStateType);
        }
    }
}