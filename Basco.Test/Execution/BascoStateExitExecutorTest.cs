namespace Basco.Test.Execution
{
    using Basco.Execution;
    using Basco.Test.Utilities;
    using FluentAssertions;
    using Moq;
    using Xunit;

    public class BascoStateExitExecutorTest
    {
        private readonly BascoStateExitExecutor testee;

        public BascoStateExitExecutorTest()
        {
            this.testee = new BascoStateExitExecutor();
        }

        public interface ITestState : IState
        {
            void Exit();
        }

        [Fact]
        public void Exit_WhenStateIsExitable_MustExitState()
        {
            var enterableState = new Mock<IStateExit>();

            this.testee.Exit(enterableState.Object);

            enterableState.Verify(x => x.Exit(), Times.Once);
        }

        [Fact]
        public void Exit_WhenStateIsNotExitable_MustNotExitState()
        {
            var notEnterableState = new Mock<ITestState>();

            this.testee.Exit(notEnterableState.Object);

            notEnterableState.Verify(x => x.Exit(), Times.Never());
        }

        [Fact]
        public void Exit_WhenStateIsExitable_MustRaiseStateExiting()
        {
            var enterableState = new Mock<IStateExit>();
            this.testee.MonitorEvents();

            this.testee.Exit(enterableState.Object);

            this.testee.ShouldRaise<IBascoStateExitExecutor>(x => x.StateExiting += null);
        }
    }
}