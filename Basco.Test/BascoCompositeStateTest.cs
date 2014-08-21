namespace Basco.Test
{
    using FluentAssertions;
    using Moq;
    using Xunit;

    public class BascoCompositeStateTest
    {
        private readonly BascoCompositeState<TestTrigger, SimpleTestState> testee;

        public BascoCompositeStateTest()
        {
            this.testee = new BascoCompositeState<TestTrigger, SimpleTestState>();
        }

        [Fact]
        public void Initialize_WhenStateNull_MustThrow()
        {
            var basco = new Mock<IBasco<TestTrigger>>();

            this.testee.Invoking(x => x.Initialize(basco.Object, null))
                .ShouldThrow<BascoException>();
        }

        [Fact]
        public void Enter_MustStartSubBasco()
        {
            var basco = new Mock<IBasco<TestTrigger>>();
            this.testee.Initialize(basco.Object, Mock.Of<IState>());

            this.testee.Enter();

            basco.Verify(x => x.Start(), Times.Once);
        }

        [Fact]
        public void Enter_MustEnterEnterableState()
        {
            var state = new Mock<IStateEnter>();
            this.testee.Initialize(new Mock<IBasco<TestTrigger>>().Object, state.Object);

            this.testee.Enter();

            state.Verify(x => x.Enter(), Times.Once);
        }

        [Fact]
        public void Execute_MustExecuteState()
        {
            var state = new Mock<IState>();
            this.testee.Initialize(Mock.Of<IBasco<TestTrigger>>(), state.Object);

            this.testee.Execute();

            state.Verify(x => x.Execute(), Times.Once);
        }

        [Fact]
        public void Exit_MustStopSubBasco()
        {
            var basco = new Mock<IBasco<TestTrigger>>();
            this.testee.Initialize(basco.Object, Mock.Of<IState>());

            this.testee.Exit();

            basco.Verify(x => x.Stop(), Times.Once);
        }

        [Fact]
        public void Exit_MustExitExitableState()
        {
            var state = new Mock<IStateExit>();
            this.testee.Initialize(new Mock<IBasco<TestTrigger>>().Object, state.Object);

            this.testee.Exit();

            state.Verify(x => x.Exit(), Times.Once);
        }
    }
}