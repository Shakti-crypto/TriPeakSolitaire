using PrimeTween;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using TriPeakSolitaire.Cards;
using TriPeakSolitaire.Gameplay;
using UnityEngine;

public class DeckManager : MonoBehaviour
{
    [SerializeField] private Transform deckPileTransform;
    [SerializeField] private Transform wastePileTransform;
    [SerializeField] private TweenSettings moveAnimationSettings;
    private Stack<CardController> deckPile;
    private Stack<CardController> wastePile = new Stack<CardController>();

    public int numberOfCardsInDeck
    {
        get
        {
            if (deckPile == null) return 0;
            else return deckPile.Count;
        }
    }

    public Vector3 wastePilePosition
    {
        get
        {
            Vector3 wastePileCardPosition = wastePileTransform.position;
            wastePileCardPosition.z = -wastePile.Count * 0.1f;
            return wastePileCardPosition;
        }
    }

    public void SetupDeckCards(List<CardController> deckCards)
    {
        deckPile = new Stack<CardController>(deckCards);
        wastePile = new Stack<CardController>(deckCards);
        int cardIndex = 0;
        foreach (CardController cardController in deckCards)
        {
            cardController.onCardClicked += OnCardClicked;

            CardView cardView = cardController.cardView;
            Vector3 cardPosition = deckPileTransform.position;
            cardPosition.z -= cardIndex * 0.1f;
            cardView.SetCardPosition(cardPosition);
        }

        MoveCardFromDeckToWastePile();
    }

    private void OnCardClicked(CardController card)
    {
        MoveCardFromDeckToWastePile();
    }

    public CardController GetCardInWastePile()
    {
        if (wastePile == null) return null;

        return wastePile.Peek();
    }

    public void MoveCardFromDeckToWastePile()
    {
        if (deckPile.Count == 0)
        {
            Debug.Log("No card left in deck pile");
            return;
        }

        CardController cardOnTop = deckPile.Pop();
        wastePile.Push(cardOnTop);

        cardOnTop.onCardClicked -= OnCardClicked;
        cardOnTop.onCardClicked = null;

        Tween.Position(cardOnTop.cardView.transform, wastePilePosition, moveAnimationSettings).OnComplete(() =>
        {
            wastePile.Peek().SetCardFaceUp();
            GameManager.Instance.EvaluateGameState();
        });
    }

    public void AddCardToWastePileFromPyramid(CardController card)
    {
        wastePile.Push(card);
    }

    private void OnDisable()
    {
        foreach (CardController card in deckPile)
        {
            card.onCardClicked -= OnCardClicked;
        }
    }
}
