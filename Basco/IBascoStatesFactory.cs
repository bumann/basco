namespace Basco
{
    using System.Collections.Generic;

    public interface IBascoStatesFactory
    {
        IEnumerable<IState> CreateStates();
    }
}