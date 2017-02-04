namespace Mentula.BasicContent.Config
{
    using Core;
    using Core.Runtime;
    using System;

    /// <summary>
    /// A container for loading <see cref="Config"/> files.
    /// </summary>
    public static class ConfigLoader
    {
        /// <summary>
        /// The last error thrown by the <see cref="ConfigLoader"/>.
        /// </summary>
        public static BuildException LastError { get; private set; }

        /// <summary>
        /// Load a specified <see cref="Config"/> file.
        /// </summary>
        /// <param name="path"> The relative path to the <see cref="Config"/> file. </param>
        /// <param name="msg"> The function used to log messages. </param>
        /// <param name="war"> The function used to log warnings. </param>
        /// <param name="err"> The function used to log errors. </param>
        /// <returns> A <see cref="Config"/> file at the specified path, if an <see cref="BuildException"/> occured; null. </returns>
        public static Config Load(string path, Log msg = null, Log war = null, Log err = null)
        {
            try
            {
                MMSource src = MentulaLoader.Load(path, msg, war, err);
                return new Config(src, path);
            }
            catch(BuildException be)
            {
                LastError = be;
                return null;
            }
        }
    }
}