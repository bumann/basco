namespace Basco.Sample.Basic.Fsm
{
    using Basco.Configuration;
    using Basco.Sample.Basic.Fsm.States;

    public class BascoConfigurator : IBascoConfigurator<TransitionTrigger>
    {
        public void Configurate(IBascoInternal<TransitionTrigger> basco)
        {
            basco.For<ConnectedState, TransitionTrigger>(x => x
                .When(TransitionTrigger.Run).Goto<ProcessingState>());

            basco.For<ProcessingState, TransitionTrigger>(x => x
                .When(TransitionTrigger.Stop).Goto<ConnectedState>()
                .When(TransitionTrigger.Error).Goto<ErrorState>());
              
            basco.For<ErrorState, TransitionTrigger>(x => x
                .When(TransitionTrigger.Reset).Goto<ConnectedState>());
        }
    }
}