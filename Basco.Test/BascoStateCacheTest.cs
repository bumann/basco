namespace Basco.Test
{
    using FluentAssertions;
    using Moq;
    using Xunit;

    public class BascoStateCacheTest
    {
        private readonly Mock<IBascoFactory> factory;
        private readonly BascoStateCache testee;

        public BascoStateCacheTest()
        {
            this.factory = new Mock<IBascoFactory>();
            this.testee = new BascoStateCache(this.factory.Object);
        }

        [Fact]
        public void Initialize_MustCreateStates()
        {
            this.factory.Setup(x => x.CreateAll<IState>()).Returns(new[] { new SimpleTestState() });

            this.testee.Initialize();

            this.factory.Verify(x => x.CreateAll<IState>(), Times.Once);
        }

        [Fact]
        public void Initialize_WhenNoStatesCreated_MustThrow()
        {
            this.testee.Invoking(x => x.Initialize())
                .ShouldThrow<BascoException>();
        }

        [Fact]
        public void GetState_MustReturnState()
        {
            var expectedState = new SimpleTestState();
            this.factory.Setup(x => x.CreateAll<IState>()).Returns(new[] { expectedState });
            this.testee.Initialize();

            var result = this.testee.GetState(expectedState.GetType());

            result.Should().Be(expectedState);
        }
    }
}