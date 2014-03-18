using System;

namespace WpfUtils
{
    public static class MathEx
    {
        /// <summary>
        /// Convert the given value from degrees to radians.
        /// </summary>
        /// <param name="degrees">An angle, in degrees.</param>
        /// <returns>Returns the equivalent angle in radians.</returns>
        public static double ToRadians(double degrees)
        {
            return degrees * (Math.PI / 180.0);
        }

        /// <summary>
        /// Convert the given value from radians to degrees.
        /// </summary>
        /// <param name="degrees">An angle, in radians.</param>
        /// <returns>Returns the equivalent angle in degrees.</returns>
        public static double ToDegrees(double radians)
        {
            return radians / (Math.PI / 180.0);
        }
    }
}
