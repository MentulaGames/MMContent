namespace Mentula.BasicContent.Core.Runtime
{
    using Microsoft.Xna.Framework.Content.Pipeline;
    using System.Runtime.CompilerServices;

    /// <summary>
    /// Represents a function used for logging content loading messages.
    /// </summary>
    /// <param name="msg"> Message being reported. </param>
    /// <param name="args"> Arguments for the reported message </param>
    public delegate void Log(string msg, params object[] args);

    internal sealed class MentulaLogger : ContentBuildLogger
    {
        private Log msg, war, err;

        public MentulaLogger(Log msg, Log war, Log err)
        {
            this.msg = msg;
            this.war = war;
            this.err = err;
        }

        public override void LogImportantMessage(string message, params object[] messageArgs)
        {
            Invoke(err, message, messageArgs);
        }

        public override void LogMessage(string message, params object[] messageArgs)
        {
            Invoke(msg, message, messageArgs);
        }

        public override void LogWarning(string helpLink, ContentIdentity contentIdentity, string message, params object[] messageArgs)
        {
            Invoke(war, message, messageArgs);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void Invoke(Log log, string msg, object[] args)
        {
            if (log != null) log(msg, args);
        }
    }
}