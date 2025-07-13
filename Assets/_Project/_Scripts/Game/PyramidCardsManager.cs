

namespace TriPeakSolitaire.Gameplay
{
    using MEC;
    using System.Collections.Generic;
    using TriPeakSolitaire.Cards;
    using UnityEngine;

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
