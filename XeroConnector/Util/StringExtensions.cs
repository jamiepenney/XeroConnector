using System;

namespace XeroConnector.Util
{
    public static class StringExtensions
    {
        public static Guid ToGuid(this string value)
        {
            Guid g;
            return Guid.TryParse(value, out g) ? g : Guid.Empty;
        }

        public static T? As<T>(this string s) where T : struct, IConvertible
        {
            if (string.IsNullOrEmpty(s))
                return null;

            try
            {
                Type type = typeof(T);
                bool isEnum = typeof(Enum).IsAssignableFrom(type);
                return (T)(isEnum
                    ? Enum.Parse(type, s, true)
                    : Convert.ChangeType(s, type));
            }
            catch
            {
                return null;
            }
        }
    }
}