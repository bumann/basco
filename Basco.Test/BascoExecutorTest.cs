namespace Basco.Test
{
    using FluentAssertions;
    using Xunit;

    public class BascoExecutorTest
    {
        private readonly BascoExecutor<TestTrigger> testee;
        private TestableState initialState;
        private TestableState nextState;

        public BascoExecutorTest()
        {
            this.testee = new BascoExecutor<TestTrigger>();
        }

        [Fact]
        public void Start_MustSetInitialState()
        {
            var expectedState = new TestableState();

            this.testee.Start(expectedState);

            this.testee.CurrentState.Should().Be(expectedState);
        }

        [Fact]
        public void Start_WhenEnterableInitialState_MustEnterInitialState()
        {
            bool called = false;
            this.SetupStates();
            this.initialState.OnEnter = () => { called = true; };

            this.testee.Start(this.initialState);

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
            bool called = false;
            this.SetupStates();
            this.initialState.OnExit = () => { called = true; };
            this.testee.Start(this.initialState);

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
            this.initialState = new TestableState();
            this.testee.Start(this.initialState);

            this.Invoking(x => this.testee.ChangeState(TestTrigger.TransitionOne))
                .ShouldNotThrow();
        }

        [Fact]
        public void ChangeState_WhenExitableState_MustExitInitialState()
        {
            bool called = false;
            this.SetupStates();
            this.initialState.OnExit = () => { called = true; };
            this.testee.Start(this.initialState);
            
            this.testee.ChangeState(TestTrigger.TransitionOne);

            called.Should().BeTrue();
        }

        [Fact]
        public void ChangeState_MustSetNextState()
        {
            this.SetupStates();
            this.testee.Start(this.initialState);
            
            this.testee.ChangeState(TestTrigger.TransitionOne);

            IState<TestTrigger> result = this.testee.CurrentState;
            result.Should().Be(this.nextState);
        }

        [Fact]
        public void ChangeState_WhenEnterableState_MustEnterNextState()
        {
            bool called = false;
            this.SetupStates();
            this.nextState.OnEnter = () => { called = true; };
            this.testee.Start(this.initialState);

            this.testee.ChangeState(TestTrigger.TransitionOne);

            called.Should().BeTrue();
        }

        [Fact]
        public void ChangeState_MustExecuteNextState()
        {
            bool called = false;
            this.SetupStates();
            this.nextState.OnExecution = () => { called = true; };
            this.testee.Start(this.initialState);

            this.testee.ChangeState(TestTrigger.TransitionOne);

            called.Should().BeTrue();
        }

        private void SetupStates()
        {
            this.initialState = new TestableState();
            this.nextState = new TestableState();
            this.initialState.Transitions.Add(TestTrigger.TransitionOne, this.nextState);
        }
    }
}