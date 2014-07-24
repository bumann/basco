namespace Basco.Test
{
    using FluentAssertions;
    using Moq;
    using Xunit;

    public class BascoStateCacheTest
    {
        private readonly Mock<IBascoStatesFactory<TestTrigger>> factory;
        private readonly BascoStateCache<TestTrigger> testee;

        public BascoStateCacheTest()
        {
            this.factory = new Mock<IBascoStatesFactory<TestTrigger>>();
            this.testee = new BascoStateCache<TestTrigger>(this.factory.Object);
        }

        [Fact]
        public void Initialize_MustCreateStates()
        {
            var basco = Mock.Of<IBasco<TestTrigger>>();
            this.factory.Setup(x => x.CreateStates(It.IsAny<IBasco<TestTrigger>>())).Returns(new[] { new SimpleTestState() });

            this.testee.Initialize(basco);

            this.factory.Verify(x => x.CreateStates(basco), Times.Once);
        }

        [Fact]
        public void Initialize_WhenNoStatesCreated_MustThrow()
        {
            this.testee.Invoking(x => x.Initialize(Mock.Of<IBasco<TestTrigger>>()))
                .ShouldThrow<BascoException>();
        }

        [Fact]
        public void GetState_MustReturnState()
        {
            var expectedState = new SimpleTestState();
            this.factory.Setup(x => x.CreateStates(It.IsAny<IBasco<TestTrigger>>())).Returns(new[] { expectedState });
            this.testee.Initialize(Mock.Of<IBasco<TestTrigger>>());

            var result = this.testee.RetrieveState(expectedState.GetType());

            result.Should().Be(expectedState);
        }
    }
}