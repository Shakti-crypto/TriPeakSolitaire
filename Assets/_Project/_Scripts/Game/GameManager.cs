namespace TriPeakSolitaire.Gameplay
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using TriPeakSolitaire.Cards;
    using UnityEngine;

    public class GameManager : MonoBehaviour
    {
        [SerializeField] private CardSpawner cardSpawner;
        [SerializeField] private PyramidBuilder pyramidBuilder;
        [SerializeField] private DeckManager deckManager;
        private CardController[] cardControllers;
        private List<CardView> pyramidCardViews;
        private List<CardController> deckCardPile;

        private void Start()
        {
            cardControllers = cardSpawner.SpawnCards();
            CardShuffler.Shuffle(cardControllers);

            pyramidCardViews = new List<CardView>();
            for(int i = 0; i < 28; i++)
            {
                pyramidCardViews.Add(cardControllers[i].cardView);
                if (i > 17) //last 10 cards at the pyramid bottom
                {
                    cardControllers[i].SetCardFaceUp();
                }
            }

            deckCardPile = new List<CardController>();
            for(int i = 28; i < 52; i++)
            {
                deckCardPile.Add(cardControllers[i]);
            }

            pyramidBuilder.BuildPyramid(pyramidCardViews);
            deckManager.SetupDeckCards(deckCardPile);
        }
    }
}
