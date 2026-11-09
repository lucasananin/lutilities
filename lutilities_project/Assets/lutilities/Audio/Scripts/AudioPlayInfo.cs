using UnityEngine;

namespace lutilities.Audio
{
    public class AudioPlayInfo
    {
        public AudioSO so = null;
        public Transform parent = null;
        public Vector3 position = default;
    }

    public class AudioStopInfo
    {
        public AudioSO so = null;
        public AudioEmitter emitter = null;
    }
}
