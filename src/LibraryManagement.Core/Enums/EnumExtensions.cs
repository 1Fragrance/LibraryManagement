using System;
using System.ComponentModel;

namespace LibraryManagement.Core.Enums
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

    }
}
