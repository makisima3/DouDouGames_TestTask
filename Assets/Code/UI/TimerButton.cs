using System;
using Code.Core;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Code.UI
{
    public class TimerButton : MonoBehaviour
    {
        [SerializeField] private TMP_Text timerTextPlace;
        [SerializeField] private Button showButton;
        [SerializeField] private Button deleteButton;
        [SerializeField] private RectTransform animationContainer;
        [SerializeField] private RectTransform shownPoint;
        [SerializeField] private RectTransform hiddenPoint;

        private Timer _timer;
        private TimerView _timerView;
        private TimerHolder _timerHolder;
        private TimerButtonHolder _timerButtonHolder;

        
        public Timer Timer => _timer;

        public void Init(Timer timer, TimerView timerView, TimerHolder timerHolder, TimerButtonHolder timerButtonHolder)
        {
            _timerHolder = timerHolder;
            _timerButtonHolder = timerButtonHolder;
            _timer = timer;
            timerTextPlace.text = _timer.TimerState.Name;
            _timerView = timerView;
            showButton.onClick.AddListener(OnTimerButtonClick);
            deleteButton.onClick.AddListener(OnDeleteButtonClick);
            MakeInteractable(false);
            
            animationContainer.position = hiddenPoint.position;
            gameObject.SetActive(true);
        }

        private void Update()
        {
            showButton.image.color = _timer.TimerState.IsOnPause ? Color.white : Color.grey;
        }

        public void Show(float moveDelay, float timeToMove, Action onComplete = null)
        {
            gameObject.SetActive(true);

            animationContainer.DOMove(shownPoint.position, timeToMove)
                .SetEase(Ease.OutBack)
                .SetDelay(moveDelay)
                .OnComplete(() =>
                {
                    onComplete?.Invoke();
                });
        }

        public void MakeInteractable(bool value)
        {
            showButton.interactable = value;
            deleteButton.interactable = value;
        }

        public void Hide(float moveDelay, float timeToMove, Action onComplete=null)
        {
            MakeInteractable(false);

            animationContainer.DOMove(hiddenPoint.position, timeToMove)
                .SetEase(Ease.InBack)
                .SetDelay(moveDelay)
                .OnComplete(() => onComplete?.Invoke());
        }

        private void OnTimerButtonClick()
        {
            _timerButtonHolder.Hide(() =>
            {
                _timerView.Init(_timer);
                _timerView.Show();
            });
        }

        private void OnDeleteButtonClick()
        {
            _timerHolder.DeleteTimer(_timer);
            _timerButtonHolder.DeleteTimerButton(this);
        }
    }
}