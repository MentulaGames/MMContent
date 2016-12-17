namespace Mentula.Content.Core
{
    using Collections;
    using Microsoft.Xna.Framework.Content.Pipeline;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Text;
    using ATTR = System.Collections.Generic.KeyValuePair<string, string>;

    /// <summary>
    /// Represents the compiled version of a .mm file.
    /// </summary>
#if !DEBUG
    [System.Diagnostics.DebuggerStepThrough]
#endif
    public class MMSource : Container
    {
        private Stack<Container> cntStack;

        internal MMSource(string cmpr, string filename, ContentImporterContext context)
        {
            cntStack = new Stack<Container>();
            context.Logger.LogMessage("Started converting.");

            string[] lines = SplitLines(cmpr);
            bool lineSkip = false;

            for (int i = 0; i < lines.Length; i++)
            {
                string line = lines[i];
                try
                {
                    if (line.Length == 1)
                    {
                        char command = line[0];
                        if (command == '{') StartContainer(lines, i);
                        else if (command == '}') EndContainer();
                        lineSkip = false;
                    }
                    else
                    {
                        if (i + 1 < lines.Length && lines[i + 1] != "{") ProcessAttributes(line);
                        else if (!lineSkip) lineSkip = true;
                        else context.Logger.LogWarning(null, new ContentIdentity(filename), "Skipped line '{0}'!", line);
                    }
                }
                catch (Exception e)
                {
                    context.Logger.LogImportantMessage("An error occured: {0}", e.ToString());
                    throw new BuildException($"An error occured while processing line '{line}'", e);
                }
            }

            context.Logger.LogMessage("Finished converting.");
            context.Logger.PopFile();
        }

        private void ProcessAttributes(string line)
        {
            string[] split = line.Split(new char[] { '[' }, StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < split.Length; i++)
            {
                string hdr = '[' + split[i];
                ATTR[] attributes = SplitAttrLine(hdr, false);

                if (attributes.Length > 1)
                {
                    StartContainer(new string[] { hdr, null }, 1);
                    EndContainer();
                }
                else
                {
                    ATTR attr = attributes[0];
                    cntStack.Peek().Values.Add(attr.Key, attr.Value);
                }
            }
        }

        private void EndContainer()
        {
            if (cntStack.Count == 0) throw new BuildException("No container to close");
            cntStack.Pop();
        }

        private void StartContainer(string[] lines, int index)
        {
            if (index == 0) throw new BuildException("No header found for container!");

            try
            {
                ATTR[] header = SplitAttrLine(lines[index - 1], true);
                ATTR def = header[0];

                Container cnt = new Container(def.Key);
                if (!string.IsNullOrEmpty(def.Value)) cnt.DefaultValue = def.Value;

                for (int i = 1; i < header.Length; i++)
                {
                    ATTR hdrAttr = header[i];
                    if (cnt.Values.ContainsKey(hdrAttr.Key)) throw new BuildException($"Attribute with the name '{hdrAttr.Key}' has already been added.");
                    cnt.Values.Add(hdrAttr.Key, hdrAttr.Value);
                }

                if (cntStack.Count == 0)
                {
                    Name = cnt.Name;
                    DefaultValue = cnt.DefaultValue;
                    Values = cnt.Values;
                    cntStack.Push(this);
                }
                else
                {
                    cntStack.Peek().AddChild(cnt);
                    cntStack.Push(cnt);
                }
            }
            catch (Exception e)
            {
                throw new BuildException($"An error occured while starting a container with the header '{lines[index - 1]}'", e);
            }
        }

        private ATTR[] SplitAttrLine(string line, bool header)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(line)) throw new BuildException("Syntax error: expected a attribute.");
                if (line[0] != '[') throw new BuildException("Syntax error: expected a attribute opener.");

                ATTR[] result = new ATTR[1];
                if (line.ContainsAmount(c => c == ':' || c == '=', 2))
                {
                    int end = line.IndexOf(':');
                    int end2 = line.IndexOf('=');
                    if (end == -1) end = end2;

                    if (end != -1 && end2 != -1) end = Math.Min(end, end2);
                    if (end == -1) throw new BuildException("Syntax error: expected a separator.");

                    string name = line.Substring(1, end - 1);
                    result[0] = new ATTR(name, null);

                    for (int i = end + 1; i < line.Length; i++)
                    {
                        result = result.Add(SplitAttr(line, ref i, false));
                    }
                }
                else
                {
                    int i = 1;
                    result[0] = SplitAttr(line, ref i, header);
                }

                return result;
            }
            catch (Exception e)
            {
                throw new BuildException($"An error occured while processing attributes.", e);
            }
        }

        private ATTR SplitAttr(string attr, ref int i, bool header)
        {
            string name = string.Empty;
            string value = string.Empty;

            try
            {
                if (string.IsNullOrWhiteSpace(attr)) throw new BuildException("Syntax error: expected a attribute.");

                for (; i < attr.Length; i++)
                {
                    char c = attr[i];
                    if (c == ':' || c == '=' || c == '[' || c == ']') break;
                    if (c == '\'' || c == '"') throw new BuildException("Syntax error: expected a separator.");
                    name += c;
                }

                if (string.IsNullOrWhiteSpace(name)) throw new BuildException("Null error: name missing.");

                for (i = i + 1; i < attr.Length; i++)
                {
                    char c = attr[i];
                    if (c == '\'' || c == '"') continue;
                    if (c == ']' || c == ',') break;
                    value += c;
                }

                if (!header && string.IsNullOrWhiteSpace(value)) throw new BuildException("Null error: value missing.");

                return new ATTR(name, value);
            }
            catch (Exception e)
            {
                throw new BuildException($"Unexpected error in attribute: '{attr}'.", e);
            }
        }

        private string[] SplitLines(string text)
        {
            string[] lines = new string[0];
            ArrayEnumerator<char> raw = ArrayEnumerator.FromString(text);
            StringBuilder sb = new StringBuilder();

            while (raw.MoveNext())
            {
                if (IsBraces(raw.Current))
                {
                    string prevLine;
                    if ((prevLine = sb.ToString()).Length > 0)
                    {
                        string header = ContainsHeaders(prevLine) ? ExtractHeader(ref prevLine) : null;

                        lines = lines.Add(prevLine);
                        if (!string.IsNullOrEmpty(header)) lines = lines.Add(header);
                        sb.Clear();
                    }

                    lines = lines.Add(raw.Current.ToString());
                    continue;
                }

                sb.Append(raw.Current);
            }

            return lines;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static bool IsBraces(char c)
        {
            return c == '{' || c == '}';
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static bool ContainsHeaders(string str)
        {
            return str.ContainsAmount('[', 2);
        }

        private static string ExtractHeader(ref string line)
        {
            int start = line.LastIndexOf('[');
            string header = line.Substring(start);
            line = line.Remove(start);
            return header;
        }
    }
}