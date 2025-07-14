namespace TriPeakSolitaire.Cards
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Acts as the Controller in the MVC architecture for a card.
    /// Connects the CardModel (data) and CardView (UI), listens for model updates,
    /// and modifies the view accordingly. Handles card interaction logic.
    /// </summary>
    public class CardController
    {
        public CardModel cardModel;
        public CardView cardView;

        /// <summary>
        /// This list stores the cards blocked by the current card.
        /// The cards above the current card will be updated through this list,
        /// when the current card is moved to the waste pile
        /// </summary>
        private List<CardController> cardsThisBlocks;

        /// <summary>
        /// Store the event based on the card position.
        /// Pyramid cards and deck cards will have different events, called 
        /// by their respective managers
        /// </summary>
        public Action<CardController> onCardClicked;
        public int cardValue => cardModel.cardData.value;

        public CardController(CardModel _cardModel, CardView _cardView)
        {
            cardModel = _cardModel;
            cardView = _cardView;
            cardsThisBlocks = new List<CardController>();

            cardView.Init(this);
            UpdateSprite(false);
        }

        public void UpdateSprite(bool isFaceUp)
        {
            cardView.SetupSprite(cardModel.cardSprite,isFaceUp);
        }

        public void SetCardFaceUp()
        {
            cardView.SetupSprite(cardModel.cardSprite, true);
        }


        // This function will add cards that are blocked by this current card.
        public void AddCardThisBlocks(CardController cardController)
        {
            cardsThisBlocks.Add(cardController);
        }

        public void AddCardsThatBlockThis(CardController cardController)
        {
            cardModel.AddBlocker(1);
            cardController.AddCardThisBlocks(this);
        }

        public void UpdateCardsThisBlocks()
        {
            foreach(CardController cardThisBlocks in cardsThisBlocks)
            {
                cardThisBlocks.UnblockThis();
            }
        }

        public void UnblockThis()
        {
            cardModel.RemoveBlocker();

            if(cardModel.IsPlayable) SetCardFaceUp();
        }

        public void OnCardClicked()
        {
            
            if (cardModel.IsPlayable) onCardClicked?.Invoke(this);
        }
    }
}
