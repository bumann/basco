namespace Basco.Samples.States
{
    public class ErrorState : IErrorState
    {
        public ITransitionPool<Transitions> Transitions { get; set; }

        public void Execute()
        {
        }

        public void Enter()
        {
        }

        public void Exit()
        {
        }
    }
}