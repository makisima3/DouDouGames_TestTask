using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using PersistentStorage;
using UnityEngine;

namespace Code.Core.StorageObjects
{
    public class TimersStorageObject : PlainStorageObject<List<TimersStorageObject.TimerState>>
    {
        [Serializable]
        public class TimerState
        {
            public string Name { get; set; }
            public TimeSpan Duration { get; set; }
            public DateTime StartTime { get; set; }
            public bool IsOnPause { get; set; }
        }

        public override string PrefKey => nameof(TimersStorageObject);


        public TimersStorageObject(Action<List<TimerState>> afterLoading = null,
            Func<List<TimerState>> beforeSaving = null) : base(
            new List<TimersStorageObject.TimerState>()
            {
                new TimerState()
                {
                    Name = "Timer 1",
                    Duration = TimeSpan.Zero,
                    IsOnPause = true
                },
                new TimerState()
                {
                    Name = "Timer 2",
                    Duration = TimeSpan.Zero,
                    IsOnPause = true
                },
                new TimerState()
                {
                    Name = "Timer 3",
                    Duration = TimeSpan.Zero,
                    IsOnPause = true
                }
            },
            afterLoading,
            beforeSaving)
        {
        }
    }
}