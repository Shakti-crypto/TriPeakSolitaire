namespace TriPeakSolitaire.UI
{
    using System;
    using TriPeakSolitaire.Gameplay;
    using UnityEngine;
    using UnityEngine.SceneManagement;

    public class GameOverUI : MonoBehaviour
    {
        [SerializeField] private DeckManager deckManager;
        [SerializeField] private GameObject gameOverPanel;
        [SerializeField] private GameObject buyNewDeckButton;
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
            deckManager.DisableBuyNewDeckButton();
            buyNewDeckButton.SetActive(!deckManager.deckBoughtOnce);
            gameOverPanel.SetActive(true);
        }

        public void Retry()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        public void BuyNewDeck()
        {
            GameManager.Instance.RestartGameWithNewDeck();
            gameOverPanel.SetActive(false);
        }

        private void OnDisable()
        {
            GameManager.onGameOver -= GameOver;
        }
    }
}
