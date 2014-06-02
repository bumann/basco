namespace Basco.Test
{
    using FluentAssertions;
    using Xunit;

    public class StateExtensionsTest
    {
        [Fact]
        public void On_MustReturnTransitionsBuilder()
        {
            IStateTransitionsBuilder<TestTrigger> result = (new TestableState()).On(TestTrigger.TransitionOne);

            result.Should().BeOfType<StateTransitionsBuilder<TestTrigger>>();
        } 
    }
}