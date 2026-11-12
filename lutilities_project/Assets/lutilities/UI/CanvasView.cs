using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

namespace lutilities.UI
{
    [RequireComponent(typeof(CanvasGroup), typeof(Canvas))]
    public class CanvasView : MonoBehaviour
    {
        [SerializeField] CanvasGroup _canvasGroup = null;
        [SerializeField] float _fadeDuration = 0.3f;
        [SerializeField] float _delay = 0f;
        [SerializeField] bool _hideOnAwake = true;
        [SerializeField] bool _showOnStart = false;
        [Space]
        [SerializeField] UnityEvent OnShow_Event = null;
        [SerializeField] UnityEvent OnHide_Event = null;

        public event UnityAction OnShow = null;
        public event UnityAction OnHide = null;

        public static UnityAction<CanvasView> OnAnyShow = null;
        public static UnityAction<CanvasView> OnAnyHide = null;

        private void Awake()
        {
            if (_hideOnAwake)
                InstantHide();
        }

        private void Start()
        {
            if (_showOnStart)
                Show();
        }

        private void OnValidate()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
        }

        public void Show()
        {
            if (_delay > 0)
                StartCoroutine(Show_Routine());
            else
                ShowView();
        }

        private void ShowView()
        {
            if (_fadeDuration > 0)
            {
                _canvasGroup.DOComplete();
                _canvasGroup.DOFade(1, _fadeDuration);
            }
            else
            {
                _canvasGroup.alpha = 1;
            }

            _canvasGroup.interactable = true;
            _canvasGroup.blocksRaycasts = true;
            OnShow?.Invoke();
            OnAnyShow?.Invoke(this);
        }

        public void Hide()
        {
            if (_fadeDuration > 0)
            {
                _canvasGroup.DOComplete();
                _canvasGroup.DOFade(0, _fadeDuration);
            }
            else
            {
                _canvasGroup.alpha = 0;
            }

            _canvasGroup.interactable = false;
            _canvasGroup.blocksRaycasts = false;
            OnHide?.Invoke();
            OnAnyHide?.Invoke(this);
        }

        public void InstantShow()
        {
            _canvasGroup.DOComplete();
            _canvasGroup.alpha = 1;
            _canvasGroup.interactable = true;
            _canvasGroup.blocksRaycasts = true;
            OnShow?.Invoke();
            OnAnyShow?.Invoke(this);
        }

        public void InstantHide()
        {
            _canvasGroup.DOComplete();
            _canvasGroup.alpha = 0;
            _canvasGroup.interactable = false;
            _canvasGroup.blocksRaycasts = false;
            OnHide?.Invoke();
            OnAnyHide?.Invoke(this);
        }

        public void ShowAndHide()
        {
            _canvasGroup.DOComplete();
            _canvasGroup.alpha = 1;
            _canvasGroup.DOFade(0, _fadeDuration);
            _canvasGroup.interactable = false;
            _canvasGroup.blocksRaycasts = false;
        }

        public bool IsVisible()
        {
            return _canvasGroup.alpha > 0;
        }

        private IEnumerator Show_Routine()
        {
            yield return new WaitForSeconds(_delay);
            ShowView();
        }
    }
}
