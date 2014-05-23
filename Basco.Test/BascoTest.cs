namespace Basco.Test
{
    using Appccelerate.AsyncModule;
    using FluentAssertions;
    using Moq;
    using Xunit;

    public class BascoTest
    {
        private readonly Mock<IBascoConfigurator<TestTrigger>> bascoConfigurator;
        private readonly Mock<IBascoExecutor<TestTrigger>> bascoExecutor;
        private readonly Mock<IModuleController> moduleController;
        private readonly Basco<TestTrigger> testee;

        public BascoTest()
        {
            this.bascoConfigurator = new Mock<IBascoConfigurator<TestTrigger>>();
            this.bascoExecutor = new Mock<IBascoExecutor<TestTrigger>>();
            this.moduleController = new Mock<IModuleController>();
            this.testee = new Basco<TestTrigger>(
                this.moduleController.Object,
                this.bascoConfigurator.Object,
                this.bascoExecutor.Object);
        }

        [Fact]
        public void Ctor_MustInitializeModuleController()
        {
            this.moduleController.Verify(x => x.Initialize(this.testee, 1, false, "Basco"));
        }

        [Fact]
        public void Start_WhenNoStartStateFound_MustNotStartExecutorWithInitialState()
        {
            this.testee.Start<TestableState>();

            this.bascoExecutor.Verify(x => x.Start(It.IsAny<IState<TestTrigger>>()), Times.Never);
        }

        [Fact]
        public void Start_MustConfigurateTransitions()
        {
            this.testee.Start<TestableState>();

            this.bascoConfigurator.Verify(x => x.Configurate());
        }

        [Fact]
        public void Start_MustNotInitializeModuleController()
        {
            this.testee.Start<TestableState>();

            this.moduleController.Verify(x => x.Initialize(this.testee, 1, false, "Basco"), Times.Once);
        }

        [Fact]
        public void Start_WhenNoStartStateFound_MustNotStartModuleController()
        {
            this.testee.Start<TestableState>();

            this.moduleController.Verify(x => x.Start(), Times.Never);
        }

        [Fact]
        public void Start_MustSetIsRunning()
        {
            this.testee.Start<TestableState>();

            this.testee.IsRunning.Should().BeTrue();
        }

        [Fact]
        public void Start_WhenStartedTwice_MustThrowBascoException()
        {
            this.testee.Start<TestableState>();

            this.testee.Invoking(x => x.Start<TestableState>())
                .ShouldThrow<BascoException>();
        }

        [Fact]
        public void Start_WhenStoppedBeforeRestart_MustNotThrowBascoException()
        {
            this.testee.Start<TestableState>();
            this.testee.Stop();

            this.testee.Invoking(x => x.Start<TestableState>())
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

            this.moduleController.Verify(x => x.Stop());
        }

        [Fact]
        public void Stop_MustResetIsRunning()
        {
            this.testee.Start<TestableState>();

            this.testee.Stop();

            this.testee.IsRunning.Should().BeFalse();
        }

        [Fact]
        public void Trigger_MustEnqueueMessage()
        {
            const TestTrigger ExpectedTrigger = TestTrigger.TransitionOne;

            this.testee.Trigger(ExpectedTrigger);

            this.moduleController.Verify(x => x.EnqueueMessage(ExpectedTrigger));
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