using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Code.Core;
using Code.Core.StorageObjects;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Code.UI
{
    public class TimerButtonHolder : MonoBehaviour
    {
        
        [SerializeField] private TimerButton timerButtonPrototype;
        [SerializeField] private TimerView timerView;
        [SerializeField] private TimerHolder timerHolder;
        [SerializeField] private Button CreateTimerButton;
        [SerializeField] private float appearDuration = 3f;
        [SerializeField] private float addingDuration = 0.3f;
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
            button.Init(timer, timerView, timerHolder, this);

            _timerButtons.Add(button);

            return button;
        }

        public void Show()
        {
            gameObject.SetActive(true);
            
            var buttonMoveDuration = appearDuration / _timerButtons.Count;
            var buttonShowDelay = 0f;
            var buttonShowDelayDelta = buttonMoveDuration / 2;
            
            Action onComplete = () =>
            {
                _timerButtons.ForEach(b => b.MakeInteractable(true));
                CreateTimerButton.interactable = true;
            };
            
            foreach (var button in _timerButtons)
            {
                button.Show(buttonShowDelay, buttonMoveDuration, button == _timerButtons.LastOrDefault() ? onComplete : null);
                buttonShowDelay += buttonShowDelayDelta;
            }
        }

        public void Hide(Action onComplete = null)
        {
            CreateTimerButton.interactable = false;
            var buttonMoveDuration = appearDuration / _timerButtons.Count;
            var buttonHideDelay = 0f;
            var buttonHideDelayDelta = buttonMoveDuration / 2;

            var reversedButtons = Enumerable.Reverse(_timerButtons).ToArray();
            var lastButton = reversedButtons.LastOrDefault();
            reversedButtons = reversedButtons.Take(reversedButtons.Length - 1).ToArray();

            foreach (var button in reversedButtons)
            {
                button.Hide(buttonHideDelay, buttonMoveDuration);
                buttonHideDelay += buttonHideDelayDelta;
            }

            if (lastButton == null)
                return;
            lastButton.Hide(buttonHideDelay, buttonMoveDuration, onComplete);
        }

        public void DeleteTimerButton(TimerButton button)
        {
            _timerButtons.Remove(button);
            Destroy(button.gameObject);
        }

        public void AddTimer()
        {
            var button = CreateButton(timerHolder.CreateTimer(new TimersStorageObject.TimerState()
            {
                Name = GetValidName(),
                Duration = TimeSpan.Zero,
                IsOnPause = true
            }));

            StartCoroutine(ShowDelay(button));
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

        //fix unity bugs...
        private IEnumerator ShowDelay(TimerButton button)
        {
            yield return new WaitForEndOfFrame();
            button.Show(0, addingDuration, () => button.MakeInteractable(true));
        }
    }
}