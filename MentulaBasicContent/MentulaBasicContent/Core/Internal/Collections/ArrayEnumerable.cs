namespace Mentula.Content.Core.Collections
{
    using System.Collections;
    using System.Collections.Generic;

#if !DEBUG
    [System.Diagnostics.DebuggerStepThrough]
#endif
    internal abstract class ArrayEnumerable<T> : IEnumerable<T>
    {
        public abstract ArrayEnumerator<T> GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}