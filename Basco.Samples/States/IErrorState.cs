namespace Basco.Samples.States
{
    /// <summary>
    /// ErrorState: state with code to execute on state enter and exit
    /// </summary>
    public interface IErrorState : IState, IStateEnter, IStateExit
    {
    }
}