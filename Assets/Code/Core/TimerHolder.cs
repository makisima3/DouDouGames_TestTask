using System;
using System.Collections.Generic;
using Code.Core.StorageObjects;
using UnityEngine;

namespace Code.Core
{
  
    public class TimerHolder : MonoBehaviour
    {
        private List<Timer> _timers;

        public Timer[] Timers => _timers.ToArray();

        public void Init()
        {
            _timers = new List<Timer>();
        }
        
        public Timer CreateTimer(TimersStorageObject.TimerState time)
        {
            var timer = gameObject.AddComponent<Timer>();
            timer.Init(time);
            _timers.Add(timer);
            
            return timer;
        }

        public void DeleteTimer(Timer timer)
        {
            timer.Stop();

            _timers.Remove(timer);
            Destroy(timer);
        }
    }
}