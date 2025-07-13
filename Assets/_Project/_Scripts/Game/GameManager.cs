namespace TriPeakSolitaire.Gameplay
{
    using MEC;
    using PrimeTween;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using TriPeakSolitaire.Cards;
    using UnityEngine;

    public class GameManager : Singleton<GameManager>
    {
        [SerializeField] private CardSpawner cardSpawner;
        [SerializeField] private PyramidCardsManager pyramidManager;
        [SerializeField] private DeckManager deckManager;
        [SerializeField] private TweenSettings cardMovementAnimationSettings;
        private CardController[] cardControllers;
        private List<CardController> pyramidCards;
        private List<CardController> deckCardPile;

        private void Start()
        {
            cardControllers = cardSpawner.SpawnCards();
            CardShuffler.Shuffle(cardControllers);

            pyramidCards = new List<CardController>();
            for(int i = 0; i < 28; i++)
            {
                pyramidCards.Add(cardControllers[i]);
                if (i > 17) //last 10 cards at the pyramid bottom
                {
                    cardControllers[i].SetCardFaceUp();
                }
            }

            deckCardPile = new List<CardController>();
            for(int i = 28; i < 52; i++)
            {
                deckCardPile.Add(cardControllers[i]);
            }

            pyramidManager.SetupPyramid(pyramidCards);
            deckManager.SetupDeckCards(deckCardPile);
        }

        public void CheckCardFromPyramidToWaste(CardController pyramidCard)
        {
            CardController wastePileCard = deckManager.GetCardInWastePile();
            int pyramidCardValue = pyramidCard.cardValue;
            int wasteCardValue = wastePileCard.cardValue;

            if(AreAdjacent(wasteCardValue, pyramidCardValue))
            {
                MoveCardFromPyramidToWastePile(pyramidCard);
                Timing.RunCoroutine(pyramidManager.WaitToUpdateOtherCards(pyramidCard).CancelWith(gameObject));
            }
        }

        private void MoveCardFromPyramidToWastePile(CardController pyramidCard)
        {
            deckManager.AddCardToWastePileFromPyramid(pyramidCard);
            Tween.Position(pyramidCard.cardView.transform, deckManager.wastePilePosition, cardMovementAnimationSettings).OnComplete(() => {
                EvaluateGameState();
                pyramidCard.onCardClicked = null;
            });
        }

        public bool AreAdjacent(int value1, int value2)
        {
            int diff = Mathf.Abs(value1 - value2);

            // Direct neighbor OR Ace–King wraparound
            return diff == 1 || (value1 == 1 && value2 == 13) || (value1 == 13 && value2 == 1);
        }

        public void EvaluateGameState()
        {
            Timing.RunCoroutine(WaitToCheckGameState().CancelWith(gameObject));
        }

        private IEnumerator<float> WaitToCheckGameState()
        {
            yield return Timing.WaitForSeconds(0.5f);
            CheckForWin();
            CheckForLoss();
        }

        private void CheckForWin()
        {
            if(pyramidManager.numberOfCardsInPyramid == 0)
            {
                Debug.Log("Game Won!!!");
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


            Debug.Log("Game Over!!!");
        }

    }
}
