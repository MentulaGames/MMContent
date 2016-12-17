namespace Mentula.Content.Core.Collections
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

#if !DEBUG
    [System.Diagnostics.DebuggerStepThrough]
#endif
    internal class ArrayEnumerator<T> : IEnumerator<T>
    {
        public T Current { get { return array[index]; } }
        object IEnumerator.Current { get { return Current; } }
        public bool Disposed { get; private set; }

        private T[] array;
        private int index;

        public ArrayEnumerator(T[] source)
        {
            array = source;
            Reset();
        }

        public void Dispose()
        {
            if (!Disposed)
            {
                Reset();
                array = null;
                Disposed = true;
            }
        }

        public bool MovePrevious()
        {
            if (Disposed) throw new ObjectDisposedException("ArrayEnumerator");
            return --index >= 0;
        }

        public bool MoveNext()
        {
            if (Disposed) throw new ObjectDisposedException("ArrayEnumerator");
            return ++index < array.Length;
        }

        public void Reset()
        {
            if (Disposed) throw new ObjectDisposedException("ArrayEnumerator");
            index = -1;
        }
    }

    internal static class ArrayEnumerator
    {
        public static ArrayEnumerator<char> FromString(string source)
        {
            return new ArrayEnumerator<char>(source.ToCharArray());
        }
    }
}