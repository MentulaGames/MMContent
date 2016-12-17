namespace Mentula.Content.Core
{
    using Reading;
    using System;
    using System.ComponentModel;
    using System.Globalization;

#if !DEBUG
    [System.Diagnostics.DebuggerStepThrough]
#endif
    internal sealed class ColorTypeConverter : TypeConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            if (sourceType == typeof(string)) return true;
            return base.CanConvertFrom(context, sourceType);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            if (value.GetType() == typeof(string)) return Utils.ConvertToColor("<<NoName>>", (string)value);
            return base.ConvertFrom(context, culture, value);
        }
    }
}