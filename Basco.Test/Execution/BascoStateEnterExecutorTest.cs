namespace Basco.Test.Execution
{
    using Basco.Execution;
    using Basco.Test.Utilities;
    using FluentAssertions;
    using Moq;
    using Xunit;

    public class BascoStateEnterExecutorTest
    {
        private readonly BascoStateEnterExecutor testee;

        public BascoStateEnterExecutorTest()
        {
            this.testee = new BascoStateEnterExecutor();
        }

        public interface ITestState : IState
        {
            void Enter();
        }

        [Fact]
        public void Enter_WhenStateIsEnterable_MustEnterState()
        {
            var enterableState = new Mock<IStateEnter>();

            this.testee.Enter(enterableState.Object);

            enterableState.Verify(x => x.Enter(), Times.Once);
        }

        [Fact]
        public void Enter_WhenStateIsNotEnterable_MustNotEnterState()
        {
            var notEnterableState = new Mock<ITestState>();

            this.testee.Enter(notEnterableState.Object);

            notEnterableState.Verify(x => x.Enter(), Times.Never());
        }

        [Fact]
        public void Enter_WhenStateIsEntered_MustRaiseStateEntered()
        {
            var enterableState = new Mock<IStateEnter>();
            this.testee.MonitorEvents();

            this.testee.Enter(enterableState.Object);

            this.testee.ShouldRaise<IBascoStateEnterExecutor>(x => x.StateEntered += null);
        }
    }
}