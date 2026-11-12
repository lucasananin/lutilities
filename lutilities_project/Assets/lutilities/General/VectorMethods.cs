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
    }
}
