namespace TriPeakSolitaire.Gameplay
{
    using UnityEngine;
    using TriPeakSolitaire.Cards;

    public class CardSpawner : MonoBehaviour
    {
        [SerializeField] private CardData[] cardDatas;
        [SerializeField] private GameObject cardPrefab;
        [SerializeField] private Transform cardSpawnParent;
        private CardController[] cardControllers;

        private void Start()
        {
            CreateCards();
        }

        private void CreateCards()
        {   
            cardControllers = new CardController[cardDatas.Length];
            for(int i=0;i<cardDatas.Length;i++)
            {

                GameObject cardGO = Instantiate(cardPrefab, cardSpawnParent);

                CardData cardData = cardDatas[i];
                CardModel cardModel = new CardModel(cardData);
                CardView cardView = cardGO.GetComponent<CardView>();

                CardController cardController = new CardController(cardModel, cardView);
                cardControllers[i] = cardController;
            }
        }
    }
}
