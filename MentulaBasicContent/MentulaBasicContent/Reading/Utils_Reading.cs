namespace Mentula.BasicContent.Reading
{
    using Core;
    using System;
    using System.Runtime.CompilerServices;

    /// <summary>
    /// This class contains utility functions for reading a .mm file.
    /// </summary>
#if !DEBUG
    [System.Diagnostics.DebuggerStepThrough]
#endif
    public static partial class Utils
    {
        /// <summary>
        /// Checks whether the right processor is selected for processing the .mm file.
        /// </summary>
        /// <param name="type"> The type of data required. </param>
        /// <param name="source"> The conpiled source .mm file. </param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void CheckProcessorType(string type, MMSource source)
        {
            string request;
            if (!source.TryGetValue("name", out request))
            {
                if (!source.DefaultSet)
                {
                    throw new ArgumentException("No processor type found.")
                        .AsContainerException(source.Name);
                }
                else request = source.DefaultValue;
            }

            if (type.ToUpper() != request.ToUpper())
            {
                throw new ArgumentException($"Wrong processor type selected this= '{type}', needed='{request}'").AsContainerException(source.Name);
            }
        }
    }
}