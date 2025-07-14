
namespace TriPeakSolitaire.Gameplay
{
    using MEC;
    using System.Collections.Generic;
    using TriPeakSolitaire.Cards;
    using UnityEngine;

    /// <summary>
    /// Tracks all cards in the pyramid during gameplay. Provides utility methods
    /// to query face-up or remaining cards, used for win/loss evaluations.
    /// </summary>
    public class PyramidCardsManager : MonoBehaviour
    {
        [SerializeField] private PyramidBuilder pyramidBuilder;
        private List<CardController> pyramidCards;

        public int numberOfCardsInPyramid
        {
            get
            {
                if (pyramidCards == null) return 0;
                else return pyramidCards.Count;
            }
        }

        public void SetupPyramid(List<CardController> _pyramidCards)
        {
            pyramidCards = _pyramidCards;
            pyramidBuilder.BuildPyramid(pyramidCards);

            foreach (CardController card in pyramidCards)
            {
                card.onCardClicked += OnCardClicked;
            }
        }

        private void OnCardClicked(CardController cardToCheck)
        {
            GameManager.Instance.CheckCardFromPyramidToWaste(cardToCheck);

        }

        public IEnumerator<float> WaitToUpdateOtherCards(CardController card)
        {
            yield return Timing.WaitForSeconds(0.5f);
            card.UpdateCardsThisBlocks();
            pyramidCards.Remove(card);
        }

        /// <summary>
        /// Used in GameManager to evaluate winning condition
        /// </summary>
        public List<CardController> GetFaceUpCardsInPyramid()
        {
            List<CardController> faceUpCards = new List<CardController>();
            foreach(CardController card in pyramidCards)
            {
                if (card.cardModel.IsPlayable) faceUpCards.Add(card);
            }

            return faceUpCards;
        }


        private void OnDisable()
        {
            foreach (CardController card in pyramidCards)
            {
                card.onCardClicked -= OnCardClicked;
            }
        }
    }
}
