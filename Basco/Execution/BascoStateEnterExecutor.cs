namespace Basco.Execution
{
    using System;

    public class BascoStateEnterExecutor : IBascoStateEnterExecutor
    {
        public event EventHandler StateEntered;

        public void Enter(IState state)
        {
            var enterableState = state as IStateEnter;
            if (enterableState == null)
            {
                return;
            }

            enterableState.Enter();

            this.OnStateEntered();
        }

        protected void OnStateEntered()
        {
            if (this.StateEntered != null)
            {
                this.StateEntered(this, new EventArgs());
            }
        }
    }
}