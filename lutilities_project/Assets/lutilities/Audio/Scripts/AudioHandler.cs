using DG.Tweening;
using UnityEngine;

namespace lutilities.Audio
{
    public class AudioHandler : MonoBehaviour
    {
        [SerializeField] AudioEmitterPool _pool = null;
        [SerializeField] AudioEmitter _musicEmitter = null;
        [SerializeField] float _musicFadeOutDuration = 1f;

        private void OnEnable()
        {
            AudioSO.OnPlay += PlayAudio;
            AudioSO.OnStop += StopAudio;
        }

        private void OnDisable()
        {
            AudioSO.OnPlay -= PlayAudio;
            AudioSO.OnStop -= StopAudio;
        }

        private AudioEmitter PlayAudio(AudioPlayInfo info)
        {
            var _emitter = info.so.IsMusic ? _musicEmitter : _pool.Get();
            _emitter.SetInfo(info);

            if (info.so.IsMusic && _emitter.AudioSource.isPlaying)
            {
                _emitter.AudioSource.DOFade(0f, _musicFadeOutDuration).OnComplete(() =>
                {
                    _emitter.Play();
                });
            }
            else
            {
                _emitter.Play();
            }

            return _emitter;
        }

        private bool StopAudio(AudioStopInfo info)
        {
            if (info.emitter != null)
            {
                info.emitter.StopAndReturn();
                return true;
            }

            return false;
        }
    }
}
