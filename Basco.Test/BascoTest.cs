namespace Basco.Test
{
    using Appccelerate.AsyncModule;
    using FluentAssertions;
    using Moq;
    using Xunit;

    public class BascoTest
    {
        private readonly Mock<IBascoConfigurator> bascoConfigurator;
        private readonly Mock<IBascoExecutor<TestTrigger>> bascoExecutor;
        private readonly Mock<IModuleController> moduleController;
        private readonly Basco<TestTrigger> testee;

        public BascoTest()
        {
            this.bascoConfigurator = new Mock<IBascoConfigurator>();
            this.bascoExecutor = new Mock<IBascoExecutor<TestTrigger>>();
            this.moduleController = new Mock<IModuleController>();
            this.testee = new Basco<TestTrigger>(
                this.bascoConfigurator.Object,
                this.bascoExecutor.Object,
                this.moduleController.Object);
        }

        [Fact]
        public void Ctor_MustInitializeModuleController()
        {
            this.moduleController.Verify(x => x.Initialize(this.testee, 1, false, "Basco"));
        }

        [Fact]
        public void Start_MustStartExecutorWithInitialState()
        {
            var expectedState = new TestableState();

            this.testee.Start(expectedState);

            this.bascoExecutor.Verify(x => x.Start(expectedState));
        }

        [Fact]
        public void Start_MustConfigurateTransitions()
        {
            var expectedState = new TestableState();

            this.testee.Start(expectedState);

            this.bascoConfigurator.Verify(x => x.Configurate());
        }

        [Fact]
        public void Start_MustNotInitializeModuleController()
        {
            var expectedState = new TestableState();

            this.testee.Start(expectedState);

            this.moduleController.Verify(x => x.Initialize(this.testee, 1, false, "Basco"), Times.Once);
        }

        [Fact]
        public void Start_MustStartModuleController()
        {
            var expectedState = new TestableState();

            this.testee.Start(expectedState);

            this.moduleController.Verify(x => x.Start());
        }

        [Fact]
        public void Start_MustSetIsRunning()
        {
            var expectedState = new TestableState();

            this.testee.Start(expectedState);

            this.testee.IsRunning.Should().BeTrue();
        }

        [Fact]
        public void Start_WhenStartedTwice_MustThrowBascoException()
        {
            var expectedState = new TestableState();

            this.testee.Start(expectedState);

            this.testee.Invoking(x => x.Start(expectedState))
                .ShouldThrow<BascoException>();
        }

        [Fact]
        public void Start_WhenStoppedBeforeRestart_MustNotThrowBascoException()
        {
            var expectedState = new TestableState();

            this.testee.Start(expectedState);
            this.testee.Stop();

            this.testee.Invoking(x => x.Start(expectedState))
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
            this.testee.Start(new TestableState());

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