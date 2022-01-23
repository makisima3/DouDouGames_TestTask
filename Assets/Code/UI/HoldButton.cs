using System.Collections;
using Code.UI.Events;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Code.UI
{
    public class HoldButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler
    {
        [SerializeField] private float baseDelay = 1f;
        [SerializeField] private float delayDelta = 0.1f;
        [SerializeField] private HoldButtonTickEvent onButtonTick;

        private float _currentDelay;
        private Coroutine _holdTickCoroutine;

        public HoldButtonTickEvent OnButtonTick => onButtonTick;

        public void OnPointerDown(PointerEventData eventData)
        {
            _currentDelay = baseDelay;

            if (_holdTickCoroutine != null)
            {
                StopCoroutine(_holdTickCoroutine);
                _holdTickCoroutine = null;
            }
            
            _holdTickCoroutine = StartCoroutine(HoldTick());
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            StopTicking();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            StopTicking();
        }

        private IEnumerator HoldTick()
        {
            while (true)
            {
                onButtonTick.Invoke();

                yield return new WaitForSeconds(_currentDelay);
                
                _currentDelay -= delayDelta;
                _currentDelay = Mathf.Clamp(_currentDelay, 0, float.MaxValue);
            }
        }

        private void StopTicking()
        {
            if (_holdTickCoroutine != null)
            {
                StopCoroutine(_holdTickCoroutine);
                _holdTickCoroutine = null;
            }
        }
    }
}