using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

namespace lutilities.SceneManagement
{
    public class SceneLoader : MonoBehaviour
    {
#if UNITY_EDITOR
        [SerializeField] private SceneAsset _sceneToLoad = null;
        [SerializeField] private SceneAsset _sceneToUnload = null;
#endif
        [SerializeField] bool _loadOnStart = false;

        private string _pathToLoad = null;
        private string _pathToUnload = null;

        public static event UnityAction<string, string> OnLoadScene = null;

#if UNITY_EDITOR
        private void OnValidate()
        {
            // Keeps the string in sync whenever you change the asset
            _pathToLoad = _sceneToLoad != null ? AssetDatabase.GetAssetPath(_sceneToLoad) : string.Empty;
            _pathToUnload = _sceneToUnload != null ? AssetDatabase.GetAssetPath(_sceneToUnload) : string.Empty;
        }
#endif

        private IEnumerator Start()
        {
            yield return null;

            if (_loadOnStart)
            {
                Load();
            }
        }

        public virtual void Load()
        {
            OnLoadScene?.Invoke(_pathToLoad, _pathToUnload);
        }
    }
}
