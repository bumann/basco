namespace Basco.Test
{
    using System;
    using System.Collections.Generic;
    using Basco.Configuration;
    using Basco.Execution;
    using Basco.Test.Utilities;
    using FluentAssertions;
    using Moq;
    using Scyano;
    using Xunit;

    public class BascoTest
    {
        private readonly Mock<IScyano> scyano;
        private readonly Mock<IBascoStateCache<TestTrigger>> stateCache;
        private readonly Mock<IBascoExecutor<TestTrigger>> bascoExecutor;
        private readonly Mock<IBascoConfigurator<TestTrigger>> bascoConfigurator;
        private readonly Basco<TestTrigger> testee;

        public BascoTest()
        {
            this.scyano = new Mock<IScyano>();
            this.stateCache = new Mock<IBascoStateCache<TestTrigger>> { DefaultValue = DefaultValue.Mock };
            this.bascoExecutor = new Mock<IBascoExecutor<TestTrigger>>();
            this.bascoConfigurator = new Mock<IBascoConfigurator<TestTrigger>>();
            this.testee = new Basco<TestTrigger>(
                this.scyano.Object,
                this.stateCache.Object,
                new Mock<IBascoTransitionCache<TestTrigger>>().Object,
                this.bascoExecutor.Object,
                this.bascoConfigurator.Object);
        }

        [Fact]
        public void Initialize_MustInitializeStateCache()
        {
            var states = Mock.Of<IEnumerable<IState>>();

            this.testee.Initialize(states, typeof(IState));

            this.stateCache.Verify(x => x.Initialize(this.testee, states));
        }

        [Fact]
        public void Initialize_MustInitializeBascoExecutor()
        {
            this.testee.Initialize(Mock.Of<IEnumerable<IState>>(), typeof(IState));

            this.bascoExecutor.Verify(x => x.Initialize(typeof(IState)));
        }

        [Fact]
        public void Initialize_MustInitializeModuleController()
        {
            this.testee.Initialize(Mock.Of<IEnumerable<IState>>(), typeof(IState));

            this.scyano.Verify(x => x.Initialize(this.testee));
        }

        [Fact]
        public void Initialize_MustConfigurateTransitions()
        {
            this.testee.Initialize(Mock.Of<IEnumerable<IState>>(), typeof(IState));

            this.bascoConfigurator.Verify(x => x.Configurate(this.testee));
        }

        [Fact]
        public void Initialize_MustSetInitializedToTrue()
        {
            this.testee.Initialize(Mock.Of<IEnumerable<IState>>(), typeof(IState));

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
            this.testee.Initialize(Mock.Of<IEnumerable<IState>>(), typeof(IState));

            this.testee.Start();

            this.testee.Invoking(x => x.Start())
                .ShouldThrow<BascoException>();
        }

        [Fact]
        public void Start_MustSetIsRunningToTrue()
        {
            this.testee.Initialize(Mock.Of<IEnumerable<IState>>(), typeof(IState));

            this.testee.Start();

            this.testee.IsRunning.Should().BeTrue();
        }

        [Fact]
        public void Start_MustStartScyano()
        {
            this.testee.Initialize(Mock.Of<IEnumerable<IState>>(), typeof(IState));

            this.testee.Start();

            this.scyano.Verify(x => x.Start(), Times.Once);
        }

        [Fact]
        public void Start_WhenStoppedBeforeRestart_MustNotThrow()
        {
            this.testee.Initialize(Mock.Of<IEnumerable<IState>>(), typeof(IState));
            this.testee.Start();
            this.testee.Stop();

            this.testee.Invoking(x => x.Start())
                .ShouldNotThrow<BascoException>();
        }

        [Fact]
        public void Start_WhenExceptionOccurs_MustStopScyano()
        {
            this.testee.Initialize(Mock.Of<IEnumerable<IState>>(), typeof(IState));
            this.scyano.Setup(x => x.Start())
                .Throws<Exception>();

            this.testee.Invoking(x => x.Start())
                .ShouldThrow<Exception>();

            this.scyano.Verify(x => x.Start(), Times.Once);
        }

        [Fact]
        public void Start_WhenExceptionOccurs_MustSetIsRunningToFalse()
        {
            this.testee.Initialize(Mock.Of<IEnumerable<IState>>(), typeof(IState));
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
            this.testee.Initialize(Mock.Of<IEnumerable<IState>>(), typeof(IState));
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
        public void TriggerConsumer_MustTriggerSubBascos()
        {
            const TestTrigger ExpectedTrigger = TestTrigger.TransitionOne;
            var subBasco1 = new Mock<IBascoInternal<TestTrigger>> { DefaultValue = DefaultValue.Mock };
            var subBasco2 = new Mock<IBascoInternal<TestTrigger>> { DefaultValue = DefaultValue.Mock };
            var executor1 = new Mock<IBascoExecutor<TestTrigger>>();
            var executor2 = new Mock<IBascoExecutor<TestTrigger>>();
            subBasco1.Setup(x => x.BascoExecutor).Returns(executor1.Object);
            subBasco2.Setup(x => x.BascoExecutor).Returns(executor2.Object);
            this.testee.AddHierchary(subBasco1.Object);
            this.testee.AddHierchary(subBasco2.Object);

            this.testee.TriggerConsumer(ExpectedTrigger);

            executor1.Verify(x => x.ChangeState(ExpectedTrigger));
            executor2.Verify(x => x.ChangeState(ExpectedTrigger));
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
            this.testee.Initialize(Mock.Of<IEnumerable<IState>>(), typeof(IState));
            this.testee.StateChanged += (sender, args) => { called = true; };

            this.bascoExecutor.Raise(x => x.StateChanged += null, new EventArgs());

            called.Should().BeTrue();
        }

        [Fact]
        public void AddHierchary_MustAddEventHandlerForSubBasco()
        {
            var basco = new Mock<IBascoInternal<TestTrigger>>();
            this.testee.AddHierchary(basco.Object);
            this.testee.MonitorEvents();

            basco.Raise(x => x.StateChanged += null, new EventArgs());

            this.testee.ShouldRaise<IBascoInternal<TestTrigger>>(x => x.StateChanged += null);
        }
    }
}