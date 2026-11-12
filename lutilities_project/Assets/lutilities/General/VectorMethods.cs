using UnityEngine;

namespace lutilities.General
{
    public class VectorMethods
    {
        public static float InverseLerp(Vector3 _a, Vector3 _b, Vector3 _value)
        {
            Vector3 AB = _b - _a;
            Vector3 AV = _value - _a;
            return Vector3.Dot(AV, AB) / Vector3.Dot(AB, AB);
        }

        public static Vector3 GetRandomPointInBounds(Bounds _bounds)
        {
            return new Vector3(
                Random.Range(_bounds.min.x, _bounds.max.x),
                Random.Range(_bounds.min.y, _bounds.max.y),
                Random.Range(_bounds.min.z, _bounds.max.z)
            );
        }

        public static Vector2 GetRandomInCircle(float _min, float _max)
        {
            return Random.insideUnitCircle.normalized * Random.Range(_min, _max);
        }

        public static Vector3 GetRandomInSphere(float _min, float _max)
        {
            return Random.insideUnitSphere.normalized * Random.Range(_min, _max);
        }

        public static Vector2 GetRandomDirection2D()
        {
            return Random.insideUnitCircle.normalized;
        }

        public static Vector3 GetRandomDirection()
        {
            return Random.insideUnitSphere.normalized;
        }
    }
}
