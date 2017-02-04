namespace Mentula.BasicContent
{
    using System;

    /// <summary>
    /// Represents a class or struct that can be saved as a .mm file.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, Inherited = false, AllowMultiple = false)]
    public sealed class MMEditable : Attribute { }

    /// <summary>
    /// Represents a field or property that should be ignored when saving a class as a .mm file.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
    public sealed class MMIgnore : Attribute { }

    /// <summary>
    /// Represents a field or property that is optional in the .mm file.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
    public sealed class MMOptional : Attribute { }

    /// <summary>
    /// Represents a alternative name for a class, struct, field or property when saving or loading a class as a .mm file.
    /// </summary>
    [AttributeUsage(AttributeTargets.All, Inherited = false, AllowMultiple = false)]
    public sealed class AlternativeName : Attribute
    {
        /// <summary> The name to be used in conversion. </summary>
        public readonly string NewName;

        /// <summary>
        /// Initializes a new instance of the <see cref="AlternativeName"/> class with a specified name.
        /// </summary>
        /// <param name="name"> The new name to be used. </param>
        public AlternativeName(string name)
        {
            NewName = name;
        }
    }

    /// <summary>
    /// Represents a field or property the reader or writer should interpret as the default value when saving or loading a class as a .mm file.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
    public sealed class MMIsDefault : Attribute { }

    /// <summary>
    /// Represents a field or property the reader or writer should interpret as the name of the container when saving or loading a class as a .mm file.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
    public sealed class MMIsName : Attribute { }
}