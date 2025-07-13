using MEC;
using System.Collections;
using System.Collections.Generic;
using TriPeakSolitaire.Cards;
using TriPeakSolitaire.Gameplay;
using UnityEngine;

public class PyramidCardsManager : MonoBehaviour
{
    [SerializeField] private PyramidBuilder pyramidBuilder;
    private List<CardController> pyramidCards;
    public void SetupPyramid(List<CardController> _pyramidCards)
    {
        pyramidCards = _pyramidCards;
        pyramidBuilder.BuildPyramid(pyramidCards);

        foreach(CardController card in pyramidCards)
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
    }

    private void OnDisable()
    {
        foreach (CardController card in pyramidCards)
        {
            card.onCardClicked -= OnCardClicked;
        }
    }
}
