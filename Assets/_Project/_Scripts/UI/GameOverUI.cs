namespace TriPeakSolitaire.UI
{
    using System;
    using TriPeakSolitaire.Gameplay;
    using UnityEngine;
    using UnityEngine.SceneManagement;

    public class GameOverUI : MonoBehaviour
    {
        [SerializeField] private GameObject gameOverPanel;
        private void OnEnable()
        {
            GameManager.onGameOver += GameOver;
        }

        private void Start()
        {
            gameOverPanel.SetActive(false);
        }

        private void GameOver()
        {
            gameOverPanel.SetActive(true);
        }

        public void Retry()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        private void OnDisable()
        {
            GameManager.onGameOver -= GameOver;
        }
    }
}
