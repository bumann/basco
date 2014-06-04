namespace Basco.Test
{
    using System;

    public class SimpleTestState : IState
    {
        public SimpleTestState()
        {
            this.OnEnter = () => { };
            this.OnExecution = () => { };
            this.OnExit = () => { };
        }

        public string Name { get; set; }

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