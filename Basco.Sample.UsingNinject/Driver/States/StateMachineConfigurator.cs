namespace Basco.Sample.UsingNinject.Driver.States
{
    using Basco.Configuration;

    public class StateMachineConfigurator : IBascoConfigurator<TransitionTrigger>
    {
        public void Configurate(IBasco<TransitionTrigger> basco)
        {
            basco.In<ConnectedState, TransitionTrigger>(x => x
                .When(TransitionTrigger.Run).Goto<ProcessingState>());

            basco.In<ProcessingState, TransitionTrigger>(x => x
                .When(TransitionTrigger.Stop).Goto<ConnectedState>()
                .When(TransitionTrigger.Error).Goto<ErrorState>());

            basco.In<ErrorState, TransitionTrigger>(x => x
                .When(TransitionTrigger.Reset).Goto<ConnectedState>());
        }
    }
}