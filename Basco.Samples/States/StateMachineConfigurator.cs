namespace Basco.Samples.States
{
    public class StateMachineConfigurator : IBascoConfigurator<TransitionTrigger>
    {
        public void Configurate(IBasco<TransitionTrigger> basco)
        {
            basco.In<ConnectedState, TransitionTrigger>()
                .On(TransitionTrigger.Run).Goto<ProcessingState, TransitionTrigger>()
                .On(TransitionTrigger.Stop).ReEnter()
                .On(TransitionTrigger.Error).Stay()
                .On(TransitionTrigger.Reset).Stay();

            basco.In<ProcessingState, TransitionTrigger>()
                .On(TransitionTrigger.Stop).Goto<ConnectedState, TransitionTrigger>()
                .On(TransitionTrigger.Error).Goto<ErrorState, TransitionTrigger>()
                .On(TransitionTrigger.Reset).Stay();

            basco.In<ErrorState, TransitionTrigger>()
                .On(TransitionTrigger.Reset).Goto<ConnectedState, TransitionTrigger>();
        }
    }
}