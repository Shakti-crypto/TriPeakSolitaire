namespace TriPeakSolitaire.UI
{
    using TMPro;
    using TriPeakSolitaire.Gameplay;
    using UnityEngine;

    public class GameTimerUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI timerText;
        [SerializeField] private GameTimer timerController;

        private void Update()
        {
            if (timerController.IsRunning) UpdateTimerText();
        }

        private void UpdateTimerText()
        {
            TimeData timeData= timerController.GetTime();
            timerText.text = $"{timeData.minutes:00}:{timeData.seconds:00}";
        }
    }
}
