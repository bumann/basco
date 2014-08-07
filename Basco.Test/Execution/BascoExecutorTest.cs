namespace Basco.Test.Execution
{
    using System;
    using Basco.Execution;
    using Basco.Test.Utilities;
    using FluentAssertions;
    using Moq;
    using Xunit;

    public class BascoExecutorTest
    {
        private readonly Mock<IBascoStateEnterExecutor> enterExecutor;
        private readonly Mock<IBascoStateExitExecutor> exitExecutor;
        private readonly Mock<IBascoStatesProvider<TestTrigger>> stateProvider;
        private readonly BascoExecutor<TestTrigger> testee;

        public BascoExecutorTest()
        {
            this.stateProvider = new Mock<IBascoStatesProvider<TestTrigger>> { DefaultValue = DefaultValue.Mock };
            this.enterExecutor = new Mock<IBascoStateEnterExecutor>();
            this.exitExecutor = new Mock<IBascoStateExitExecutor>();
            this.testee = new BascoExecutor<TestTrigger>(
                this.stateProvider.Object, 
                this.enterExecutor.Object,
                this.exitExecutor.Object);
        }

        [Fact]
        public void InitializeWithStartState_MustSetInitialState()
        {
            var state = Mock.Of<IState>();
            this.stateProvider.Setup(x => x.Retrieve(It.IsAny<Type>())).Returns(state);

            this.testee.InitializeWithStartState<IState>(Mock.Of<IBasco<TestTrigger>>());

            this.testee.CurrentState.Should().Be(state);
        }

        [Fact]
        public void InitializeWithStartState_WhenNoValidStartState_MustThrow()
        {
            this.stateProvider.Setup(x => x.Retrieve(It.IsAny<Type>())).Returns<IState>(null);

            this.testee.Invoking(x => x.InitializeWithStartState<ExtendedTestState>(Mock.Of<IBasco<TestTrigger>>()))
                .ShouldThrow<BascoException>();
        }

        [Fact]
        public void Start_MustEnterState()
        {
            var state = new Mock<IState>();
            this.stateProvider.Setup(x => x.Retrieve(It.IsAny<Type>())).Returns(state.Object);
            this.testee.InitializeWithStartState<ExtendedTestState>(Mock.Of<IBasco<TestTrigger>>());

            this.testee.Start();

            this.enterExecutor.Verify(x => x.Enter(this.testee.CurrentState), Times.Once);
        }

        [Fact]
        public void Start_MustPerformExecuteOnInitialState()
        {
            var state = new Mock<IState>();
            this.stateProvider.Setup(x => x.Retrieve(It.IsAny<Type>())).Returns(state.Object);
            this.testee.InitializeWithStartState<ExtendedTestState>(Mock.Of<IBasco<TestTrigger>>());

            this.testee.Start();

            state.Verify(x => x.Execute(), Times.Once);
        }

        [Fact]
        public void Start_MustRaiseStateChanged()
        {
            this.testee.InitializeWithStartState<ExtendedTestState>(Mock.Of<IBasco<TestTrigger>>());
            this.testee.MonitorEvents();

            this.testee.Start();

            this.testee.ShouldRaise<IBascoExecutor<TestTrigger>>(x => x.StateChanged += null);
        }

        [Fact]
        public void Stop_MustRaiseStateChanged()
        {
            this.testee.InitializeWithStartState<ExtendedTestState>(Mock.Of<IBasco<TestTrigger>>());
            this.testee.MonitorEvents();

            this.testee.Stop();

            this.testee.ShouldRaise<IBascoExecutor<TestTrigger>>(x => x.StateChanged += null);
        }

        [Fact]
        public void ChangeState_WhenInitialStateNotSet_MustNotThrow()
        {
            this.Invoking(x => this.testee.ChangeState(TestTrigger.TransitionOne))
                .ShouldNotThrow();
        }

        [Fact]
        public void ChangeState_WhenNextStateNotSet_MustNotThrow()
        {
            this.testee.InitializeWithStartState<ExtendedTestState>(Mock.Of<IBasco<TestTrigger>>());

            this.testee.Start();

            this.Invoking(x => this.testee.ChangeState(TestTrigger.TransitionTwo))
                .ShouldNotThrow();
        }

        [Fact]
        public void ChangeState_MustExitPreviousState()
        {
            var initialState = Mock.Of<IState>();
            this.stateProvider.Setup(x => x.Retrieve(typeof(ExtendedTestState)))
                .Returns(initialState);
            this.stateProvider.Setup(x => x.RetrieveNext(It.IsAny<IState>(), It.IsAny<TestTrigger>()))
                .Returns(Mock.Of<IState>());
            this.testee.InitializeWithStartState<ExtendedTestState>(Mock.Of<IBasco<TestTrigger>>());

            this.testee.ChangeState(TestTrigger.TransitionOne);

            this.exitExecutor.Verify(x => x.Exit(initialState), Times.Once);
        }

        [Fact]
        public void ChangeState_MustSetNextState()
        {
            var nextState = Mock.Of<IState>();
            this.stateProvider.Setup(x => x.RetrieveNext(It.IsAny<IState>(), It.IsAny<TestTrigger>()))
                .Returns(nextState);
            this.testee.InitializeWithStartState<ExtendedTestState>(Mock.Of<IBasco<TestTrigger>>());

            this.testee.ChangeState(TestTrigger.TransitionOne);

            this.testee.CurrentState.Should().Be(nextState);
        }

        [Fact]
        public void ChangeState_MustEnterNextState()
        {
            var nextState = Mock.Of<IState>();
            this.stateProvider.Setup(x => x.RetrieveNext(It.IsAny<IState>(), It.IsAny<TestTrigger>()))
                .Returns(nextState);
            this.testee.InitializeWithStartState<ExtendedTestState>(Mock.Of<IBasco<TestTrigger>>());

            this.testee.ChangeState(TestTrigger.TransitionOne);

            this.enterExecutor.Verify(x => x.Enter(nextState), Times.Once);
        }

        [Fact]
        public void ChangeState_MustExecuteNextState()
        {
            var nextState = new Mock<IState>();
            this.stateProvider.Setup(x => x.RetrieveNext(It.IsAny<IState>(), It.IsAny<TestTrigger>()))
                .Returns(nextState.Object);
            this.testee.InitializeWithStartState<ExtendedTestState>(Mock.Of<IBasco<TestTrigger>>());
            
            this.testee.ChangeState(TestTrigger.TransitionOne);

            nextState.Verify(x => x.Execute(), Times.Once);
        }

        [Fact]
        public void ChangeState_MustRaiseStateChanged()
        {
            this.stateProvider.Setup(x => x.RetrieveNext(It.IsAny<IState>(), It.IsAny<TestTrigger>()))
                .Returns(Mock.Of<IState>());
            this.testee.InitializeWithStartState<ExtendedTestState>(Mock.Of<IBasco<TestTrigger>>());
            this.testee.MonitorEvents();

            this.testee.ChangeState(TestTrigger.TransitionOne);

            this.testee.ShouldRaise<IBascoExecutor<TestTrigger>>(x => x.StateChanged += null);
        }

        [Fact]
        public void RetrieveState_MustReturnState()
        {
            var state = Mock.Of<IState>();
            this.stateProvider.Setup(x => x.Retrieve(typeof(ExtendedTestState))).Returns(state);

            IState result = this.testee.RetrieveState<ExtendedTestState>();

            result.Should().Be(state);
        }

        [Fact]
        public void RetrieveState_WhenStateNotExists_MustReturnNull()
        {
            this.stateProvider.Setup(x => x.Retrieve(It.IsAny<Type>())).Returns<IState>(null);

            IState result = this.testee.RetrieveState<SimpleTestState>();

            result.Should().BeNull();
        }
    }
}