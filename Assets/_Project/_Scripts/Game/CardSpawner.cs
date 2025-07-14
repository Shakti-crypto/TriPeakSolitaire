namespace TriPeakSolitaire.Gameplay
{
    using UnityEngine;
    using TriPeakSolitaire.Cards;
    using System.Collections.Generic;


    /// <summary>
    /// Handles the instantiation of cards using CardData assets and sets up
    /// their MVC components (CardModel, CardView, and CardController).
    /// Generates new deck cards.
    /// </summary>
    public class CardSpawner : MonoBehaviour
    {
        [SerializeField] private CardData[] cardDatas;
        [SerializeField] private GameObject cardPrefab;
        [SerializeField] private Transform cardSpawnParent;

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
        
        public List<CardController> SpawnNewDeck(int deckLength)
        {
            List<CardController> cardControllers = new List<CardController>();

            for(int i = 0; i < deckLength; i++)
            {
                GameObject cardGO = Instantiate(cardPrefab, cardSpawnParent);
                CardData cardData = cardDatas[Random.Range(0,cardDatas.Length)];
                CardModel cardModel = new CardModel(cardData);
                CardView cardView = cardGO.GetComponent<CardView>();

                CardController cardController = new CardController(cardModel, cardView);
                cardControllers.Add(cardController);
            }

            return cardControllers;
        }
    }
}
