﻿namespace Basco.Test
{
    using FluentAssertions;
    using Moq;
    using Scyano;
    using Xunit;

    public class BascoExtensionsTest
    {
        [Fact]
        public void In_MustReturnStateTransitionsBuilder()
        {
            var basco = new Basco<TestTrigger>(Mock.Of<IScyano>(), Mock.Of<IBascoConfigurator<TestTrigger>>(), Mock.Of<IBascoExecutor<TestTrigger>>());

            IStateTransitionsBuilder<TestTrigger> result = basco.In<SimpleTestState, TestTrigger>();

            result.Should().BeOfType<StateTransitionsBuilder<TestTrigger>>();
        }

        [Fact]
        public void In_MustAddStateTransitionToExecutor()
        {
            var executor = new Mock<IBascoExecutor<TestTrigger>>();
            var basco = new Basco<TestTrigger>(Mock.Of<IScyano>(), Mock.Of<IBascoConfigurator<TestTrigger>>(), executor.Object);

            basco.In<SimpleTestState, TestTrigger>();

            executor.Verify(x => x.AddStateTransitions<SimpleTestState>(It.IsAny<StateTransitions<TestTrigger>>()));
        }
    }
}