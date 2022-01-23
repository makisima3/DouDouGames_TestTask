using System;
using Code.Core;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Code.UI
{
    public class TimerView : MonoBehaviour
    {
        [SerializeField] private TMP_Text timerTextPlace;
        [SerializeField] private HoldButton addTimeButton;
        [SerializeField] private HoldButton subTimeButton;
        [SerializeField] private Button playButton;
        [SerializeField] private Button pauseButton;
        [SerializeField] private Button stopButton;
        [SerializeField] private Button hideButton;

        private Timer _timer;
        private bool _isShowing;
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
        }

        private void Update()
        {
            if(!_isShowing)
                return;
            
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
            _isShowing = true;
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            _isShowing = false;
            gameObject.SetActive(false);
        }
    }
}