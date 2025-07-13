using PrimeTween;
using System.Collections;
using System.Collections.Generic;
using TriPeakSolitaire.Cards;
using UnityEngine;

public class DeckManager : MonoBehaviour
{
    [SerializeField] private Transform deckPileTransform;
    [SerializeField] private Transform wastePileTransform;
    [SerializeField] private TweenSettings moveAnimationSettings;
    private Stack<CardController> deckPile;
    private List<CardController> wastePile;

    public void SetupDeckCards(List<CardController> deckCards)
    {
        deckPile = new Stack<CardController>(deckCards);
        int cardIndex=0;
        foreach(CardController cardController in deckCards)
        {
            CardView cardView = cardController.cardView;
            Vector3 cardPosition = deckPileTransform.position;
            cardPosition.z -= cardIndex * 0.1f;
            cardView.SetCardPosition(cardPosition);
        }
    }

    public void MoveCardFromDeckToWastePile()
    {
        if (deckPile.Count == 0)
        {
            Debug.Log("No card left in deck pile");
            return;
        }

        CardController cardOnTop =  deckPile.Pop();

        Tween.Position(cardOnTop.cardView.transform, wastePileTransform.position, moveAnimationSettings).OnComplete(() => { wastePile.Add(cardOnTop); });
    }


}
