namespace Basco.Test.Execution
{
    using System;
    using Basco.Execution;
    using FluentAssertions;
    using Moq;
    using Xunit;

    public class BascoNextStateProviderTest
    {
        private readonly Mock<IBascoStateCache<TestTrigger>> stateCache;
        private readonly Mock<IBascoTransitionCache<TestTrigger>> transitionCache;
        private readonly BascoStatesProvider<TestTrigger> testee;

        public BascoNextStateProviderTest()
        {
            this.stateCache = new Mock<IBascoStateCache<TestTrigger>>();
            this.transitionCache = new Mock<IBascoTransitionCache<TestTrigger>>();
            this.testee = new BascoStatesProvider<TestTrigger>(this.stateCache.Object, this.transitionCache.Object);
        }

        [Fact]
        public void RetrieveNext_MustRetrieveNextStateType()
        {
            var state = Mock.Of<IState>();
            const TestTrigger TransitionOne = TestTrigger.TransitionOne;

            this.testee.RetrieveNext(state, TransitionOne);

            this.transitionCache.Verify(x => x.RetrieveNextStateType(state, TransitionOne), Times.Once);
        }

        [Fact]
        public void RetrieveNext_MustRetrieveCorrectState()
        {
            Type stateType = typeof(object);
            this.transitionCache.Setup(x => x.RetrieveNextStateType(It.IsAny<IState>(), It.IsAny<TestTrigger>()))
                .Returns(stateType);

            this.testee.RetrieveNext(Mock.Of<IState>(), TestTrigger.TransitionOne);

            this.stateCache.Verify(x => x.RetrieveState(stateType), Times.Once);
        }

        [Fact]
        public void RetrieveNext_MustReturnState()
        {
            var state = Mock.Of<IState>();
            this.stateCache.Setup(x => x.RetrieveState(It.IsAny<Type>()))
                .Returns(state);

            IState result = this.testee.RetrieveNext(Mock.Of<IState>(), TestTrigger.TransitionOne);

            result.Should().Be(state);
        }
    }
}