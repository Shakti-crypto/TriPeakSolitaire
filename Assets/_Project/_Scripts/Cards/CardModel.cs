namespace TriPeakSolitaire.Cards
{
    using UnityEngine;

    /// <summary>
    /// Represents the logic and runtime state of a card
    /// Handles the data regarding the cards that block this current card
    /// </summary>
    public class CardModel
    {
        public CardData cardData;
        private int blockersRemaining;

        public Sprite cardSprite => cardData.frontSprite;
        public bool IsPlayable => blockersRemaining == 0;

        public CardModel(CardData _cardData)
        {
            cardData = _cardData;
        }

        public void RemoveBlocker()
        {
            blockersRemaining = Mathf.Max(0, blockersRemaining - 1);
        }

        public void AddBlocker(int numberOfBlockers)
        {
            blockersRemaining += numberOfBlockers;
        }

    }
}
