namespace TriPeakSolitaire.Cards
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class CardController
    {
        private CardModel cardModel;
        private CardView cardView;

        public CardController(CardModel _cardModel, CardView _cardView)
        {
            cardModel = _cardModel;
            cardView = _cardView;

            Init();
        }

        public void Init()
        {
            cardView.SetupSprite(cardModel.cardSprite, cardModel.isFaceUp);
        }
    }
}
