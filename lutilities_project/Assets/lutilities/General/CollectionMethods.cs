using System.Collections.Generic;
using System;
using UnityEngine;

namespace lutilities.General
{
    public static class CollectionMethods
    {
        /// <summary>
        /// Swaps two elements in a collection using their indexes.
        /// </summary>
        public static void Swap<T>(IList<T> collection, int indexA, int indexB)
        {
            if (collection == null)
                throw new ArgumentNullException(nameof(collection));

            if (indexA < 0 || indexA >= collection.Count)
                throw new ArgumentOutOfRangeException(nameof(indexA));

            if (indexB < 0 || indexB >= collection.Count)
                throw new ArgumentOutOfRangeException(nameof(indexB));

            (collection[indexA], collection[indexB]) =
                (collection[indexB], collection[indexA]);
        }

        /// <summary>
        /// Returns a random element from a collection.
        /// Throws an exception if the collection is null or empty.
        /// </summary>
        public static T GetRandom<T>(IList<T> collection)
        {
            if (collection == null)
                throw new ArgumentNullException(nameof(collection));

            if (collection.Count == 0)
                throw new InvalidOperationException("Collection is empty.");

            int randomIndex = UnityEngine.Random.Range(0, collection.Count);

            return collection[randomIndex];
        }

        /// <summary>
        /// Attempts to get a random element from a collection.
        /// Returns false if the collection is null or empty.
        /// </summary>
        public static bool TryGetRandom<T>(IList<T> collection, out T item)
        {
            if (collection == null || collection.Count == 0)
            {
                item = default;
                return false;
            }

            item = collection[UnityEngine.Random.Range(0, collection.Count)];
            return true;
        }

        /// <summary>
        /// Randomly shuffles the elements of a collection in place.
        /// Uses the Fisher-Yates shuffle algorithm.
        /// </summary>
        public static void Shuffle<T>(IList<T> collection)
        {
            if (collection == null)
                throw new ArgumentNullException(nameof(collection));

            for (int i = collection.Count - 1; i > 0; i--)
            {
                int randomIndex = UnityEngine.Random.Range(0, i + 1);

                Swap(collection, i, randomIndex);
            }
        }

        public static void SortByDistance<T>(List<T> list, Vector3 referencePosition, Func<T, Vector3> getPosition)
        {
            list.Sort((a, b) =>
            {
                float distanceA = (getPosition(a) - referencePosition).sqrMagnitude;
                float distanceB = (getPosition(b) - referencePosition).sqrMagnitude;

                return distanceA.CompareTo(distanceB);
            });
        }
    }
}
