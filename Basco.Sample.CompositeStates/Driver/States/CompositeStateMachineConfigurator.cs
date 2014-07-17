namespace Basco.Sample.CompositeStates.Driver.States
{
    public class CompositeStateMachineConfigurator : IBascoConfigurator<TransitionTrigger>
    {
        public void Configurate(IBasco<TransitionTrigger> basco)
        {
            basco.In<StateA, TransitionTrigger>()
                //.WithSubState<SubStateD, TransitionTrigger>(state => state
                //    .On(TransitionTrigger.GotoE).Goto<SubStateE, TransitionTrigger>())
                //.WithSubState<SubStateE, TransitionTrigger>(state => state
                //    .On(TransitionTrigger.GotoD).Goto<SubStateD, TransitionTrigger>()))
                .On(TransitionTrigger.Run).Goto<StateB, TransitionTrigger>();

            basco.In<StateB, TransitionTrigger>()
                //.WithSubState<SubStateF, TransitionTrigger>(state => state
                //    .On(TransitionTrigger.GotoG).Goto<SubStateG, TransitionTrigger>())
                //.WithSubState<SubStateG, TransitionTrigger>(state => state
                //    .On(TransitionTrigger.GotoF).Goto<SubStateF, TransitionTrigger>()))
                .On(TransitionTrigger.Stop).Goto<StateA, TransitionTrigger>()
                .On(TransitionTrigger.Error).Goto<StateC, TransitionTrigger>();

            basco.In<StateC, TransitionTrigger>()
               .On(TransitionTrigger.Reset).Goto<StateB, TransitionTrigger>();
        }

        //public void Configurate(IBasco<TransitionTrigger> basco)
        //{
        //    basco.In<SubStateD, TransitionTrigger>()
        //        .On(TransitionTrigger.GotoE).Goto<SubStateE, TransitionTrigger>();

        //    basco.In<SubStateE, TransitionTrigger>()
        //        .On(TransitionTrigger.GotoD).Goto<SubStateD, TransitionTrigger>();
        //}
    }
}