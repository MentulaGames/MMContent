namespace Mentula.BasicContent.Core
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;

    /// <summary>
    /// Represents a container for storing the data from a .mm file.
    /// </summary>
#if !DEBUG
    [DebuggerStepThrough]
#endif
    [DebuggerDisplay("{ToString()}")]
    public class Container
    {
        /// <summary> The name the container. </summary>
        public string Name;
        /// <summary> The default value of the container. </summary>
        public string DefaultValue;
        /// <summary> The raw values of the container. </summary>
        public Dictionary<string, string> Values;
        /// <summary> The child containers of this container. </summary>
        public Container[] Childs;

        /// <summary> Gets whether the default value has been set. </summary>
        public bool DefaultSet { get { return !string.IsNullOrEmpty(DefaultValue); } }
        /// <summary> Gets whether the container is useless (no name or no underlying values). </summary>
        public bool IsUseless { get { return string.IsNullOrWhiteSpace(Name) || (Values.Count == 0 && Childs.Length == 0); } }

        internal Container()
        {
            Name = string.Empty;
            Values = new Dictionary<string, string>();
            Childs = new Container[0];
        }

        internal Container(string name)
        {
            Name = name;
            Values = new Dictionary<string, string>();
            Childs = new Container[0];
        }

        /// <summary>
        /// Tries to get a specific raw value.
        /// </summary>
        /// <param name="name"> The name of the raw value. </param>
        /// <param name="value"> The string to assign the value. </param>
        /// <returns> Whether the raw value was found. </returns>
        [Obsolete("This method should be avoided use Utils for value loading.")]
        public bool TryGetValue(string name, out string value)
        {
            name = name.ToUpper();
            value = Values.FirstOrDefault(v => v.Key.ToUpper() == name).Value;
            return !string.IsNullOrWhiteSpace(value);
        }

        /// <summary>
        /// Tries to get a specific child container.
        /// </summary>
        /// <param name="name"> The name of the child container. </param>
        /// <param name="value"> The container to assign the value. </param>
        /// <returns> Whether the container was found. </returns>
        public bool TryGetChild(string name, out Container value)
        {
            name = name.ToUpper();
            value = Childs.FirstOrDefault(c => c.Name.ToUpper() == name);
            return value != null;
        }

        /// <summary>
        /// Returns a string that represents the current container.
        /// </summary>
        /// <returns> A string that represents the current container. </returns>
        public override string ToString()
        {
            return $"Name={Name}, NumValues={Values.Count}, NumChilds={Childs.Length}";
        }

        internal void AddChild(Container child)
        {
            int pLen = Childs.Length;
            Array.Resize(ref Childs, pLen + 1);
            Childs[pLen] = child;
        }
    }
}