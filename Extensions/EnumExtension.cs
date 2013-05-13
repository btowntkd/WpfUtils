using System;
using System.ComponentModel;
using System.Reflection;

namespace WpfUtils.Extensions
{
    /// <summary>
    /// Contains extension methods for Enums, relevant to the EnumDescriptionAttribute.
    /// </summary>
    public static class EnumExtension
    {
        /// <summary>
        /// Retrieve a string value for the current enum, as specified by a <see cref="DescriptionAttribute"/>.
        /// </summary>
        /// <param name="value">The target enum object.</param>
        /// <returns>
        /// Returns the string value for the current enum, as specified by a <see cref="DescriptionAttribute"/>.
        /// If no attribute exists, it will return the enum's ToString value.
        /// </returns>
        public static string GetDescription(this Enum value)
        {
            return value.GetDescription(value.ToString());
        }

        /// <summary>
        /// Retrieve a string value for the current enum, as specified by a <see cref="DescriptionAttribute"/>.
        /// </summary>
        /// <param name="value">The target enum object.</param>
        /// <param name="defaultDescription">The default description to return, if no <see cref="DescriptionAttribute"/> exists.</param>
        /// <returns>
        /// Returns the string value for the current enum, as specified by a <see cref="DescriptionAttribute"/>.
        /// If no attribute exists, returns defaultDescription.
        /// </returns>
        public static string GetDescription(this Enum value, string defaultDescription)
        {
            Type type = value.GetType();
            FieldInfo fieldInfo = type.GetField(value.ToString());

            var attributes = fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), true) as DescriptionAttribute[];
            return attributes.Length > 0 ? attributes[0].Description : defaultDescription;
        }
    }
}
