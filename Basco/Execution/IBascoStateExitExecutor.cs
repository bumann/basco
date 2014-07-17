namespace Basco.Execution
{
    using System;

    public interface IBascoStateExitExecutor
    {
        event EventHandler StateExiting;

        void Exit(IState state);
    }
}