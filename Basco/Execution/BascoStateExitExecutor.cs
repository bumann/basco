namespace Basco.Execution
{
    using System;

    public class BascoStateExitExecutor : IBascoStateExitExecutor
    {
        public event EventHandler StateExiting;

        public void Exit(IState state)
        {
            var exitableState = state as IStateExit;
            if (exitableState == null)
            {
                return;
            }

            this.OnStateExiting();

            exitableState.Exit();
        }

        protected void OnStateExiting()
        {
            if (this.StateExiting != null)
            {
                this.StateExiting(this, new EventArgs());
            }
        }
    }
}