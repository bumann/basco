namespace Basco.Test
{
    using System;

    internal class TestableState : IState<TestTrigger>, IStateEnter, IStateExit
    {
        public TestableState()
        {
            this.Transitions = new TransitionPool<TestTrigger>();
            this.OnEnter = () => { };
            this.OnExecution = () => { };
            this.OnExit = () => { };
        }

        public string Name { get; set; }

        public ITransitionPool<TestTrigger> Transitions { get; set; }

        public Action OnEnter { get; set; }

        public Action OnExit { get; set; }

        public Action OnExecution { get; set; }

        public void Execute()
        {
            this.OnExecution();
        }

        public void Enter()
        {
            this.OnEnter();
        }

        public void Exit()
        {
            this.OnExit();
        }
    }
}