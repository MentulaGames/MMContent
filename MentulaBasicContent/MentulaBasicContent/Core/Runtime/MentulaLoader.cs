namespace Mentula.BasicContent.Core.Runtime
{
    using System;

    internal static class MentulaLoader
    {
        private static MMImporter importer;
        private static MentulaContext context;
        private static bool initialized;

        internal static MMSource Load(string path, Log msg, Log war, Log err)
        {
            Initialize(msg, war, err);
            return Import(path);
        }

        private static MMSource Import(string path)
        {
            if (!initialized) throw new InvalidOperationException("Initialize must be called before calling import!");
            return importer.Import(path, context);
        }

        private static void Initialize(Log msg, Log war, Log err)
        {
            if (!initialized)
            {
                importer = new MMImporter();
                context = new MentulaContext(msg, war, err);
                initialized = true;
            }
        }
    }
}