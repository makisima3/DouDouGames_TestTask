using System;
using Code.Core;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Code.UI
{
    public class TimerView : MonoBehaviour
    {
        
        [SerializeField] private TimerButtonHolder buttonHolder;
        [SerializeField] private TMP_Text timerTextPlace;
        [SerializeField] private HoldButton addTimeButton;
        [SerializeField] private HoldButton subTimeButton;
        [SerializeField] private Button playButton;
        [SerializeField] private Button pauseButton;
        [SerializeField] private Button stopButton;
        [SerializeField] private Button hideButton;

        [SerializeField] private float moveDuration = 0.5f;
        [SerializeField] private RectTransform targetContainer;
        [SerializeField] private RectTransform shownPoint;
        [SerializeField] private RectTransform hiddenPoint;

        private Timer _timer;
        private void Awake()
        {
            playButton.gameObject.SetActive(true);
            pauseButton.gameObject.SetActive(false);

            addTimeButton.OnButtonTick.AddListener(() => _timer.AddSeconds(1));
            subTimeButton.OnButtonTick.AddListener(() => _timer.AddSeconds(-1));
            playButton.onClick.AddListener(() =>
            {
                _timer.Play();
                playButton.gameObject.SetActive(false);
                pauseButton.gameObject.SetActive(true);
            });
            stopButton.onClick.AddListener(() =>
            {
                _timer.Stop();
                playButton.gameObject.SetActive(true);
                pauseButton.gameObject.SetActive(false);
            });
            pauseButton.onClick.AddListener(() =>
            {
                _timer.Pause();
                playButton.gameObject.SetActive(true);
                pauseButton.gameObject.SetActive(false);
            });
            hideButton.onClick.AddListener(Hide);
            
            gameObject.SetActive(false);
            targetContainer.position = hiddenPoint.position;
        }

        private void Update()
        {
            pauseButton.gameObject.SetActive(!_timer.TimerState.IsOnPause);
            playButton.gameObject.SetActive(_timer.TimerState.IsOnPause);
           timerTextPlace.text = _timer.RemainingTime.ToString(@"hh\:mm\:ss");
           
        }

        public void Init(Timer timer)
        {
            _timer = timer;
            pauseButton.gameObject.SetActive(!timer.TimerState.IsOnPause);
            playButton.gameObject.SetActive(timer.TimerState.IsOnPause);
        }

        public void Show()
        {
            gameObject.SetActive(true);
            
            targetContainer.DOMove(shownPoint.position, moveDuration)
                .SetEase(Ease.OutBack);
        }

        public void Hide()
        {
            targetContainer.DOMove(hiddenPoint.position, moveDuration)
                .SetEase(Ease.InBack)
                .OnComplete(() =>
                {
                    gameObject.SetActive(false);
                    buttonHolder.Show();
                });
            
            
        }
    }
}