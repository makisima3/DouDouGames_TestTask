using System;
using System.Collections.Generic;
using System.Linq;
using Code.Core.StorageObjects;
using Code.UI;
using UnityEngine;

namespace Code.Core
{
    /// <summary>
    /// First Called Component With High Script Execution Priority
    /// </summary>
    public class SceneGameLoader : MonoBehaviour
    {
        [SerializeField] private TimerHolder timerHolder;
        [SerializeField] private TimerButtonHolder timerButtonHolder;

        private TimersStorageObject _timersStorageObject;

        private void OnApplicationPause(bool pauseStatus)
        {
            if(pauseStatus)
                PersistentStorage.PersistentStorage.Save<TimersStorageObject, List<TimersStorageObject.TimerState>>(_timersStorageObject);
        }

        private void OnApplicationQuit()
        {
            PersistentStorage.PersistentStorage.Save<TimersStorageObject, List<TimersStorageObject.TimerState>>(_timersStorageObject);
        }

        private void Awake()
        {
            _timersStorageObject = new TimersStorageObject(
                
                beforeSaving: () => timerHolder.Timers.Select(t => t.TimerState).ToList()
            );

            PersistentStorage.PersistentStorage.Load<TimersStorageObject, List<TimersStorageObject.TimerState>>(
                _timersStorageObject);

            
            
            timerHolder.Init();
            timerButtonHolder.Init();

            foreach (var timerState in _timersStorageObject.Data)
            {
                var timer = timerHolder.CreateTimer(timerState);
                timerButtonHolder.CreateButton(timer);
            }
        }
    }
}