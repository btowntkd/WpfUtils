using System;

namespace WpfUtils.Extensions
{
    /// <summary>
    /// Contains extension methods for the IComparable interface.
    /// </summary>
    public static class IComparableExtension
    {
        /// <summary>
        /// Determines if the current value occurs with the range of values between lower 
        /// and upper (including the lower and upper values).
        /// </summary>
        /// <typeparam name="T">The type of the value to compare.</typeparam>
        /// <param name="value">The target IComparable object.</param>
        /// <param name="lower">The lower value in the range.</param>
        /// <param name="upper">The upper value in the range.</param>
        /// <returns>
        /// Returns true if the current value is less-than or equal-to the upper value,
        /// and greater-than or equal-to the lower value.  Otherwise, returns false.
        /// </returns>
        public static bool IsInRange<T>(this IComparable value, T lower, T upper) where T : IComparable
        {
            if (lower.CompareTo(upper) > 0)
            {
                throw new ArgumentException("lower value must be less than or equal to upper value.");
            }

            return (value.CompareTo(lower) >= 0)
                && (value.CompareTo(upper) <= 0);

        }
    }
}
