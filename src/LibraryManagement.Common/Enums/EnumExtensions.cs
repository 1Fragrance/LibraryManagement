using System;
using System.ComponentModel;

namespace LibraryManagement.Common.Enums
{
    public static class EnumExtensions
    {
        public static string GetDescription(this Enum enumeration)
        {
            var fi = enumeration.GetType().GetField(enumeration.ToString());
            if (fi != null)
            {
                var attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);

                return attributes.Length > 0 ? attributes[0].Description : enumeration.ToString();
            }

            return null;
        }

        public static T GetValueFromName<T>(string description)
        {
            var type = typeof(T);
            if (!type.IsEnum)
            {
                throw new InvalidOperationException();
            }

            foreach (var field in type.GetFields())
            {
                if (field.Name == description)
                {
                    return (T)field.GetValue(null);
                }

                if (Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) is DescriptionAttribute attribute)
                {
                    if (attribute.Description == description)
                    {
                        return (T) field.GetValue(null);
                    }
                }
            }

            return default;
        }
    }
}
