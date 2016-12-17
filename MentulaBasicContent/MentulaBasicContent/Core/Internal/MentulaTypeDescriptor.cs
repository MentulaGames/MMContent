namespace Mentula.Content.Core
{
    using Microsoft.Xna.Framework;
    using System;
    using System.ComponentModel;

    /// <summary>
    /// Containes functions for working with the <see cref="TypeDescriptor"/>.
    /// </summary>
#if !DEBUG
    [System.Diagnostics.DebuggerStepThrough]
#endif
    public static class MentulaTypeDescriptor
    {
        static MentulaTypeDescriptor()
        {
            AssignTypeConverter<Color, ColorTypeConverter>();
        }

        /// <summary>
        /// Gets a <see cref="TypeConverter"/> that can convert from string to the specified type T.
        /// </summary>
        /// <typeparam name="T"> The type to convert to. </typeparam>
        /// <returns> The required converter. </returns>
        public static TypeConverter GetFromString<T>()
        {
            TypeConverter converter = Get<T>();
            if (IsNullOrNonString(converter)) ThrowNonString<T>();
            return converter;
        }

        /// <summary>
        /// Gets a <see cref="TypeConverter"/> that can convert to the specified type T.
        /// </summary>
        /// <typeparam name="T"> The type to convert to. </typeparam>
        /// <returns> The required converter. </returns>
        public static TypeConverter Get<T>()
        {
            return TypeDescriptor.GetConverter(typeof(T));
        }

        /// <summary>
        /// Adds a <see cref="TypeConverter"/> to the default pool.
        /// </summary>
        /// <typeparam name="IType"> The type that the typeconverter can convert to. </typeparam>
        /// <typeparam name="IConverterType"> The type of class that will handle the convertion. </typeparam>
        public static void AssignTypeConverter<IType, IConverterType>()
        {
            TypeDescriptor.AddAttributes(typeof(IType), new TypeConverterAttribute(typeof(IConverterType)));
        }

        private static bool IsNullOrNonString(TypeConverter tc)
        {
            return tc == null || !tc.CanConvertFrom(typeof(string));
        }

        private static void ThrowNonString<T>()
        {
            throw new ArgumentException($"Cannot convert to type {typeof(T)}");
        }
    }
}
