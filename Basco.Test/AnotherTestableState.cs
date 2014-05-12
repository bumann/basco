namespace Basco.Test
{
    internal class AnotherTestableState : IState<TestTrigger>
    {
        public ITransitionPool<TestTrigger> Transitions { get; set; }

        public string Name { get; set; }

        public void Execute()
        {
        }
    }
}