using UnityEngine;

namespace lutilities.Audio
{
    public class AudioPlayer : MonoBehaviour
    {
        [SerializeField] AudioSO _so = null;
        [SerializeField] bool _playOnEnable = false;
        [SerializeField] bool _stopOnDisable = false;
        [SerializeField] bool _overrideParent = false;

        private AudioEmitter _emitter = null;

        private void OnEnable()
        {
            if (_playOnEnable)
            {
                Play();
            }
        }

        private void OnDisable()
        {
            if (_stopOnDisable && _emitter != null)
            {
                _so.Stop(_emitter);
            }
        }

        public void Play(Transform _parent = null)
        {
            _emitter = _so.Play(_overrideParent ? transform : _parent, transform.position);
            _emitter.OnReturnToPool += ClearReference;
        }

        public void Stop()
        {
            _so.Stop(_emitter);
        }

        private void ClearReference()
        {
            _emitter = null;
        }
    }
}
