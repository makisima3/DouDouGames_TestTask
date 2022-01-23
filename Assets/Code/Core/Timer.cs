using System;
using Code.Core.StorageObjects;
using UnityEngine;

namespace Code.Core
{
    public class Timer : MonoBehaviour
    {
        private TimersStorageObject.TimerState _timerState;

        public TimersStorageObject.TimerState TimerState => _timerState;

        public TimeSpan RemainingTime { get; private set; }
        
        public void Init(TimersStorageObject.TimerState timerState)
        {
            _timerState = timerState;
        }

        private void Update()
        {
            /*if (!_timerState.IsOnPause)
            {
                _timerState.Duraiton -= Time.deltaTime;
            }
            else
            {
                _timerState.StartTime = DateTime.Now;
            }

            RemainingTime = _timerState.StartTime.AddSeconds(_timerState.Duraiton) - _timerState.StartTime;*/
            
            if (_timerState.IsOnPause)
                _timerState.StartTime = DateTime.Now;
            
            RemainingTime = _timerState.StartTime.AddSeconds(_timerState.Duration.TotalSeconds) - DateTime.Now;

            if(RemainingTime <= TimeSpan.Zero)
               Stop();
        }

        public void Play()
        {
            _timerState.IsOnPause = false;
        }

        public void Pause()
        {
            _timerState.Duration -= _timerState.Duration - RemainingTime;    
            _timerState.IsOnPause = true;
        }

        public void Stop()
        {
            _timerState.Duration = TimeSpan.Zero;
            _timerState.IsOnPause = true;
        }

        public void AddSeconds(int seconds)
        {
            _timerState.Duration += new TimeSpan(0,0,seconds);
        }
    }
}