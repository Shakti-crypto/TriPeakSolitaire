namespace TriPeakSolitaire.Cards
{
    using UnityEngine;

    public class CardModel
    {
        private CardData cardData;
        private int blockersRemaining;
        public bool isFaceUp;

        public Sprite cardSprite => cardData.frontSprite;
        public bool IsPlayable => isFaceUp && blockersRemaining == 0;

        public CardModel(CardData _cardData)
        {
            cardData = _cardData;
        }

        public void RemoveBlocker()
        {
            blockersRemaining = Mathf.Max(0, blockersRemaining - 1);
        }

    }
}
