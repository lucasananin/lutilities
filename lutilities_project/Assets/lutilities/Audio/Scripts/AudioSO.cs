using UnityEngine;
using UnityEngine.Audio;

namespace lutilities.Audio
{
    [CreateAssetMenu(fileName = "Audio_", menuName = "Scriptable Objects/Audio/Audio Data")]
    public class AudioSO : ScriptableObject
    {
        [Header("// DEFAULT SETTINGS")]
        [SerializeField] AudioClip _clip = null;
        [SerializeField] AudioMixerGroup _group = null;
        [Space]
        [SerializeField] bool _mute = false;
        [SerializeField] bool _bypassEffect = false;
        [SerializeField] bool _bypassListenerEffects = false;
        [SerializeField] bool _bypassReverbZones = false;
        [SerializeField] bool _loop = false;
        [Space]
        [SerializeField] PriorityLevel _priority = PriorityLevel.Standard;
        [SerializeField, Range(0, 1)] float _volume = 1;
        [SerializeField, Range(-3, 3)] float _minPitch = 1;
        [SerializeField, Range(-3, 3)] float _maxPitch = 1;
        [SerializeField, Range(-1, 1)] float _stereoPan = 0;
        [SerializeField, Range(0, 1)] float _spatialBlend = 0;
        [SerializeField, Range(0, 1.1f)] float _reverbZoneMix = 1;

        [Header("// 3D SETTINGS")]
        [SerializeField, Range(0, 5)] float _dopplerLevel = 1;
        [SerializeField, Range(0, 360)] float _spread = 0;
        [SerializeField] AudioRolloffMode _volumeRolloff = AudioRolloffMode.Logarithmic;
        [SerializeField, Range(0, 360)] float _minDistance = 1;
        [SerializeField, Range(0, 360)] float _maxDistance = 500;

        [Header("// CUSTOM SETTINGS")]
        [SerializeField] bool _isMusic = false;
        [SerializeField, Range(0f, 10f)] float _time = 0f;
        [SerializeField, Range(0f, 10f)] float _delay = 0f;

        public static event AudioPlayAction OnPlay = null;
        public static event AudioStopAction OnStop = null;

        const float LENGTH_OFFSET = 0.2f;

        public int Priority { get { return (int)_priority; } set { _priority = (PriorityLevel)value; } }
        public bool Loop { get => _loop; set => _loop = value; }
        public bool IsMusic { get => _isMusic; set => _isMusic = value; }
        public float Time { get => _time; set => _time = value; }
        public float Delay { get => _delay; set => _delay = value; }

        public void ApplySettings(ref AudioSource _source)
        {
            _source.clip = _clip;
            _source.outputAudioMixerGroup = _group;

            _source.mute = _mute;
            _source.bypassEffects = _bypassEffect;
            _source.bypassListenerEffects = _bypassListenerEffects;
            _source.bypassReverbZones = _bypassReverbZones;
            _source.loop = _loop;

            _source.priority = Priority;
            _source.volume = _volume;
            _source.pitch = Random.Range(_minPitch, _maxPitch);
            _source.panStereo = _stereoPan;
            _source.spatialBlend = _spatialBlend;
            _source.reverbZoneMix = _reverbZoneMix;

            _source.dopplerLevel = _dopplerLevel;
            _source.spread = _spread;
            _source.rolloffMode = _volumeRolloff;
            _source.minDistance = _minDistance;
            _source.maxDistance = _maxDistance;

            _source.time = _time;
        }

        public float GetClipLength()
        {
            return _clip.length + _delay + LENGTH_OFFSET;
        }

        public bool HasDelay()
        {
            return _delay > 0;
        }

        public AudioEmitter Play(Transform _parent = null, Vector3 _position = default)
        {
            AudioPlayInfo _info = new() { so = this, position = _position, parent = _parent };
            return OnPlay?.Invoke(_info);
        }

        public bool Stop(AudioEmitter _emitter)
        {
            AudioStopInfo _info = new() { so = this, emitter = _emitter };
            return OnStop.Invoke(_info);
        }
    }

    public enum PriorityLevel
    {
        Highest = 0,
        High = 64,
        Standard = 128,
        Low = 194,
        VeryLow = 256,
    }

    public delegate AudioEmitter AudioPlayAction(AudioPlayInfo info);
    public delegate bool AudioStopAction(AudioStopInfo info);
}
