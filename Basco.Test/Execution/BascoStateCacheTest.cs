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

        public BascoStateCacheTest()
        {
            this.bascoUsingState = new Mock<IStateUsingBasco<TestTrigger>>();
            this.simpleState = new Mock<IState>();
        }

        [Fact]
        public void Initialize_MustSetBascoOnStatesUsingIt()
        {
            var basco = Mock.Of<IBasco<TestTrigger>>();
            var testee = new BascoStateCache<TestTrigger>(new[] { this.bascoUsingState.Object, this.simpleState.Object });
            
            testee.Initialize(basco);

            this.bascoUsingState.VerifySet(x => x.Basco = basco, Times.Once);
        }

        [Fact]
        public void Initialize_WhenNoStatesAvailable_MustThrow()
        {
            var testee = new BascoStateCache<TestTrigger>(new List<IState>());
            
            testee.Invoking(x => x.Initialize(Mock.Of<IBasco<TestTrigger>>()))
                .ShouldThrow<BascoException>();
        }

        [Fact]
        public void RetrieveState_MustReturnState()
        {
            var expectedState = new SimpleTestState();
            var testee = new BascoStateCache<TestTrigger>(new[] { expectedState });
            testee.Initialize(Mock.Of<IBasco<TestTrigger>>());

            IState result = testee.RetrieveState(expectedState.GetType());

            result.Should().Be(expectedState);
        }
    }
}