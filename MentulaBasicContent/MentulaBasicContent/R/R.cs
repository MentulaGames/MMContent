namespace Mentula.Content.R
{
    using System.Collections.Generic;
    using System.Diagnostics;

    /// <summary>
    /// Represents a Resource config file.
    /// </summary>
    /// <remarks>
    /// The key of the underlying <see cref="Dictionary{TKey, TValue}"/> is the Int32 ID
    /// and the value is the file name.
    /// </remarks>
#if !DEBUG
    [DebuggerStepThrough]
#endif
    [DebuggerDisplay("{ToString()}")]
    public sealed class R : Dictionary<int, string>
    {
        internal R(KeyValuePair<string, KeyValuePair<int, string>[]>[] raw)
        {
            for (int i = 0; i < raw.Length; i++)
            {
                KeyValuePair<string, KeyValuePair<int, string>[]> iA = raw[i];

                for (int j = 0; j < iA.Value.Length; j++)
                {
                    KeyValuePair<int, string> item = iA.Value[j];

                    if (ContainsKey(item.Key))
                    {
                        throw new ContainerException(iA.Key,  new BuildException($"{item.Key} has already been added!"));
                    }

                    Add(item.Key, $"{iA.Key}/{item.Value}");
                }
            }
        }

        /// <summary>
        /// Gets the ID from the specified name.
        /// </summary>
        /// <param name="name"> The specified tag name. </param>
        /// <returns> The ID of the tag when successfull otherwise; -1. </returns>
        public int GetTagId(string name)
        {
            foreach (KeyValuePair<int, string> cur in this)
            {
                if (cur.Value == name) return cur.Key;
            }

            return -1;
        }
    }
}