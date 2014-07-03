namespace Basco.Sample.Basic
{
    using System.Collections.Generic;
    using Basco.Sample.Basic.Fsm.States;

    public class BascoStatesFactory : IBascoStatesFactory
    {
        public IEnumerable<IState> CreateStates()
        {
            return new List<IState> { new ConnectedState(), new ProcessingState(), new ErrorState() };
        }
    }
}