namespace Mentula.Content.R
{
    using Microsoft.Xna.Framework.Content;
    using System;
    using System.Collections.Generic;
    using System.IO;

#if !DEBUG
    [System.Diagnostics.DebuggerStepThrough]
#endif
    internal sealed class MRReader : ContentTypeReader<R>
    {
        protected override R Read(ContentReader input, R existingInstance)
        {
            try
            {
                int diff = input.ReadInt32();
                KeyValuePair<string, KeyValuePair<int, string>[]>[] raw = new KeyValuePair<string, KeyValuePair<int, string>[]>[diff];

                for (int i = 0; i < diff; i++)
                {
                    int length = input.ReadInt32();
                    string dir = input.ReadString();

                    KeyValuePair<int, string>[] items = new KeyValuePair<int, string>[length];

                    for (int j = 0; j < length; j++)
                    {
                        int id = input.ReadInt32();
                        string name = input.ReadString();
                        items[j] = new KeyValuePair<int, string>(id, name);
                    }

                    raw[i] = new KeyValuePair<string, KeyValuePair<int, string>[]>(dir, items);
                }

                return new R(raw);
            }
            catch (Exception e)
            {
                throw new FileLoadException("The file could not be loaded.", input.AssetName, e);
            }
        }
    }
}