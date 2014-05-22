namespace Basco.Samples.States
{
    public class ConnectedState : IConnectedState
    {
        public ITransitionPool<Transitions> Transitions { get; set; }

        public void Execute()
        {
        }

        public void Enter()
        {
        }
    }
}