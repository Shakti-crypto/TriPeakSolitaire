namespace TriPeakSolitaire.Gameplay
{
    using UnityEngine;
    using TriPeakSolitaire.Cards;
    using System.Collections.Generic;

    public class CardSpawner : MonoBehaviour
    {
        [SerializeField] private CardData[] cardDatas;
        [SerializeField] private GameObject cardPrefab;
        [SerializeField] private Transform cardSpawnParent;
        [SerializeField] private PyramidBuilder pyramidBuilder;
        [SerializeField]int cardsRequiredForPyramid = 6;


        public CardController[] SpawnCards()
        {
            CardController[] cardControllers = new CardController[cardDatas.Length];
            for(int i=0;i<cardDatas.Length;i++)
            {

                GameObject cardGO = Instantiate(cardPrefab, cardSpawnParent);
                CardData cardData = cardDatas[i];
                CardModel cardModel = new CardModel(cardData);
                CardView cardView = cardGO.GetComponent<CardView>();

                CardController cardController = new CardController(cardModel, cardView);
                cardControllers[i] = cardController;

            }
            return cardControllers;
        }

    }
}
