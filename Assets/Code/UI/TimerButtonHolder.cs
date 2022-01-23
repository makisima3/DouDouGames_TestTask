using System;
using System.Collections.Generic;
using System.Linq;
using Code.Core;
using Code.Core.StorageObjects;
using UnityEngine;
using UnityEngine.UI;

namespace Code.UI
{
    public class TimerButtonHolder : MonoBehaviour
    {
        [SerializeField] private TimerButton timerButtonPrototype;
        [SerializeField] private TimerView timerView;
        [SerializeField] private TimerHolder timerHolder;
        [SerializeField] private Button CreateTimerButton;
        private List<TimerButton> _timerButtons;

        public void Init()
        {
            _timerButtons = new List<TimerButton>();
            CreateTimerButton.onClick.AddListener(AddTimer);
        }

        public TimerButton CreateButton(Timer timer)
        {
            var button = Instantiate(timerButtonPrototype.gameObject, timerButtonPrototype.transform.parent)
                .GetComponent<TimerButton>();
            _timerButtons.Add(button);
            button.Init(timer, timerView, _timerButtons.Count, timerHolder, this);
            return button;
        }

        public void DeleteTimerButton(TimerButton button)
        {
            _timerButtons.Remove(button);
            Destroy(button.gameObject);
        }

        public void AddTimer()
        {
            CreateButton(timerHolder.CreateTimer(new TimersStorageObject.TimerState()
            {
                Name = GetValidName(),
                Duration = TimeSpan.Zero,
                IsOnPause = true
            }));
        }

        private string GetValidName()
        {
            var existNames = _timerButtons.Select(b => b.Timer.TimerState.Name).ToArray();

            for (int i = 0; i < int.MaxValue; i++)
            {
                var nameCandidate = $"Timer {i + 1}";
                if (!existNames.Contains(nameCandidate))
                    return nameCandidate;
            }

            return "Timer -1";
        }
    }
}