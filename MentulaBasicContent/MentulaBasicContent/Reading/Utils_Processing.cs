namespace Mentula.BasicContent.Reading
{
    using Core;
    using Microsoft.Xna.Framework;

    public static partial class Utils
    {
        private const string DEFAULT_VISUAL_NAME = "<<DEFAULT>>";

        /// <summary>
        /// Get the name of the specified container.
        /// </summary>
        /// <param name="cnt"> The specified container. </param>
        /// <returns> The <see cref="Container.Name"/> attribute of the specified <see cref="Container"/>. </returns>
        /// <exception cref="ParameterNullException"> The container has no name specified. </exception>
        public static string GetContainerName(this Container cnt)
        {
            if (!string.IsNullOrEmpty(cnt.Name)) return cnt.Name;
            else throw new ParameterNullException("Container name");
        }

        /// <summary>
        /// Get a specified value from the cotainer as a String.
        /// </summary>
        /// <param name="cnt"> The specified container. </param>
        /// <param name="name"> The specified name of the value. </param>
        /// <returns> The value as a String. </returns>
        /// <exception cref="ParameterNullException"> The container does not contain an argument with the specified name. </exception>
        public static string GetStringValue(this Container cnt, string name)
        {
            string rawValue;
            if (cnt.TryGetValue(name, out rawValue)) return rawValue;
            else throw new ParameterNullException(name);
        }

        /// <summary>
        /// Get a specified value from the container as a Int16.
        /// </summary>
        /// <param name="cnt"> The specified container. </param>
        /// <param name="name"> The specified name of the value. </param>
        /// <returns> The value as Int16. </returns>
        /// <exception cref="ParameterNullException"> The container does not contain an argument with the specified name. </exception>
        /// <exception cref="ParameterException"> The value could not be converted. </exception>
        public static short GetInt16Value(this Container cnt, string name)
        {
            string rawValue = cnt.GetStringValue(name);
            return ConvertToInt16(name, rawValue);
        }

        /// <summary>
        /// Get a specified value from the container as a Int32.
        /// </summary>
        /// <param name="cnt"> The specified container. </param>
        /// <param name="name"> The specified name of the value. </param>
        /// <returns> The value as Int32. </returns>
        /// <exception cref="ParameterNullException"> The container does not contain an argument with the specified name. </exception>
        /// <exception cref="ParameterException"> The value could not be converted. </exception>
        public static int GetInt32Value(this Container cnt, string name)
        {
            string rawValue = cnt.GetStringValue(name);
            return ConvertToInt32(name, rawValue);
        }

        /// <summary>
        /// Get a specified value from the container as a Int64.
        /// </summary>
        /// <param name="cnt"> The specified container. </param>
        /// <param name="name"> The specified name of the value. </param>
        /// <returns> The value as Int64. </returns>
        /// <exception cref="ParameterNullException"> The container does not contain an argument with the specified name. </exception>
        /// <exception cref="ParameterException"> The value could not be converted. </exception>
        public static long GetInt64Value(this Container cnt, string name)
        {
            string rawValue = cnt.GetStringValue(name);
            return ConvertToInt64(name, rawValue);
        }

        /// <summary>
        /// Get a specified value from the container as a UInt16.
        /// </summary>
        /// <param name="cnt"> The specified container. </param>
        /// <param name="name"> The specified name of the value. </param>
        /// <returns> The value as UInt16. </returns>
        /// <exception cref="ParameterNullException"> The container does not contain an argument with the specified name. </exception>
        /// <exception cref="ParameterException"> The value could not be converted. </exception>
        public static ushort GetUInt16Value(this Container cnt, string name)
        {
            string rawValue = cnt.GetStringValue(name);
            return ConvertToUInt16(name, rawValue);
        }

        /// <summary>
        /// Get a specified value from the container as a UInt32.
        /// </summary>
        /// <param name="cnt"> The specified container. </param>
        /// <param name="name"> The specified name of the value. </param>
        /// <returns> The value as UInt32. </returns>
        /// <exception cref="ParameterNullException"> The container does not contain an argument with the specified name. </exception>
        /// <exception cref="ParameterException"> The value could not be converted. </exception>
        public static uint GetUInt32Value(this Container cnt, string name)
        {
            string rawValue = cnt.GetStringValue(name);
            return ConvertToUInt32(name, rawValue);
        }

        /// <summary>
        /// Get a specified value from the container as a UInt64.
        /// </summary>
        /// <param name="cnt"> The specified container. </param>
        /// <param name="name"> The specified name of the value. </param>
        /// <returns> The value as UInt64. </returns>
        /// <exception cref="ParameterNullException"> The container does not contain an argument with the specified name. </exception>
        /// <exception cref="ParameterException"> The value could not be converted. </exception>
        public static ulong GetUInt64Value(this Container cnt, string name)
        {
            string rawValue = cnt.GetStringValue(name);
            return ConvertToUInt64(name, rawValue);
        }

        /// <summary>
        /// Get a specified value from the container as a Single.
        /// </summary>
        /// <param name="cnt"> The specified container. </param>
        /// <param name="name"> The specified name of the value. </param>
        /// <returns> The value as Single. </returns>
        /// <exception cref="ParameterNullException"> The container does not contain an argument with the specified name. </exception>
        /// <exception cref="ParameterException"> The value could not be converted. </exception>
        public static float GetFloatValue(this Container cnt, string name)
        {
            string rawValue = cnt.GetStringValue(name);
            return ConvertToFloat(name, rawValue);
        }

        /// <summary>
        /// Get a specified value from the container as a Boolean.
        /// </summary>
        /// <param name="cnt"> The specified container. </param>
        /// <param name="name"> The specified name of the value. </param>
        /// <returns> The value as Boolean. </returns>
        /// <exception cref="ParameterNullException"> The container does not contain an argument with the specified name. </exception>
        /// <exception cref="ParameterException"> The value could not be converted. </exception>
        public static bool GetBoolValue(this Container cnt, string name)
        {
            string rawValue = cnt.GetStringValue(name);
            return ConvertToBool(name, rawValue);
        }

        /// <summary>
        /// Get a specified value from the container as a Color.
        /// </summary>
        /// <param name="cnt"> The specified container. </param>
        /// <param name="name"> The specified name of the value. </param>
        /// <returns> The value as Color. </returns>
        /// <exception cref="ParameterNullException"> The container does not contain an argument with the specified name. </exception>
        /// <exception cref="ParameterException"> The value could not be converted. </exception>
        public static Color GetColorValue(this Container cnt, string name)
        {
            string rawValue = cnt.GetStringValue(name);
            return ConvertToColor(name, rawValue);
        }

        /// <summary>
        /// Gets the default value as a String.
        /// </summary>
        /// <param name="cnt"> The specified container. </param>
        /// <returns> The default value as String. </returns>
        /// <exception cref="ParameterNullException"> The container does not contain an default value. </exception>
        public static string GetStringDefault(this Container cnt)
        {
            if (cnt.DefaultSet) return cnt.DefaultValue;
            else throw new ParameterNullException(DEFAULT_VISUAL_NAME);
        }

        /// <summary>
        /// Gets the default value as a Int16.
        /// </summary>
        /// <param name="cnt"> The specified container. </param>
        /// <returns> The default value as Int16. </returns>
        /// <exception cref="ParameterNullException"> The container does not contain an default value. </exception>
        /// <exception cref="ParameterException"> The value could not be converted. </exception>
        public static short GetInt16Default(this Container cnt)
        {
            string rawValue = cnt.GetStringDefault();
            return ConvertToInt16(DEFAULT_VISUAL_NAME, rawValue);
        }

        /// <summary>
        /// Gets the default value as a Int32.
        /// </summary>
        /// <param name="cnt"> The specified container. </param>
        /// <returns> The default value as Int132. </returns>
        /// <exception cref="ParameterNullException"> The container does not contain an default value. </exception>
        /// <exception cref="ParameterException"> The value could not be converted. </exception>
        public static int GetInt32Default(this Container cnt)
        {
            string rawValue = cnt.GetStringDefault();
            return ConvertToInt32(DEFAULT_VISUAL_NAME, rawValue);
        }

        /// <summary>
        /// Gets the default value as a Int64.
        /// </summary>
        /// <param name="cnt"> The specified container. </param>
        /// <returns> The default value as Int64. </returns>
        /// <exception cref="ParameterNullException"> The container does not contain an default value. </exception>
        /// <exception cref="ParameterException"> The value could not be converted. </exception>
        public static long GetInt64Default(this Container cnt)
        {
            string rawValue = cnt.GetStringDefault();
            return ConvertToInt64(DEFAULT_VISUAL_NAME, rawValue);
        }

        /// <summary>
        /// Gets the default value as a UInt16.
        /// </summary>
        /// <param name="cnt"> The specified container. </param>
        /// <returns> The default value as UInt16. </returns>
        /// <exception cref="ParameterNullException"> The container does not contain an default value. </exception>
        /// <exception cref="ParameterException"> The value could not be converted. </exception>
        public static ushort GetUInt16Default(this Container cnt)
        {
            string rawValue = cnt.GetStringDefault();
            return ConvertToUInt16(DEFAULT_VISUAL_NAME, rawValue);
        }

        /// <summary>
        /// Gets the default value as a UInt32.
        /// </summary>
        /// <param name="cnt"> The specified container. </param>
        /// <returns> The default value as UInt32. </returns>
        /// <exception cref="ParameterNullException"> The container does not contain an default value. </exception>
        /// <exception cref="ParameterException"> The value could not be converted. </exception>
        public static uint GetUInt32Default(this Container cnt)
        {
            string rawValue = cnt.GetStringDefault();
            return ConvertToUInt32(DEFAULT_VISUAL_NAME, rawValue);
        }

        /// <summary>
        /// Gets the default value as a UInt64.
        /// </summary>
        /// <param name="cnt"> The specified container. </param>
        /// <returns> The default value as UInt64. </returns>
        /// <exception cref="ParameterNullException"> The container does not contain an default value. </exception>
        /// <exception cref="ParameterException"> The value could not be converted. </exception>
        public static ulong GetUInt64Default(this Container cnt)
        {
            string rawValue = cnt.GetStringDefault();
            return ConvertToUInt64(DEFAULT_VISUAL_NAME, rawValue);
        }

        /// <summary>
        /// Gets the default value as a Single.
        /// </summary>
        /// <param name="cnt"> The specified container. </param>
        /// <returns> The default value as Single. </returns>
        /// <exception cref="ParameterNullException"> The container does not contain an default value. </exception>
        /// <exception cref="ParameterException"> The value could not be converted. </exception>
        public static float GetFloatDefault(this Container cnt)
        {
            string rawValue = cnt.GetStringDefault();
            return ConvertToFloat(DEFAULT_VISUAL_NAME, rawValue);
        }

        /// <summary>
        /// Gets the default value as a Boolean.
        /// </summary>
        /// <param name="cnt"> The specified container. </param>
        /// <returns> The default value as Boolean. </returns>
        /// <exception cref="ParameterNullException"> The container does not contain an default value. </exception>
        /// <exception cref="ParameterException"> The value could not be converted. </exception>
        public static bool GetBoolDefault(this Container cnt)
        {
            string rawValue = cnt.GetStringDefault();
            return ConvertToBool(DEFAULT_VISUAL_NAME, rawValue);
        }

        /// <summary>
        /// Gets the default value as a Color.
        /// </summary>
        /// <param name="cnt"> The specified container. </param>
        /// <returns> The default value as Color. </returns>
        /// <exception cref="ParameterNullException"> The container does not contain an default value. </exception>
        /// <exception cref="ParameterException"> The value could not be converted. </exception>
        public static Color GetColorDefault(this Container cnt)
        {
            string rawValue = cnt.GetStringDefault();
            return ConvertToColor(DEFAULT_VISUAL_NAME, rawValue);
        }
    }
}