using System;
using UnityEngine;

namespace Game.Event
{
    public class GameEvent : MonoBehaviour
    {
        public static event Action OnStartWave;
        public static event Action OnStopWave;

        public static void RaiseStartWave() => OnStartWave?.Invoke();
        public static void RaiseStopWave() => OnStopWave?.Invoke();
    }
}
