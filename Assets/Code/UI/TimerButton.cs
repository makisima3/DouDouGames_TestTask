using Code.Core;
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

        private Timer _timer;
        private TimerView _timerView;
        private TimerHolder _timerHolder;
        private TimerButtonHolder _timerButtonHolder;

        public Timer Timer => _timer;

        public void Init(Timer timer, TimerView timerView, int place, TimerHolder timerHolder, TimerButtonHolder timerButtonHolder)
        {
            _timerHolder = timerHolder;
            _timerButtonHolder = timerButtonHolder;
            _timer = timer;
            timerTextPlace.text = _timer.TimerState.Name;
            _timerView = timerView;
            showButton.onClick.AddListener(OnTimerButtonClick);
            deleteButton.onClick.AddListener(OnDeleteButtonClick);

            gameObject.SetActive(true);
        }

        private void OnTimerButtonClick()
        {
            _timerView.Init(_timer);
            _timerView.Show();
        }

        private void OnDeleteButtonClick()
        {
            _timerHolder.DeleteTimer(_timer);
            _timerButtonHolder.DeleteTimerButton(this);
        }
    }
}