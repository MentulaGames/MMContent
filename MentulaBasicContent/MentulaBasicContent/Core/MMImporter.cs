#if MONO
extern alias Mono;
#else
extern alias Xna;
#endif

namespace Mentula.BasicContent.Core
{
#if MONO
    using Mono.Microsoft.Xna.Framework.Content.Pipeline;
#else
    using Xna.Microsoft.Xna.Framework.Content.Pipeline;
#endif
    using Collections;
    using System;
    using System.IO;
    using System.Runtime.CompilerServices;
    using System.Text;

    /// <summary>
    /// The default importer for mentula material (.mm) files.
    /// </summary>
#if !DEBUG
    [System.Diagnostics.DebuggerStepThrough]
#endif
    [ContentImporter(".mm", DefaultProcessor = "MMProcessor", DisplayName = "Mentula Importer")]
    public class MMImporter : ContentImporter<MMSource>
    {
        private static readonly char[] validControlls = new char[]
        {
            '\n',
            '\t',
            '\r'
        };

        /// <summary>
        /// Imports the specified file as a .mm file.
        /// </summary>
        /// <param name="filename"> The absolute path to the file. </param>
        /// <param name="context"> The specified context for importing. </param>
        /// <returns></returns>
        public override MMSource Import(string filename, ContentImporterContext context)
        {
            context.Logger.PushFile(filename);
            context.Logger.LogMessage($"Importing: {Path.GetFileName(filename)}.");
            context.Logger.LogMessage("Starting text compression.");

            try
            {
                ArrayEnumerator<char> raw = ArrayEnumerator.FromString(File.ReadAllText(filename));
                StringBuilder cmpr = new StringBuilder();

                bool inside = false, comment = false;
                while (raw.MoveNext())
                {
                    char c = raw.Current;

                    if (IsControl(c))
                    {
                        if (IsInvalid(c)) LogInvalid(context, filename, c);
                        continue;      // Outside of ascii packet
                    }

                    if (c == ' ' && !inside) continue;
                    else if (c == '"')
                    {
                        inside = !inside;
                        continue;
                    }

                    if (c == '/')
                    {
                        if (!raw.MoveNext()) raw.MovePrevious();
                        else if (raw.Current == '*') comment = true;
                        else raw.MovePrevious();
                    }

                    if (!comment) cmpr.Append(c);
                    else if (c == '*')
                    {
                        if (!raw.MoveNext()) raw.MovePrevious();
                        else if (raw.Current == '/') comment = false;
                        else raw.MovePrevious();
                    }
                }

                context.Logger.LogMessage("Finished text compression.");
                return new MMSource(cmpr.ToString(), filename, context);
            }
            catch (Exception e)
            {
                context.Logger.LogImportantMessage("An error occurred: {0}", e.ToString());
                context.Logger.PopFile();
                throw new BuildException($"An error occured while importing '{Path.GetFileName(filename)}'.", e);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static bool IsControl(char c)
        {
            return c < 32 || c > 126;
        }

        private static bool IsInvalid(char c)
        {
            for (int i = 0; i < validControlls.Length; i++)
            {
                if (c == validControlls[i]) return false;
            }

            return true;
        }

        private static void LogInvalid(ContentImporterContext context, string filename, char c)
        {
            context.Logger.LogWarning(
                "http://www.asciitable.com/",
                new ContentIdentity(filename),
                "Character {0}({1}) is not valid and has been skipped!",
                c, (byte)c);
        }
    }
}