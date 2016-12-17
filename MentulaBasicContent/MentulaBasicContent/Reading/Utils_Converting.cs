namespace Mentula.Content.Reading
{
    using Microsoft.Xna.Framework;
    using System;
    using System.Globalization;

    public static partial class Utils
    {
        internal static readonly CultureInfo usInfo = CultureInfo.CreateSpecificCulture("en-US");

        /// <summary>
        /// Converts the specified exception to a <see cref="ContainerException"/>.
        /// </summary>
        /// <param name="e"> The specified exception. </param>
        /// <param name="name"> The name of the container. </param>
        /// <returns> <paramref name="e"/> as a <see cref="ContainerException"/>. </returns>
        public static ContainerException AsContainerException(this Exception e, string name)
        {
            return new ContainerException(name, e);
        }

        /// <summary>
        /// Tries to convert the specified value to a Int16.
        /// </summary>
        /// <param name="name"> The name of the parameter. </param>
        /// <param name="value"> The value of the paramter. </param>
        /// <returns> <paramref name="value"/> as a Int16. </returns>
        /// <exception cref="ParameterException"> <paramref name="value"/> could not be converted. </exception>
        public static short ConvertToInt16(string name, string value)
        {
            short result;
            if (short.TryParse(value, out result)) return result;
            else throw new ParameterException(name, value, typeof(short));
        }

        /// <summary>
        /// Tries to convert the specified value to a Int32.
        /// </summary>
        /// <param name="name"> The name of the parameter. </param>
        /// <param name="value"> The value of the paramter. </param>
        /// <returns> <paramref name="value"/> as a Int32. </returns>
        /// <exception cref="ParameterException"> <paramref name="value"/> could not be converted. </exception>
        public static int ConvertToInt32(string name, string value)
        {
            int result;
            if (int.TryParse(value, out result)) return result;
            else throw new ParameterException(name, value, typeof(int));
        }

        /// <summary>
        /// Tries to convert the specified value to a Int64.
        /// </summary>
        /// <param name="name"> The name of the parameter. </param>
        /// <param name="value"> The value of the paramter. </param>
        /// <returns> <paramref name="value"/> as a Int64. </returns>
        /// <exception cref="ParameterException"> <paramref name="value"/> could not be converted. </exception>
        public static long ConvertToInt64(string name, string value)
        {
            long result;
            if (long.TryParse(value, out result)) return result;
            else throw new ParameterException(name, value, typeof(long));
        }


        /// <summary>
        /// Tries to convert the specified value to a UInt16.
        /// </summary>
        /// <param name="name"> The name of the parameter. </param>
        /// <param name="value"> The value of the paramter. </param>
        /// <returns> <paramref name="value"/> as a UInt16. </returns>
        /// <exception cref="ParameterException"> <paramref name="value"/> could not be converted. </exception>
        public static ushort ConvertToUInt16(string name, string value)
        {
            ushort result;
            if (ushort.TryParse(value, out result)) return result;
            else throw new ParameterException(name, value, typeof(ushort));
        }

        /// <summary>
        /// Tries to convert the specified value to a UInt32.
        /// </summary>
        /// <param name="name"> The name of the parameter. </param>
        /// <param name="value"> The value of the paramter. </param>
        /// <returns> <paramref name="value"/> as a UInt32. </returns>
        /// <exception cref="ParameterException"> <paramref name="value"/> could not be converted. </exception>
        public static uint ConvertToUInt32(string name, string value)
        {
            uint result;
            if (uint.TryParse(value, out result)) return result;
            else throw new ParameterException(name, value, typeof(uint));
        }

        /// <summary>
        /// Tries to convert the specified value to a UInt64.
        /// </summary>
        /// <param name="name"> The name of the parameter. </param>
        /// <param name="value"> The value of the paramter. </param>
        /// <returns> <paramref name="value"/> as a UInt64. </returns>
        /// <exception cref="ParameterException"> <paramref name="value"/> could not be converted. </exception>
        public static ulong ConvertToUInt64(string name, string value)
        {
            ulong result;
            if (ulong.TryParse(value, out result)) return result;
            else throw new ParameterException(name, value, typeof(ulong));
        }

        /// <summary>
        /// Tries to convert the specified value to a Single.
        /// </summary>
        /// <param name="name"> The name of the parameter. </param>
        /// <param name="value"> The value of the paramter. </param>
        /// <returns> <paramref name="value"/> as a Single. </returns>
        /// <exception cref="ParameterException"> <paramref name="value"/> could not be converted. </exception>
        public static float ConvertToFloat(string name, string value)
        {
            float result;
            if (TryParse(value, out result)) return result;
            else throw new ParameterException(name, value, typeof(float));
        }

        /// <summary>
        /// Tries to convert the specified value to a Boolean.
        /// </summary>
        /// <param name="name"> The name of the parameter. </param>
        /// <param name="value"> The value of the paramter. </param>
        /// <returns> <paramref name="value"/> as a Boolean. </returns>
        /// <exception cref="ParameterException"> <paramref name="value"/> could not be converted. </exception>
        public static bool ConvertToBool(string name, string value)
        {
            bool result;
            if (bool.TryParse(value, out result)) return result;
            else throw new ParameterException(name, value, typeof(bool));
        }

        /// <summary>
        /// Tries to convert the specified value to a Color.
        /// </summary>
        /// <param name="name"> The name of the parameter. </param>
        /// <param name="value"> The value of the paramter. </param>
        /// <returns> <paramref name="value"/> as a Color. </returns>
        /// <exception cref="ParameterException"> <paramref name="value"/> could not be converted. </exception>
        public static Color ConvertToColor(string name, string value)
        {
            string[] rgba = value.Split(new char[1] { ';' }, StringSplitOptions.RemoveEmptyEntries);
            if (rgba.Length > 2 && rgba.Length < 5)
            {
                byte r = 0, g = 0, b = 0, a = 0;

                bool result = byte.TryParse(rgba[0], out r);
                result = result && byte.TryParse(rgba[1], out g);
                result = result && byte.TryParse(rgba[2], out b);
                if (rgba.Length == 3) a = byte.MaxValue;
                else result = result && byte.TryParse(rgba[3], out a);

                if (result) return new Color(r, g, b, a);
                else throw new ParameterException(name, value, typeof(Color));
            }
            else throw new ParameterException(name, value, typeof(Color));
        }

        private static bool TryParse(string s, out float result)
        {
            return float.TryParse(s, NumberStyles.Number, usInfo, out result);
        }
    }
}