namespace TriPeakSolitaire.UI
{
    using TMPro;
    using TriPeakSolitaire.Gameplay;
    using UnityEngine;
    using UnityEngine.SceneManagement;

    public class GameWonUI : MonoBehaviour
    {
        [SerializeField] private GameObject gameWonPanel;
        [SerializeField] private TextMeshProUGUI timerText;
        [SerializeField] private TextMeshProUGUI movesText;
        private void OnEnable()
        {
            GameManager.onGameWon += GameWon;
        }

        private void Start()
        {
            gameWonPanel.SetActive(false);
        }

        private void GameWon(TimeData timeData, int movesMade)
        {
            timerText.text = $"{timeData.minutes:00}:{timeData.seconds:00}";
            movesText.text = $"{movesMade:00}";
            gameWonPanel.SetActive(true);
        }

        public void Replay()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        private void OnDisable()
        {
            GameManager.onGameWon -= GameWon;
        }
    }
}