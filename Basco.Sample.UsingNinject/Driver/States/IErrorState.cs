namespace Basco.Sample.UsingNinject.Driver.States
{
    using Basco;

    /// <summary>
    /// ErrorState: state with code to execute on state enter and exit
    /// </summary>
    public interface IErrorState : IState, IStateEnter, IStateExit
    {
    }
}