namespace Basco.Sample.Basic.Fsm
{
    using Basco.Configuration;
    using Basco.Sample.Basic.Fsm.States;

    public class BascoConfigurator : IBascoConfigurator<TransitionTrigger>
    {
        public void Configurate(IBascoInternal<TransitionTrigger> basco)
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