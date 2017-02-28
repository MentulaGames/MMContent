#if MONO
extern alias Mono;
#else
extern alias Xna;
#endif

namespace Mentula.BasicContent.Core.Runtime
{
#if MONO
    using Mono.Microsoft.Xna.Framework.Content.Pipeline;
#else
    using Xna.Microsoft.Xna.Framework.Content.Pipeline;
#endif
    using System;

    internal sealed class MentulaContext : ContentImporterContext
    {
        public override string IntermediateDirectory { get { throw new NotImplementedException(); } }
        public override string OutputDirectory { get { throw new NotImplementedException(); } }
        public override ContentBuildLogger Logger { get { return logger; } }

        private MentulaLogger logger;

        public MentulaContext(Log msg, Log war, Log err)
        {
            logger = new MentulaLogger(msg, war, err);
        }

        public override void AddDependency(string filename)
        {
            throw new NotImplementedException();
        }
    }
}