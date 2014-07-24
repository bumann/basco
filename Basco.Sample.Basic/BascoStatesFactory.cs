namespace Basco.Sample.Basic
{
    using System.Collections.Generic;
    using Basco.Sample.Basic.Fsm.States;

    public class BascoStatesFactory : IBascoStatesFactory<TransitionTrigger>
    {
        public IEnumerable<IState> CreateStates(IBasco<TransitionTrigger> basco)
        {
            return new List<IState> { new ConnectedState(), new ProcessingState(basco), new ErrorState() };
        }
    }
}