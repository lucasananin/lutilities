using System.Collections.Generic;
using UnityEngine;

namespace lutilities.General
{
    public static class TransformMethods
    {
        /// <summary>
        /// Returns the component with the nearest Transform to a reference Transform.
        /// Returns null if the collection is null or empty.
        /// </summary>
        public static T GetNearest<T>(IList<T> collection, Vector3 referencePosition) where T : Component
        {
            if (collection == null) return null;

            T nearest = null;
            float minDistance = float.PositiveInfinity;

            for (int i = 0; i < collection.Count; i++)
            {
                T item = collection[i];

                if (item == null)
                    continue;

                float sqrDistance = (item.transform.position - referencePosition).sqrMagnitude;

                if (sqrDistance < minDistance)
                {
                    minDistance = sqrDistance;
                    nearest = item;
                }
            }

            return nearest;
        }

        /// <summary>
        /// Checks whether a position is within a specified distance of a target Transform.
        /// </summary>
        public static bool IsWithinDistance(Vector3 position, Transform target, float distance)
        {
            if (target == null || distance < 0f) return false;

            float sqrDistance = (position - target.position).sqrMagnitude;
            return sqrDistance <= distance * distance;
        }

        /// <summary>
        /// Checks whether a position is within a specified distance of a target position.
        /// </summary>
        public static bool IsWithinDistance(Vector3 position, Vector3 target, float distance)
        {
            if (target == null || distance < 0f) return false;

            float sqrDistance = (position - target).sqrMagnitude;
            return sqrDistance <= distance * distance;
        }
    }
}
