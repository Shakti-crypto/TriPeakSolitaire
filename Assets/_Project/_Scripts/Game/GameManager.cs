
namespace TriPeakSolitaire.Gameplay
{
    using MEC;
    using PrimeTween;
    using System;
    using System.Collections.Generic;
    using TriPeakSolitaire.Cards;
    using UnityEngine;

    /// <summary>
    /// Controls the overall game flow, including game start, win/loss detection,
    /// and deck rebuy functionality. Coordinates interactions between major systems
    /// like the pyramid, deck manager, and UI.
    /// </summary>
    public class GameManager : Singleton<GameManager>
    {
        #region Variables
        [SerializeField] private CardSpawner cardSpawner;
        [SerializeField] private PyramidCardsManager pyramidManager;
        [SerializeField] private DeckManager deckManager;
        [SerializeField] private GameTimer gameTimer;
        [SerializeField] private TweenSettings cardMovementAnimationSettings;
        private CoroutineHandle gameStateEvaluationCoroutine;
        public int MovesMade { get; private set; }
        public bool allowInput;
        #endregion

        #region Events
        public static Action<int> onMoveMade;
        public static Action<TimeData, int> onGameWon;
        public static Action onGameOver;
        #endregion

        private void Start()
        {

            CardController[] cardControllers = cardSpawner.SpawnCards();
            CardShuffler.Shuffle(cardControllers);

            List<CardController> pyramidCards = new List<CardController>();
            for (int i = 0; i < 28; i++)
            {
                pyramidCards.Add(cardControllers[i]);
                if (i > 17) //last 10 cards at the pyramid bottom
                {
                    cardControllers[i].SetCardFaceUp();
                }
            }

            List<CardController> deckCardPile = new List<CardController>();
            for (int i = 28; i < 52; i++)
            {
                deckCardPile.Add(cardControllers[i]);
            }

            pyramidManager.SetupPyramid(pyramidCards);
            deckManager.SetupDeckCards(deckCardPile);
            gameTimer.Start();
        }

        /// <summary>
        /// Checks whether the clicked card can move to waste pile,
        /// and moves it there if valid.
        /// </summary>
        public void CheckCardFromPyramidToWaste(CardController pyramidCard)
        {
            CardController wastePileCard = deckManager.GetCardInWastePile();
            int pyramidCardValue = pyramidCard.cardValue;
            int wasteCardValue = wastePileCard.cardValue;

            if (AreAdjacent(wasteCardValue, pyramidCardValue))
            {
                MoveCardFromPyramidToWastePile(pyramidCard);
                Timing.RunCoroutine(pyramidManager.WaitToUpdateOtherCards(pyramidCard).CancelWith(gameObject));
            }
        }

        private void MoveCardFromPyramidToWastePile(CardController pyramidCard)
        {
            allowInput = false;
            deckManager.AddCardToWastePileFromPyramid(pyramidCard);
            IncreaseMoves();
            Tween.Position(pyramidCard.cardView.transform, deckManager.wastePileCardPosition, cardMovementAnimationSettings).OnComplete(() =>
            {
                allowInput = true;
                EvaluateGameState();
                pyramidCard.onCardClicked = null;
            });
        }


        /// <summary>
        /// Checks if cards values are next to each other (2-3,Q-J),
        /// Ace wraps around both 2 and King cards
        /// </summary>
        public bool AreAdjacent(int value1, int value2)
        {
            int diff = Mathf.Abs(value1 - value2);

            // Direct neighbor OR Ace–King wraparound
            return diff == 1 || (value1 == 1 && value2 == 13) || (value1 == 13 && value2 == 1);
        }

        public void IncreaseMoves()
        {
            MovesMade++;
            onMoveMade?.Invoke(MovesMade);
        }

        public void EvaluateGameState()
        {
            gameStateEvaluationCoroutine=Timing.RunCoroutine(WaitToEvaluateStatus().CancelWith(gameObject));
        }

        /// <summary>
        /// Waiting before evaluating the game results, to ensure all the 
        /// animations have been played, and player has some time to check game status
        /// before the results are announced
        /// </summary>
        private IEnumerator<float> WaitToEvaluateStatus()
        {
            yield return Timing.WaitForSeconds(2f);
            CheckForWin();
            CheckForLoss();
        }

        /// <summary>
        /// This function stops the evaluating game status coroutine.
        /// It is called when a player buys a new deck, to make sure that the 
        /// game does not prematurely ends when a new deck is bought.
        /// </summary>
        public void StopGameStateEvaluationCoroutine()
        {
            Timing.KillCoroutines(gameStateEvaluationCoroutine);
        }

        public List<CardController> GetNewDeck(int deckLength)
        {
            return cardSpawner.SpawnNewDeck(deckLength);
        }

        /// <summary>
        /// Called from GameOverUI, it continues the game from where it ended
        /// when a new deck is bought in the game over screen.
        /// The timer is resumed, and the moves carry on.
        /// </summary>
        public void RestartGameWithNewDeck()
        {
            deckManager.BuyNewDeck();
            gameTimer.Start();
        }

        private void CheckForWin()
        {
            if (pyramidManager.numberOfCardsInPyramid == 0)
            {
                gameTimer.Stop();
                onGameWon?.Invoke(gameTimer.GetTime(), MovesMade);
            }
        }

        private void CheckForLoss()
        {
            if (deckManager.numberOfCardsInDeck > 0) return;

            List<CardController> faceUpCards = pyramidManager.GetFaceUpCardsInPyramid();
            CardController wastePileCard = deckManager.GetCardInWastePile();

            foreach (CardController pyramidCard in faceUpCards)
            {
                if (AreAdjacent(pyramidCard.cardValue, wastePileCard.cardValue)) return;
            }

            gameTimer.Stop();
            onGameOver?.Invoke();
        }

    }
}
