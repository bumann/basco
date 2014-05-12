namespace Basco.Test
{
    using Appccelerate.AsyncModule;
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
        public void Start_MustInitializeModuleController()
        {
            var expectedState = new TestableState();

            this.testee.Start(expectedState);

            this.moduleController.Verify(x => x.Initialize(this.testee, 1, false, "Basco"));
        }

        [Fact]
        public void Start_MustStartModuleController()
        {
            var expectedState = new TestableState();

            this.testee.Start(expectedState);

            this.moduleController.Verify(x => x.Start());
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