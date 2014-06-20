namespace Basco.Test
{
    using Basco.Async;
    using FluentAssertions;
    using Moq;
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
                this.bascoConfigurator.Object,
                this.bascoExecutor.Object);
        }

        [Fact]
        public void Ctor_MustInitializeModuleController()
        {
            this.scyano.Verify(x => x.Initialize(this.testee));
        }

        [Fact]
        public void Ctor_MustConfigurateTransitions()
        {
            this.bascoConfigurator.Verify(x => x.Configurate(this.testee));
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
        public void Start_MustNotInitializeModuleController()
        {
            this.testee.Start<ExtendedTestState>();

            this.scyano.Verify(x => x.Initialize(this.testee), Times.Once);
        }

        [Fact]
        public void Start_WhenExecutorNotStarted_MustNotStartModuleController()
        {
            this.bascoExecutor.Setup(x => x.Start<ExtendedTestState>())
                .Returns(false);

            this.testee.Start<ExtendedTestState>();

            this.scyano.Verify(x => x.Start(), Times.Never);
        }

        [Fact]
        public void Start_WhenExecutorStarted_MustStartModuleController()
        {
            this.bascoExecutor.Setup(x => x.Start<ExtendedTestState>())
                .Returns(true);

            this.testee.Start<ExtendedTestState>();

            this.scyano.Verify(x => x.Start(), Times.Once);
        }

        [Fact]
        public void Start_WhenExecutorStarts_MustSetIsRunningToTrue()
        {
            this.bascoExecutor.Setup(x => x.Start<ExtendedTestState>())
                .Returns(true);

            this.testee.Start<ExtendedTestState>();

            this.testee.IsRunning.Should().BeTrue();
        }

        [Fact]
        public void Start_WhenExecutorDoesNotStart_MustSetIsRunningToFalse()
        {
            this.bascoExecutor.Setup(x => x.Start<ExtendedTestState>())
                .Returns(false);

            this.testee.Start<ExtendedTestState>();

            this.testee.IsRunning.Should().BeFalse();
        }

        [Fact]
        public void Start_WhenStartedTwice_MustThrow()
        {
            this.bascoExecutor.Setup(x => x.Start<ExtendedTestState>())
                .Returns(true);

            this.testee.Start<ExtendedTestState>();

            this.testee.Invoking(x => x.Start<ExtendedTestState>())
                .ShouldThrow<BascoException>();
        }

        [Fact]
        public void Start_WhenStoppedBeforeRestart_MustNotThrow()
        {
            this.testee.Start<ExtendedTestState>();
            this.testee.Stop();

            this.testee.Invoking(x => x.Start<ExtendedTestState>())
                .ShouldNotThrow<BascoException>();
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
            this.testee.Start<ExtendedTestState>();

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
    }
}