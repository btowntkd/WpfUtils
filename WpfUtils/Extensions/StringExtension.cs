using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace WpfUtils.Extensions
{
    /// <summary>
    /// Contains extension methods for the String class.
    /// </summary>
    public static class StringExtension
    {
        /// <summary>
        /// Remove any instances of the specified characters from the string and return the result.
        /// </summary>
        /// <param name="str">The target string object.</param>
        /// <param name="characters">The list of characters to remove.</param>
        /// <returns>Returns the string, with all instances of the given characters removed.</returns>
        public static string RemoveAny(this string str, params char[] characters)
        {
            StringBuilder strBuilder = new StringBuilder();
            for (int x = 0; x < str.Length; x++)
            {
                if (!characters.Contains(str[x]))
                {
                    strBuilder.Append(str[x]);
                }
            }
            return strBuilder.ToString();
        }

        /// <summary>
        /// Truncates or Pads a string to a fixed width.
        /// </summary>
        /// <param name="str">The target string object.</param>
        /// <param name="width">The fixed width to which the string will be padded or truncated.</param>
        /// <param name="padChar">If the string is padded, this is the character that the string will be padded with.</param>
        /// <returns>Returns the string, truncated or padded to the given width.</returns>
        public static string ToFixedWidth(this string str, int width, char padChar = ' ')
        {
            if (str.Length < width)
            {
                return str.PadRight(width, padChar);
            }
            else if (str.Length > width)
            {
                return str.Substring(0, width);
            }
            else
            {
                return str;
            }
        }

        /// <summary>
        /// Converts the given string to a List of strings, seperated by the Environment.NewLine value.
        /// </summary>
        /// <param name="str">The target string object.</param>
        /// <returns>Returns a List of strings, seperated by NewLine characters.</returns>
        public static List<string> ToLineList(this string str)
        {
            return str.Split(new string[] { Environment.NewLine }, StringSplitOptions.None).ToList();
        }

        /// <summary>
        /// Return a formatted string with the given parameters.
        /// </summary>
        /// <param name="str">The target string object.</param>
        /// <param name="args">The format parameters to use.</param>
        /// <returns>The string with the given format parameters.</returns>
        public static string FormatWith(this string str, params object[] args)
        {
            return string.Format(str, args);
        }

        /// <summary>
        /// Parse the current string to an enum value for the given Enum type.
        /// </summary>
        /// <typeparam name="T">The Enum type to parse the string into.</typeparam>
        /// <param name="str">The target string object.</param>
        /// <returns>Returns the Enum value of the parsed string.</returns>
        public static T ToEnum<T>(this string str)
        {
            if (!typeof(T).IsEnum)
            {
                throw new ArgumentException("Template type T must be an enum.");
            }
            return (T)Enum.Parse(typeof(T), str);
        }
    }
}
