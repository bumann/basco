namespace Basco.Test.Execution
{
    using System.Collections.Generic;
    using Basco.Execution;
    using FluentAssertions;
    using Moq;
    using Xunit;

    public class BascoStateCacheTest
    {
        private readonly Mock<IState> simpleState;
        private readonly Mock<IStateUsingBasco<TestTrigger>> bascoUsingState;
        private readonly BascoStateCache<TestTrigger> testee;

        public BascoStateCacheTest()
        {
            this.bascoUsingState = new Mock<IStateUsingBasco<TestTrigger>>();
            this.simpleState = new Mock<IState>();
            this.testee = new BascoStateCache<TestTrigger>();
        }

        [Fact]
        public void Initialize_MustSetBascoOnStatesUsingIt()
        {
            var basco = Mock.Of<IBasco<TestTrigger>>();
            
            this.testee.Initialize(basco, new[] { this.bascoUsingState.Object, this.simpleState.Object });

            this.bascoUsingState.VerifySet(x => x.Basco = basco, Times.Once);
        }

        [Fact]
        public void Initialize_WhenNoStatesAvailable_MustThrow()
        {
            this.testee.Invoking(x => x.Initialize(Mock.Of<IBasco<TestTrigger>>(), new List<IState>()))
                .ShouldThrow<BascoException>();
        }

        [Fact]
        public void RetrieveState_MustReturnState()
        {
            var expectedState = new SimpleTestState();
            this.testee.Initialize(Mock.Of<IBasco<TestTrigger>>(), new[] { expectedState });

            IState result = this.testee.RetrieveState(expectedState.GetType());

            result.Should().Be(expectedState);
        }

        [Fact]
        public void RetrieveStates_MustReturnStates()
        {
            var state = Mock.Of<IState>();
            this.testee.Initialize(Mock.Of<IBasco<TestTrigger>>(), new[] { state, Mock.Of<IStateEnter>() });

            IEnumerable<IState> result = this.testee.RetrieveStates(new[] { state.GetType() });

            result.Should().HaveCount(1)
                .And.Contain(state);
        }
    }
}