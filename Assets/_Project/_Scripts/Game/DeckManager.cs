namespace TriPeakSolitaire.Gameplay
{
    using PrimeTween;
    using System.Collections.Generic;
    using TriPeakSolitaire.Cards;
    using UnityEngine;


    /// <summary>
    /// Manages the deck and waste piles. Handles drawing cards, updating the visible waste card,
    /// and checking if the player can draw more cards. 
    /// </summary>
    public class DeckManager : MonoBehaviour
    {
        [SerializeField] private Transform deckPileTransform;
        [SerializeField] private Transform wastePileTransform;
        [SerializeField] private TweenSettings moveAnimationSettings;
        [SerializeField] private GameObject buyNewDeckButton;
        private Stack<CardController> deckPile = new Stack<CardController>();
        private Stack<CardController> wastePile = new Stack<CardController>();

        /// <summary>
        /// Player will be allowed to buy a new deck, only once per game.
        /// </summary>
        public bool deckBoughtOnce { get; private set; }
        private const int deckLegnth = 24;
        public int numberOfCardsInDeck
        {
            get
            {
                if (deckPile == null) return 0;
                else return deckPile.Count;
            }
        }

        /// <summary>
        /// Used to calculate next card's position on waste pile 
        /// </summary>
        public Vector3 wastePileCardPosition
        {
            get
            {
                Vector3 wastePileCardPosition = wastePileTransform.position;

                wastePileCardPosition.x += wastePile.Count * 0.01f;
                wastePileCardPosition.y += wastePile.Count * 0.01f;
                wastePileCardPosition.z = -wastePile.Count * 0.1f;
                return wastePileCardPosition;
            }
        }


        private void Start()
        {
            deckBoughtOnce = false;
            buyNewDeckButton.SetActive(false);
        }

        //Called from GameManager
        public void SetupDeckCards(List<CardController> deckCards)
        {
            deckPile = new Stack<CardController>(deckCards);
            wastePile = new Stack<CardController>();
            for (int i = 0; i < deckCards.Count; i++)
            {
                CardController cardController = deckCards[i];
                cardController.onCardClicked += OnCardClicked;

                CardView cardView = cardController.cardView;
                Vector3 cardPosition = deckPileTransform.position;
                cardPosition.x += i * 0.01f;
                cardPosition.y += i * 0.01f;
                cardPosition.z = -i * 0.1f;

                cardView.SetCardPosition(cardPosition);
            }

            MoveCardFromDeckToWastePile();
        }


        private void OnCardClicked(CardController card)
        {
            MoveCardFromDeckToWastePile();
            GameManager.Instance.IncreaseMoves();
        }

        public CardController GetCardInWastePile()
        {
            if (wastePile == null) return null;

            return wastePile.Peek();
        }

        public void MoveCardFromDeckToWastePile()
        {
            if (deckPile.Count == 0)
            {
                Debug.Log("No card left in deck pile");
                return;
            }

            GameManager.Instance.allowInput = false;
            CardController cardOnTop = deckPile.Pop();

            cardOnTop.onCardClicked -= OnCardClicked;
            cardOnTop.onCardClicked = null;

            Tween.Position(cardOnTop.cardView.transform, wastePileCardPosition, moveAnimationSettings).OnComplete(() =>
            {
                GameManager.Instance.allowInput = true;
                wastePile.Push(cardOnTop);
                cardOnTop.SetCardFaceUp();
                GameManager.Instance.EvaluateGameState();

                CheckBuyNewDeckPileButtonStatus();

            });
        }

        //Only adds the card to the stack, the animation and card's movement is handled by the GameManager
        public void AddCardToWastePileFromPyramid(CardController card)
        {
            wastePile.Push(card);
        }

        public void SetupNewDeckCards(List<CardController> deckCards)
        {
            deckPile = new Stack<CardController>(deckCards);
            for (int i = 0; i < deckCards.Count; i++)
            {
                CardController cardController = deckCards[i];
                cardController.onCardClicked += OnCardClicked;

                CardView cardView = cardController.cardView;
                Vector3 cardPosition = deckPileTransform.position;
                cardPosition.x += i * 0.01f;
                cardPosition.y += i * 0.01f;
                cardPosition.z = -i * 0.1f;

                cardView.SetCardPosition(cardPosition);
            }
        }

        public void BuyNewDeck()
        {
            deckBoughtOnce = true;
            buyNewDeckButton.SetActive(false);
            GameManager.Instance.StopGameStateEvaluationCoroutine();

            List<CardController> newDeck = GameManager.Instance.GetNewDeck(deckLegnth);
            SetupNewDeckCards(newDeck);
        }

        private void CheckBuyNewDeckPileButtonStatus()
        {

            if (!deckBoughtOnce && deckPile.Count == 0 && !buyNewDeckButton.activeInHierarchy)
            {
                buyNewDeckButton.SetActive(true);
            }
            else
            {
                buyNewDeckButton.SetActive(false);
            }
        }

        public void DisableBuyNewDeckButton()
        {
            buyNewDeckButton.SetActive(false);
        }

        private void OnDisable()
        {
            foreach (CardController card in deckPile)
            {
                card.onCardClicked -= OnCardClicked;
            }
        }
    }

}