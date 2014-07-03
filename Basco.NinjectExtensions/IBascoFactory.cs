namespace Basco.NinjectExtensions
{
    using System.Collections.Generic;

    public interface IBascoFactory
    {
        IEnumerable<T> CreateAll<T>();
    }
}