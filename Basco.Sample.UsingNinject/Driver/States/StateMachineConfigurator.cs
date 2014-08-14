namespace Basco.Sample.UsingNinject.Driver.States
{
    using Basco.Configuration;

    public class StateMachineConfigurator : IBascoConfigurator<TransitionTrigger>
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