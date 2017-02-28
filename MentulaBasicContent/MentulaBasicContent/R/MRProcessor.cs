#if MONO
extern alias Mono;
#else
extern alias Xna;
#endif

namespace Mentula.BasicContent.R
{
#if MONO
    using Mono.Microsoft.Xna.Framework.Content.Pipeline;
#else
    using Xna.Microsoft.Xna.Framework.Content.Pipeline;
#endif
    using Core;
    using Reading;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// The content processor used to process the mentula .R file extension
    /// </summary>
#if !DEBUG
    [System.Diagnostics.DebuggerStepThrough]
#endif
    [ContentProcessor(DisplayName = "Mentula R Processor")]
    public sealed class MRProcessor : ContentProcessor<MMSource, R>
    {
        /// <summary>
        /// Creates a new instance of the <see cref="R"/> class from the <see cref="MMSource"/> input.
        /// </summary>
        /// <param name="input"> The specified input. </param>
        /// <param name="context"> The specified context for loading. </param>
        /// <returns> A new instance of the <see cref="R"/> class. </returns>
        public override R Process(MMSource input, ContentProcessorContext context)
        {
            Utils.CheckProcessorType("R", input);

            int length = input.Childs.Length;
            KeyValuePair<string, KeyValuePair<int, string>[]>[] result = new KeyValuePair<string, KeyValuePair<int, string>[]>[length];

            for (int i = 0; i < length; i++)
            {
                Container cur = input.Childs[i];

                try
                {
                    string tagCategory = cur.GetContainerName();
                    KeyValuePair<int, string>[] values = new KeyValuePair<int, string>[cur.Values.Count + cur.Childs.Length];

                    int index = 0;
                    foreach (KeyValuePair<string, string> tag in cur.Values)
                    {
                        values[index++] = new KeyValuePair<int, string>(Utils.ConvertToInt32("Id", tag.Value), tag.Key);
                    }

                    for (int j = 0; j < cur.Childs.Length; j++)
                    {
                        Container tag = cur.Childs[j];
                        values[index++] = new KeyValuePair<int, string>(
                            tag.GetInt32Value("Id"),
                            tag.GetStringValue("Name"));
                    }

                    result[i] = new KeyValuePair<string, KeyValuePair<int, string>[]>(tagCategory, values);
                }
                catch (Exception e)
                {
                    throw new ContainerException(cur.Name, e);
                }
            }

            return new R(result);
        }
    }
}