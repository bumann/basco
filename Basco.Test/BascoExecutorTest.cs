﻿namespace Basco.Test
{
    using System.Collections.Generic;
    using FluentAssertions;
    using Xunit;

    public class BascoExecutorTest
    {
        private readonly SimpleTestState simpleTestState;
        private readonly ExtendedTestState extendedTestState;
        private readonly BascoExecutor<TestTrigger> testee;

        public BascoExecutorTest()
        {
            this.simpleTestState = new SimpleTestState();
            this.extendedTestState = new ExtendedTestState();
            this.testee = new BascoExecutor<TestTrigger>(new List<IState> { this.simpleTestState, this.extendedTestState });
        }

        [Fact]
        public void Ctor_WhenCreateWithNoStates_MustThrow()
        {
            this.Invoking(x => new BascoExecutor<TestTrigger>(new List<IState>()))
                .ShouldThrow<BascoException>();        
        }

        [Fact]
        public void Start_WhenNoInitialStateFound_MustReturnFalse()
        {
            bool result = this.testee.Start<FakeState>();

            result.Should().BeFalse();
        }

        [Fact]
        public void Start_WhenInitialStateFound_MustReturnTrue()
        {
            bool result = this.testee.Start<ExtendedTestState>();

            result.Should().BeTrue();
        }

        [Fact]
        public void Start_MustSetInitialState()
        {
            this.testee.Start<ExtendedTestState>();

            this.testee.CurrentState.Should().Be(this.extendedTestState);
        }

        [Fact]
        public void Start_WhenEnterableInitialState_MustEnterInitialState()
        {
            bool called = false;
            this.extendedTestState.OnEnter = () => { called = true; };

            this.testee.Start<ExtendedTestState>();

            called.Should().BeTrue();
        }

        [Fact]
        public void Start_WhenNotEnterableInitialState_MustNotEnterInitialState()
        {
            bool called = false;
            this.simpleTestState.OnEnter = () => { called = true; };

            this.testee.Start<SimpleTestState>();

            called.Should().BeFalse();
        }

        [Fact]
        public void Start_MustPerformExecuteOnInitialState()
        {
            bool called = false;
            this.extendedTestState.OnExecution = () => { called = true; };

            this.testee.Start<ExtendedTestState>();

            called.Should().BeTrue();
        }

        [Fact]
        public void Start_MustRaiseStateChanged()
        {
            bool called = false;
            this.testee.StateChanged += (sender, args) => { called = true; };

            this.testee.Start<ExtendedTestState>();

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
            this.testee.Start<ExtendedTestState>();
            this.extendedTestState.OnExit = () => { called = true; };

            this.testee.Stop();

            called.Should().BeTrue();
        }

        [Fact]
        public void Stop_WhenNotExitableState_MustNotExitState()
        {
            bool called = false;
            this.testee.Start<SimpleTestState>();
            this.simpleTestState.OnExit = () => { called = true; };

            this.testee.Stop();

            called.Should().BeFalse();
        }

        [Fact]
        public void Stop_MustRaiseStateChanged()
        {
            bool called = false;
            this.testee.Start<ExtendedTestState>();
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
            this.testee.Start<ExtendedTestState>();

            this.Invoking(x => this.testee.ChangeState(TestTrigger.TransitionOne))
                .ShouldNotThrow();
        }

        [Fact]
        public void ChangeState_WhenExitableState_MustExitInitialState()
        {
            bool called = false;
            this.extendedTestState.OnExit = () => { called = true; };
            this.SetupExtendedStateTransitions();
            this.testee.Start<ExtendedTestState>();

            this.testee.ChangeState(TestTrigger.TransitionOne);

            called.Should().BeTrue();
        }

        [Fact]
        public void ChangeState_MustSetNextState()
        {
            this.SetupExtendedStateTransitions();
            this.testee.Start<ExtendedTestState>();

            this.testee.ChangeState(TestTrigger.TransitionOne);

            IState result = this.testee.CurrentState;
            result.Should().Be(this.simpleTestState);
        }

        [Fact]
        public void ChangeState_WhenNextStateIsEnterable_MustEnterNextState()
        {
            bool called = false;
            this.SetupSimpleStateTransitions();
            this.extendedTestState.OnEnter = () => { called = true; };
            this.testee.Start<SimpleTestState>();

            this.testee.ChangeState(TestTrigger.TransitionOne);

            called.Should().BeTrue();
        }

        [Fact]
        public void ChangeState_MustExecuteNextState()
        {
            bool called = false;
            this.SetupExtendedStateTransitions();
            this.simpleTestState.OnExecution = () => { called = true; };
            this.testee.Start<ExtendedTestState>();
            
            this.testee.ChangeState(TestTrigger.TransitionOne);

            called.Should().BeTrue();
        }

        [Fact]
        public void ChangeState_MustRaiseStateChanged()
        {
            bool called = false;
            this.SetupExtendedStateTransitions();
            this.testee.Start<ExtendedTestState>();
            this.testee.StateChanged += (sender, args) => { called = true; };
            
            this.testee.ChangeState(TestTrigger.TransitionOne);
            
            called.Should().BeTrue();
        }

        [Fact]
        public void RetrieveState_WhenStateExists_MustReturnState()
        {
            IState result = this.testee.RetrieveState<SimpleTestState>();

            result.Should().Be(this.simpleTestState);
        }

        [Fact]
        public void RetrieveState_WhenStateNotExists_MustReturnNull()
        {
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
    }
}