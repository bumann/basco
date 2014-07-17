namespace Basco.Execution
{
    using System;

    public interface IBascoStateEnterExecutor
    {
        event EventHandler StateEntered;

        void Enter(IState state);
    }
}