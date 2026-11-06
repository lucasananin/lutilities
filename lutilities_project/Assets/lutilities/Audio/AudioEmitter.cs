using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

namespace lutilities.Audio
{
    public class AudioEmitter : MonoBehaviour
    {
        [SerializeField] AudioSource _audioSource = null;

        private AudioEmitterPool _pool = null;
        private AudioSO _so = null;
        private float _timeUntilReturn = 0;

        public event UnityAction OnReturnToPool = null;

        public AudioSource AudioSource { get => _audioSource; }

        private void Awake()
        {
            _pool = GetComponentInParent<AudioEmitterPool>();
        }

        public void SetInfo(AudioPlayInfo info)
        {
            _so = info.so;

            if (info.parent != null)
                transform.SetParent(info.parent);

            transform.position = info.position;
        }

        public void Play()
        {
            _so.ApplySettings(ref _audioSource);
            _audioSource.time = _so.Time;

            if (_so.HasDelay())
                _audioSource.PlayDelayed(_so.Delay);
            else
                _audioSource.Play();

            _timeUntilReturn = _so.GetClipLength();

            if (!_so.Loop)
            {
                StopAllCoroutines();
                StartCoroutine(ReturnRoutine());
            }
        }

        public void PlayMusic(float _fadeOutDuration)
        {
            if (_audioSource.isPlaying)
            {
                _audioSource.DOFade(0, _fadeOutDuration).OnComplete(() =>
                {
                    Play();
                });
            }
            else
            {
                Play();
            }
        }

        public void StopAndReturn()
        {
            StopAllCoroutines();
            _audioSource.Stop();
            _pool.Return(this);
            OnReturnToPool?.Invoke();
        }

        private IEnumerator ReturnRoutine()
        {
            yield return new WaitForSeconds(_timeUntilReturn);
            _pool.Return(this);
            OnReturnToPool?.Invoke();
        }
    }
}
