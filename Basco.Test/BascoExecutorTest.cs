namespace Basco.Test
{
    using System;
    using FluentAssertions;
    using Moq;
    using Xunit;

    public class BascoExecutorTest
    {
        private readonly Mock<IBascoStateCache> stateCache;
        private readonly SimpleTestState simpleTestState;
        private readonly ExtendedTestState extendedTestState;
        private readonly BascoExecutor<TestTrigger> testee;

        public BascoExecutorTest()
        {
            this.simpleTestState = new SimpleTestState();
            this.extendedTestState = new ExtendedTestState();
            this.stateCache = new Mock<IBascoStateCache> { DefaultValue = DefaultValue.Mock };
            this.testee = new BascoExecutor<TestTrigger>(this.stateCache.Object);
        }

        [Fact]
        public void InitializeWithStartState_MustInitializeStateCache()
        {
            this.testee.InitializeWithStartState<IState>();

            this.stateCache.Verify(x => x.Initialize());
        }

        [Fact]
        public void InitializeWithStartState_MustSetInitialState()
        {
            var state = Mock.Of<IState>();
            this.stateCache.Setup(x => x.GetState(It.IsAny<Type>())).Returns(state);

            this.testee.InitializeWithStartState<IState>();

            this.testee.CurrentState.Should().Be(state);
        }

        [Fact]
        public void InitializeWithStartState_WhenNoValidStartState_MustThrow()
        {
            this.stateCache.Setup(x => x.GetState(It.IsAny<Type>())).Returns<IState>(null);

            this.testee.Invoking(x => x.InitializeWithStartState<ExtendedTestState>())
                .ShouldThrow<BascoException>();
        }

        [Fact]
        public void Start_WhenEnterableInitialState_MustEnterInitialState()
        {
            var state = new Mock<IStateEnter>();
            this.stateCache.Setup(x => x.GetState(It.IsAny<Type>())).Returns(state.Object);
            this.testee.InitializeWithStartState<IState>();

            this.testee.Start();

            state.Verify(x => x.Enter(), Times.Once);
        }

        [Fact]
        public void Start_WhenNotEnterableInitialState_MustNotEnterInitialState()
        {
            bool called = false;
            this.simpleTestState.OnEnter = () => { called = true; };
            this.testee.InitializeWithStartState<SimpleTestState>();

            this.testee.Start();

            called.Should().BeFalse();
        }

        [Fact]
        public void Start_MustPerformExecuteOnInitialState()
        {
            var state = new Mock<IState>();
            this.stateCache.Setup(x => x.GetState(It.IsAny<Type>())).Returns(state.Object);
            this.testee.InitializeWithStartState<ExtendedTestState>();

            this.testee.Start();

            state.Verify(x => x.Execute(), Times.Once);
        }

        [Fact]
        public void Start_MustRaiseStateChanged()
        {
            bool called = false;
            this.testee.StateChanged += (sender, args) => { called = true; };
            this.testee.InitializeWithStartState<ExtendedTestState>();

            this.testee.Start();

            called.Should().BeTrue();
        }

        [Fact]
        public void Stop_MustResetInitialState()
        {
            this.testee.Stop();

            this.testee.CurrentState.Should().BeNull();
        }

        [Fact]
        public void Stop_WhenExitableState_MustExitState()
        {
            var state = new Mock<IStateExit>();
            this.stateCache.Setup(x => x.GetState(It.IsAny<Type>())).Returns(state.Object);
            this.testee.InitializeWithStartState<IStateExit>();

            this.testee.Stop();

            state.Verify(x => x.Exit(), Times.Once);
        }

        [Fact]
        public void Stop_WhenNotExitableState_MustNotExitState()
        {
            bool called = false;
            this.testee.InitializeWithStartState<SimpleTestState>();
            this.simpleTestState.OnExit = () => { called = true; };

            this.testee.Stop();

            called.Should().BeFalse();
        }

        [Fact]
        public void Stop_MustRaiseStateChanged()
        {
            bool called = false;
            this.testee.InitializeWithStartState<ExtendedTestState>();
            this.testee.StateChanged += (sender, args) => { called = true; };

            this.testee.Stop();

            called.Should().BeTrue();
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
            this.testee.InitializeWithStartState<ExtendedTestState>();

            this.testee.Start();

            this.Invoking(x => this.testee.ChangeState(TestTrigger.TransitionOne))
                .ShouldNotThrow();
        }

        [Fact]
        public void ChangeState_WhenStateNotConfigured_MustThrow()
        {
            this.SetupExtendedStateTransitions();
            this.testee.InitializeWithStartState<NotConfiguredState>();

            this.testee.Start();

            this.Invoking(x => this.testee.ChangeState(TestTrigger.TransitionOne))
                .ShouldThrow<BascoException>();
        }

        [Fact]
        public void ChangeState_WhenExitableState_MustExitInitialState()
        {
            var state = new ExtendedTestState();
            this.stateCache.Setup(x => x.GetState(It.IsAny<Type>())).Returns(state);
            bool called = false;
            state.OnExit = () => { called = true; };
            this.SetupExtendedStateTransitions();
            this.testee.InitializeWithStartState<ExtendedTestState>();
            this.testee.Start();

            this.testee.ChangeState(TestTrigger.TransitionOne);

            called.Should().BeTrue();
        }

        [Fact]
        public void ChangeState_MustSetNextState()
        {
            this.stateCache.Setup(x => x.GetState(typeof(ExtendedTestState))).Returns(this.extendedTestState);
            this.stateCache.Setup(x => x.GetState(typeof(SimpleTestState))).Returns(this.simpleTestState);
            this.SetupExtendedStateTransitions();
            this.testee.InitializeWithStartState<ExtendedTestState>();
            this.testee.Start();

            this.testee.ChangeState(TestTrigger.TransitionOne);

            IState result = this.testee.CurrentState;
            result.Should().Be(this.simpleTestState);
        }

        [Fact]
        public void ChangeState_WhenNextStateIsEnterable_MustEnterNextState()
        {
            this.stateCache.Setup(x => x.GetState(typeof(ExtendedTestState))).Returns(this.extendedTestState);
            this.stateCache.Setup(x => x.GetState(typeof(SimpleTestState))).Returns(this.simpleTestState);
            bool called = false;
            this.SetupSimpleStateTransitions();
            this.extendedTestState.OnEnter = () => { called = true; };
            this.testee.InitializeWithStartState<SimpleTestState>();
            this.testee.Start();

            this.testee.ChangeState(TestTrigger.TransitionOne);

            called.Should().BeTrue();
        }

        [Fact]
        public void ChangeState_MustExecuteNextState()
        {
            this.stateCache.Setup(x => x.GetState(typeof(ExtendedTestState))).Returns(this.extendedTestState);
            this.stateCache.Setup(x => x.GetState(typeof(SimpleTestState))).Returns(this.simpleTestState);
            bool called = false;
            this.SetupExtendedStateTransitions();
            this.simpleTestState.OnExecution = () => { called = true; };
            this.testee.InitializeWithStartState<ExtendedTestState>();
            this.testee.Start();
            
            this.testee.ChangeState(TestTrigger.TransitionOne);

            called.Should().BeTrue();
        }

        [Fact]
        public void ChangeState_MustRaiseStateChanged()
        {
            this.stateCache.Setup(x => x.GetState(typeof(ExtendedTestState))).Returns(this.extendedTestState);
            this.stateCache.Setup(x => x.GetState(typeof(SimpleTestState))).Returns(this.simpleTestState);
            bool called = false;
            this.SetupExtendedStateTransitions();
            this.testee.InitializeWithStartState<ExtendedTestState>();
            this.testee.Start();
            this.testee.StateChanged += (sender, args) => { called = true; };
            
            this.testee.ChangeState(TestTrigger.TransitionOne);
            
            called.Should().BeTrue();
        }

        [Fact]
        public void RetrieveState_MustReturnCacheState()
        {
            var state = Mock.Of<IState>();
            this.stateCache.Setup(x => x.GetState(typeof(ExtendedTestState))).Returns(state);

            IState result = this.testee.RetrieveState<ExtendedTestState>();

            result.Should().Be(state);
        }

        [Fact]
        public void RetrieveState_WhenStateNotExists_MustReturnNull()
        {
            this.stateCache.Setup(x => x.GetState(It.IsAny<Type>())).Returns<IState>(null);

            IState result = this.testee.RetrieveState<FakeState>();

            result.Should().BeNull();
        }

        private void SetupSimpleStateTransitions()
        {
            var transitions = new StateTransitions<TestTrigger>();
            transitions.Add(TestTrigger.TransitionOne, typeof(ExtendedTestState));
            this.testee.AddStateTransitions<SimpleTestState>(transitions);
        }

        private void SetupExtendedStateTransitions()
        {
            var transitions = new StateTransitions<TestTrigger>();
            transitions.Add(TestTrigger.TransitionOne, typeof(SimpleTestState));
            this.testee.AddStateTransitions<ExtendedTestState>(transitions);
        }

        private class FakeState : IState
        {
            public void Execute()
            {
            }
        }

        private class NotConfiguredState : IState
        {
            public void Execute()
            {
            }
        }
    }
}