namespace Mentula.Content.Core.Collections
{
    using System;

#if !DEBUG
    [System.Diagnostics.DebuggerStepThrough]
#endif
    internal static class CollectionExtensions
    {
        public static TSource[] Add<TSource>(this TSource[] source, TSource item)
        {
            CheckSource(source);

            int index = source.Length;
            Array.Resize(ref source, index + 1);

            source[index] = item;
            return source;
        }

        public static TSource[] Concat<TSource>(this TSource[] source, TSource[] collection)
        {
            CheckSource(source);
            if (collection == null) throw new ArgumentNullException("collection", "The collection to concat cannot be null!");

            TSource[] result = new TSource[source.Length + collection.Length];
            source.CopyTo(result, 0);
            collection.CopyTo(result, source.Length);
            return result;
        }

        public static bool ContainsAmount(this string source, char value, uint amount)
        {
            return ContainsAmount(source, c => c == value, amount);
        }

        public static bool ContainsAmount(this string source, Predicate<char> predicate, uint amount)
        {
            CheckSource(source);

            for (int i = 0, j = 0; i < source.Length; i++)
            {
                if (predicate(source[i]))
                {
                    ++j;
                    if (j >= amount) return true;
                }
            }

            return false;
        }

        private static void CheckSource<TSource>(TSource source)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
        }
    }
}
