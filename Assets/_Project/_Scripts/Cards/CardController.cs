namespace TriPeakSolitaire.Cards
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class CardController
    {
        private CardModel cardModel;
        public CardView cardView;

        public CardController(CardModel _cardModel, CardView _cardView)
        {
            cardModel = _cardModel;
            cardView = _cardView;

            UpdateSprite();
        }

        public void UpdateSprite()
        {
            cardView.SetupSprite(cardModel.cardSprite, cardModel.isFaceUp);
        }

        public void SetCardFaceUp()
        {
            cardModel.isFaceUp = true;
            cardView.SetupSprite(cardModel.cardSprite, true);
        }
    }
}
