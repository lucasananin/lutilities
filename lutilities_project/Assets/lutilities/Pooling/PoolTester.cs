using System.Collections;
using UnityEngine;

namespace lutilities.Pooling
{
    public class PoolTester : MonoBehaviour
    {
        [SerializeField] TransformPoolSO _poolSO = null;
        [SerializeField] TransformPool _poolBehaviour = null;
        [SerializeField] bool _useSO = true;

        private IEnumerator Start()
        {
            while (true)
            {
                for (int i = 0; i < Random.Range(1, 5); i++)
                {
                    var _instance = _useSO ? _poolSO.Get() : _poolBehaviour.Get();
                    _instance.SetParent(transform);
                    _instance.SetPositionAndRotation(new Vector3(Random.Range(-5f, 5f), Random.Range(-5f, 5f), 0f), Random.rotationUniform);
                    yield return new WaitForSeconds(1);
                }

                yield return new WaitForSeconds(3f);

                if (_useSO)
                    _poolSO.ReturnAll();
                else
                    _poolBehaviour.ReturnAll();

                yield return new WaitForSeconds(3f);
            }
        }
    }
}
