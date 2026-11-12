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

        [Serializable]
        public class TagEntry
        {
            [SerializeField] private string _value = "Untagged";

            public string Value => _value;
        }
    }
}
