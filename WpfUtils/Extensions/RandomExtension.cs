using System;
using System.Collections;
using System.Collections.Generic;

namespace WpfUtils.Extensions
{
    /// <summary>
    /// Contains extension methods for the Random class.
    /// </summary>
    public static class RandomExtension
    {
        /// <summary>
        /// Randomly generates a boolean value with a 50/50 coin toss.
        /// </summary>
        /// <param name="rand">The target Random object.</param>
        /// <returns>Returns a random true/false value (with 50/50 probability).</returns>
        public static bool NextBool(this Random rand)
        {
            return NextBool(rand, 0.5);
        }

        /// <summary>
        /// Randomly generates a boolean value, with the True result having the given probability.
        /// </summary>
        /// <param name="rand">The target Random object.</param>
        /// <param name="probability">The probability of a True value.  Must be between 0.0 and 1.0.</param>
        /// <returns>Returns a random true/false value (with given probability for a True result).</returns>
        public static bool NextBool(this Random rand, double probability)
        {
            if (probability > 1.0 || probability < 0.0)
            {
                throw new ArgumentException("probability must be between 0.0 and 1.0");
            }
            return rand.NextDouble() < probability;
        }

        /// <summary>
        /// Randomly choose one of the items from the list.
        /// </summary>
        /// <typeparam name="T">The type of item in the list.</typeparam>
        /// <param name="rand">The target Random object.</param>
        /// <param name="items">The list of items, from which to choose.</param>
        /// <returns>Returns one of the items in the list, chosen at random.</returns>
        public static T ChooseOne<T>(this Random rand, IList<T> items)
        {
            return items[rand.Next(items.Count)];
        }

        /// <summary>
        /// Randomly choose one of the items from the parameter list.
        /// </summary>
        /// <typeparam name="T">The type of item in the list.</typeparam>
        /// <param name="rand">The target Random object.</param>
        /// <param name="items">The list of items, from which to choose.</param>
        /// <returns>Returns one of the items in the list, chosen at random.</returns>
        public static T ChooseOne<T>(this Random rand, params T[] items)
        {
            return ChooseOne(rand, items as IList<T>);
        }

        /// <summary>
        /// Returns a shuffled copy of the given list of items, using the
        /// Fisher-Yates algorithm.
        /// </summary>
        /// <typeparam name="T">The type of the items in the list.</typeparam>
        /// <param name="rand">The target rand object.</param>
        /// <param name="items">The list of items for which to return a shuffled copy.</param>
        /// <returns>Returns a copy of the given list, with its contents shuffled.</returns>
        public static IList<T> Shuffle<T>(this Random rand, IList<T> items)
        {
            IList<T> tempList = new List<T>();
            foreach (T item in items)
            {
                tempList.Add(item);
            }

            tempList.Shuffle();
            return tempList;
        }
    }
}
