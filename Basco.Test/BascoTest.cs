namespace Basco.Test
{
    using System;
    using Basco.Configuration;
    using Basco.Execution;
    using FluentAssertions;
    using Moq;
    using Scyano;
    using Xunit;

    public class BascoTest
    {
        private readonly Mock<IBascoConfigurator<TestTrigger>> bascoConfigurator;
        private readonly Mock<IBascoExecutor<TestTrigger>> bascoExecutor;
        private readonly Mock<IScyano> scyano;
        private readonly Basco<TestTrigger> testee;

        public BascoTest()
        {
            this.bascoConfigurator = new Mock<IBascoConfigurator<TestTrigger>>();
            this.bascoExecutor = new Mock<IBascoExecutor<TestTrigger>>();
            this.scyano = new Mock<IScyano>();
            this.testee = new Basco<TestTrigger>(
                this.scyano.Object,
                new Mock<IBascoTransitionCache<TestTrigger>>().Object,
                this.bascoConfigurator.Object,
                this.bascoExecutor.Object);
        }

        [Fact]
        public void InitializeWithStartState_MustInitializeBascoExecutor()
        {
            this.testee.InitializeWithStartState<SimpleTestState>();

            this.bascoExecutor.Verify(x => x.InitializeWithStartState<SimpleTestState>(this.testee));
        }

        [Fact]
        public void InitializeWithStartState_MustInitializeModuleController()
        {
            this.testee.InitializeWithStartState<SimpleTestState>();

            this.scyano.Verify(x => x.Initialize(this.testee));
        }

        [Fact]
        public void InitializeWithStartState_MustConfigurateTransitions()
        {
            this.testee.InitializeWithStartState<SimpleTestState>();

            this.bascoConfigurator.Verify(x => x.Configurate(this.testee));
        }

        [Fact]
        public void InitializeWithStartState_MustSetInitializedToTrue()
        {
            this.testee.InitializeWithStartState<SimpleTestState>();

            this.testee.IsInitialized.Should().BeTrue();
        }

        [Fact]
        public void CurrentState_MustReturnExecutorCurrentState()
        {
            var expectedState = Mock.Of<IState>();
            this.bascoExecutor.Setup(x => x.CurrentState)
                .Returns(expectedState);

            IState result = this.testee.CurrentState;

            result.Should().Be(expectedState);
        }

        [Fact]
        public void RetrieveState_MustReturnExecutorState()
        {
            var expectedState = new SimpleTestState();
            this.bascoExecutor.Setup(x => x.RetrieveState<SimpleTestState>())
                .Returns(expectedState);

            IState result = this.testee.RetrieveState<SimpleTestState>();

            result.Should().Be(expectedState);
        }

        [Fact]
        public void Start_WhenNotInitialized_MustThrow()
        {
            this.testee.Invoking(x => x.Start())
                .ShouldThrow<BascoException>();
        }

        [Fact]
        public void Start_WhenStartedTwice_MustThrow()
        {
            this.testee.InitializeWithStartState<ExtendedTestState>();

            this.testee.Start();

            this.testee.Invoking(x => x.Start())
                .ShouldThrow<BascoException>();
        }

        [Fact]
        public void Start_MustSetIsRunningToTrue()
        {
            this.testee.InitializeWithStartState<ExtendedTestState>();

            this.testee.Start();

            this.testee.IsRunning.Should().BeTrue();
        }

        [Fact]
        public void Start_MustStartScyano()
        {
            this.testee.InitializeWithStartState<ExtendedTestState>();

            this.testee.Start();

            this.scyano.Verify(x => x.Start(), Times.Once);
        }

        [Fact]
        public void Start_WhenStoppedBeforeRestart_MustNotThrow()
        {
            this.testee.InitializeWithStartState<ExtendedTestState>();
            this.testee.Start();
            this.testee.Stop();

            this.testee.Invoking(x => x.Start())
                .ShouldNotThrow<BascoException>();
        }

        [Fact]
        public void Start_WhenExceptionOccurs_MustStopScyano()
        {
            this.testee.InitializeWithStartState<ExtendedTestState>();
            this.scyano.Setup(x => x.Start())
                .Throws<Exception>();

            this.testee.Invoking(x => x.Start())
                .ShouldThrow<Exception>();

            this.scyano.Verify(x => x.Start(), Times.Once);
        }

        [Fact]
        public void Start_WhenExceptionOccurs_MustSetIsRunningToFalse()
        {
            this.testee.InitializeWithStartState<ExtendedTestState>();
            this.scyano.Setup(x => x.Start())
                .Throws<Exception>();

            this.testee.Invoking(x => x.Start())
                .ShouldThrow<Exception>();

            this.testee.IsRunning.Should().BeFalse();
        }

        [Fact]
        public void Stop_MustStopExecutor()
        {
            this.testee.Stop();

            this.bascoExecutor.Verify(x => x.Stop());
        }

        [Fact]
        public void Stop_MustStopModuleController()
        {
            this.testee.Stop();

            this.scyano.Verify(x => x.Stop());
        }

        [Fact]
        public void Stop_MustResetIsRunning()
        {
            this.testee.InitializeWithStartState<ExtendedTestState>();
            this.testee.Start();

            this.testee.Stop();

            this.testee.IsRunning.Should().BeFalse();
        }

        [Fact]
        public void Trigger_MustEnqueue()
        {
            const TestTrigger ExpectedTrigger = TestTrigger.TransitionOne;

            this.testee.Trigger(ExpectedTrigger);

            this.scyano.Verify(x => x.Enqueue(ExpectedTrigger));
        }

        [Fact]
        public void TriggerConsumer_MustChangeState()
        {
            const TestTrigger ExpectedTrigger = TestTrigger.TransitionOne;

            this.testee.TriggerConsumer(ExpectedTrigger);

            this.bascoExecutor.Verify(x => x.ChangeState(ExpectedTrigger));
        }

        [Fact]
        public void BascoExecutorStateChanged_WhenInitialized_MustRaiseStateChanged()
        {
            var called = false;
            this.testee.InitializeWithStartState<SimpleTestState>();
            this.testee.StateChanged += (sender, args) => { called = true; };

            this.bascoExecutor.Raise(x => x.StateChanged += null, new EventArgs());

            called.Should().BeTrue();
        }
    }
}