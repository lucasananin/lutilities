using UnityEngine;
using System;

namespace lutilities.TagCollections
{
    [CreateAssetMenu(fileName = "TagCollection_", menuName = "Scriptable Objects/Tag Collection")]
    public class TagCollection : ScriptableObject
    {
        [SerializeField]
        private TagEntry[] _tags;

        public bool Contains(string tag)
        {
            return _tags != null && Array.Exists(_tags, t => t.Value == tag);
        }

        private void OnValidate()
        {
            if (_tags == null)
                return;

            foreach (var tag in _tags)
            {
                if (tag == null || string.IsNullOrEmpty(tag.Value))
                    tag.SetDefault();
            }
        }

        [Serializable]
        public class TagEntry
        {
            [SerializeField] private string _value = default;

            public string Value => _value;

            public void SetDefault()
            {
                _value = "Untagged";
            }
        }
    }
}
