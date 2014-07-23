namespace Basco.Configuration
{
    using System;
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// A concrete implementation of this interface is needed to properly configure the Basco state machine.
    /// </summary>
    /// <typeparam name="TTransitionTrigger">Enum defining state machine transitions.</typeparam>
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "Basco is the component name")]
    public interface IBascoConfigurator<TTransitionTrigger>
        where TTransitionTrigger : IComparable
    {
        void Configurate(IBasco<TTransitionTrigger> basco);
    }
}