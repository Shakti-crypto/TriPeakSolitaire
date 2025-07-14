using TMPro;
using UnityEngine;

namespace TriPeakSolitaire.Gameplay
{
    public class GameTimer : MonoBehaviour
    {
        public float ElapsedTime { get; private set; }
        public bool IsRunning { get; private set; }

        public void Start()
        {
            ElapsedTime = 0f;
            IsRunning = true;
        }

        public void Stop() => IsRunning = false;
        public void Reset()
        {
            ElapsedTime = 0f;
        }

        private void Update()
        {
            if (IsRunning)
                ElapsedTime += Time.deltaTime;
        }

        public TimeData GetTime()
        {
            int minutes = Mathf.FloorToInt(ElapsedTime / 60f);
            int seconds = Mathf.FloorToInt(ElapsedTime % 60f);
            return new TimeData(minutes, seconds);
        }
    }
}

public readonly struct TimeData
{
    public readonly int minutes;
    public readonly int seconds;

    public TimeData(int _minutes, int _seconds)
    {
        minutes = _minutes;
        seconds = _seconds;
    }
}
