using System;
using System.Collections;
using System.Collections.Generic;

namespace WpfUtils.Extensions
{
    /// <summary>
    /// Contains extension methods for the <see cref="IList"/> and
    /// <see cref="IList{T}"/> interfaces.
    /// </summary>
    public static class IListExtension
    {
        #region IList

        /// <summary>
        /// Performs an in-place Fisher-Yates shuffle on the current contents of the List.
        /// </summary>
        /// <param name="target">The target list.</param>
        public static void Shuffle(this IList target)
        {
            Random random = new Random();

            //Loop from top to bottom, replacing the current index with 
            //another random index within the remaining range.
            for (int x = target.Count - 1; x >= 0; x--)
            {
                int randomIndex = random.Next(0, x + 1);

                object temp = target[x];
                target[x] = target[randomIndex];
                target[randomIndex] = temp;
            }
        }

        /// <summary>
        /// Pad the beginning of the List with the given number of "padding elements."
        /// </summary>
        /// <param name="target">The target list.</param>
        /// <param name="padCount">The number of elements to add to the beginning of the list.</param>
        /// <param name="padValue">The value of the element to be added.</param>
        public static void PadLeft(this IList target, int padCount, object padValue)
        {
            for (int x = 0; x < padCount; x++)
            {
                target.Insert(0, padValue);
            }
        }

        /// <summary>
        /// Pad the end of the List with the given number of "padding elements."
        /// </summary>
        /// <param name="target">The target list.</param>
        /// <param name="padCount">The number of elements to add to the end of the list.</param>
        /// <param name="padValue">The value of the element to be added.</param>
        public static void PadRight(this IList target, int padCount, object padValue)
        {
            for (int x = 0; x < padCount; x++)
            {
                target.Add(padValue);
            }
        }

        #endregion

        #region IList{T}

        /// <summary>
        /// Performs an in-place Fisher-Yates shuffle on the current contents of the List.
        /// </summary>
        /// <param name="target">The target list.</param>
        public static void Shuffle<T>(this IList<T> target)
        {
            Random random = new Random();

            //Loop from top to bottom, replacing the current index with 
            //another random index within the remaining range.
            for (int x = target.Count - 1; x >= 0; x--)
            {
                int randomIndex = random.Next(0, x + 1);

                T temp = target[x];
                target[x] = target[randomIndex];
                target[randomIndex] = temp;
            }
        }

        /// <summary>
        /// Pad the beginning of the List with the given number of "padding elements."
        /// </summary>
        /// <typeparam name="T">The type of object contained in the list.</typeparam>
        /// <param name="target">The target list.</param>
        /// <param name="padCount">The number of elements to add to the beginning of the list.</param>
        /// <param name="padValue">The value of the element to be added.</param>
        public static void PadLeft<T>(this IList<T> target, int padCount, T padValue)
        {
            for (int x = 0; x < padCount; x++)
            {
                target.Insert(0, padValue);
            }
        }

        /// <summary>
        /// Pad the end of the List with the given number of "padding elements."
        /// </summary>
        /// <typeparam name="T">The type of object contained in the list.</typeparam>
        /// <param name="target">The target list.</param>
        /// <param name="padCount">The number of elements to add to the end of the list.</param>
        /// <param name="padValue">The value of the element to be added.</param>
        public static void PadRight<T>(this IList<T> target, int padCount, T padValue)
        {
            for (int x = 0; x < padCount; x++)
            {
                target.Add(padValue);
            }
        }

        #endregion
    }
}
