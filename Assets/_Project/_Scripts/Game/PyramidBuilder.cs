namespace TriPeakSolitaire.Gameplay
{
    using System.Collections.Generic;
    using TriPeakSolitaire.Cards;
    using UnityEngine;


    /// <summary>
    /// Lays out the cards in the Tri-Peaks pyramid structure.
    /// Assigns visual positions and logical blocker relationships between cards during setup.
    /// </summary>
    public class PyramidBuilder : MonoBehaviour
    {
        [SerializeField] private float horizontalSpace;
        [SerializeField] private float verticalSpace;
        [SerializeField] private Transform middlePeakPosition;
        private List<CardController> cardsInLastRow; //These are face down cards in the last row
        private float cardWidth;
        private int cardIndex;

        public void BuildPyramid(List<CardController> cards)
        {
            cardIndex = 0;
            cardWidth = cards[0].cardView.GetSpriteWidth();
            cardsInLastRow = new List<CardController>();
            for (int i = 0; i < 3; i++)
            {
                Vector3 peakPosition = middlePeakPosition.position;
                peakPosition.x = (i - 1) * (cardWidth + horizontalSpace) * 3;
                BuildPyramidPeak(cards, peakPosition);
            }

            BuildPyramidDeck(cards, middlePeakPosition.position);
        }

        public void BuildPyramidPeak(List<CardController> cards, Vector3 peakPosition)
        {
            List<CardController> cardsInThisPeak = new List<CardController>();

            for (int i = 0; i < 3; i++)
            {
                float horizontalOffset = -(i * (cardWidth + horizontalSpace)) / 2;
                for (int j = 0; j < i + 1; j++)
                {
                    Vector3 cardPosition = peakPosition;
                    cardPosition.y -= verticalSpace * i;
                    cardPosition.x += horizontalOffset + j * (cardWidth + horizontalSpace);
                    cardPosition.z = -cardIndex * 0.1f;

                    CardView card = cards[cardIndex].cardView;
                    card.SetCardPosition(cardPosition);

                    cardsInThisPeak.Add(cards[cardIndex]);
                    if (i == 2) cardsInLastRow.Add(cards[cardIndex]);

                    cardIndex++;
                }
            }

            SetupBlockedCardsForThisPeak(cardsInThisPeak);
        }

        private void SetupBlockedCardsForThisPeak(List<CardController> cardsInThisPeak)
        {

            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < i + 1; j++)
                {
                    CardController card = GetCardFromListByRowAndColumn(i, j);
                    card.AddCardsThatBlockThis(GetCardFromListByRowAndColumn(i + 1, j));
                    card.AddCardsThatBlockThis(GetCardFromListByRowAndColumn(i + 1, j + 1));
                }
            }

            CardController GetCardFromListByRowAndColumn(int row, int column)
            {
                return cardsInThisPeak[(row * (row + 1)) / 2 + column];
            }
        }

        //Here deck means the pyramid deck(bottom 10 cards of pyramid)
        private void SetupBlockedCardForLastRowOfPeak(List<CardController> deckCards)
        {
            for (int i = 0; i < cardsInLastRow.Count; i++)
            {
                CardController card = cardsInLastRow[i];
                card.AddCardsThatBlockThis(deckCards[i]);
                card.AddCardsThatBlockThis(deckCards[i + 1]);
            }
        }


        /// <summary>
        /// Places the bottom 10 cards of the pyramid
        /// </summary>
        public void BuildPyramidDeck(List<CardController> cards, Vector3 middlePeakPosition)
        {
            Vector3 firstCardPosition = middlePeakPosition;
            firstCardPosition.y -= verticalSpace * 3;
            firstCardPosition.x -= (cardWidth * 6f) + horizontalSpace;
            firstCardPosition.z = -cardIndex * 0.1f;

            List<CardController> deckCards = new List<CardController>();

            for (int i = 0; i < 10; i++)
            {
                CardView card = cards[cardIndex].cardView;
                card.SetCardPosition(firstCardPosition);
                firstCardPosition.x += (cardWidth + horizontalSpace);
                firstCardPosition.z = -cardIndex * 0.1f;
                deckCards.Add(cards[cardIndex]);
                cardIndex++;
            }

            SetupBlockedCardForLastRowOfPeak(deckCards);
        }
    }
}
