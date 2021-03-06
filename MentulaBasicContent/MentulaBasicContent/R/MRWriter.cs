﻿#if MONO
extern alias Mono;
#else
extern alias Xna;
#endif

namespace Mentula.BasicContent.R
{
#if MONO
    using Mono.Microsoft.Xna.Framework.Content.Pipeline;
    using Mono.Microsoft.Xna.Framework.Content.Pipeline.Serialization.Compiler;
#else
    using Xna.Microsoft.Xna.Framework.Content.Pipeline;
    using Xna.Microsoft.Xna.Framework.Content.Pipeline.Serialization.Compiler;
#endif
    using System.Collections.Generic;
    using System.Linq;

#if !DEBUG
    [System.Diagnostics.DebuggerStepThrough]
#endif
    [ContentTypeWriter]
    internal sealed class MRWriter : ContentTypeWriter<R>
    {
        protected override void Write(ContentWriter output, R value)
        {
            Dictionary<int, string> bases = new Dictionary<int, string>();
            Dictionary<int, KeyValuePair<int, string>> values = new Dictionary<int, KeyValuePair<int, string>>();

            for (int i = 0; i < value.Values.Count; i++)
            {
                KeyValuePair<int, string> val = value.ElementAt(i);
                string[] split = val.Value.Split('/');
                string b = "";

                for (int j = 0; j < split.Length - 1; j++)
                {
                    b += split[j];
                }

                int key;
                if (!bases.ContainsValue(b))
                {
                    key = bases.Count;
                    bases.Add(key, b);
                }
                else
                {
                    key = bases.First(ba => ba.Value == b).Key;
                }

                values.Add(val.Key, new KeyValuePair<int, string>(key, split[split.Length - 1]));
            }

            output.Write(bases.Count);

            for (int i = 0; i < bases.Count; i++)
            {
                KeyValuePair<int, string> b = bases.ElementAt(i);
                IEnumerable<KeyValuePair<int, KeyValuePair<int, string>>> vals = values.Where(v => v.Value.Key == b.Key);

                output.Write(vals.Count());
                output.Write(b.Value);

                for (int j = 0; j < vals.Count(); j++)
                {
                    KeyValuePair<int, KeyValuePair<int, string>> v = vals.ElementAt(j);

                    output.Write(v.Key);
                    output.Write(v.Value.Value);
                }
            }
        }

        public override string GetRuntimeType(TargetPlatform targetPlatform)
        {
            return typeof(R).AssemblyQualifiedName;
        }

        public override string GetRuntimeReader(TargetPlatform targetPlatform)
        {
            return "Mentula.BasicContent.R.MRReader, MentulaBasicContent, Version=1.0.0.0, Culture=neutral";
        }
    }
}