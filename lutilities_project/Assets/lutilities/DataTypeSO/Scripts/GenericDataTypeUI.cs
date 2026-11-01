using TMPro;
using UnityEngine;

namespace lutilities.DataTypeSO
{
    public class GenericDataTypeUI<T> : MonoBehaviour
    {
        [SerializeField] protected TextMeshProUGUI _text;
        [SerializeField] protected GenericDataTypeSO<T> _data;

        protected virtual void OnEnable()
        {
            if (_data == null) return;

            _data.OnChanged += UpdateText;
            UpdateText(_data.Get());
        }

        protected virtual void OnDisable()
        {
            if (_data == null) return;

            _data.OnChanged -= UpdateText;
        }

        protected virtual void UpdateText(T currentValue)
        {
            _text.text = currentValue?.ToString();
        }
    }
}
