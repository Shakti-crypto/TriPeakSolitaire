namespace TriPeakSolitaire.Cards
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class CardController
    {
        public CardModel cardModel;
        public CardView cardView;
        private List<CardController> cardsThisBlocks;
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
