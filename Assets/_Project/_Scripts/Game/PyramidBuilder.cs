using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using TMPro;
using TriPeakSolitaire.Cards;
using Unity.VisualScripting;
using UnityEngine;

public class PyramidBuilder : MonoBehaviour
{
    [SerializeField] private float horizontalSpace;
    [SerializeField] private float verticalSpace;
    [SerializeField] private Transform middlePeakPosition;
    private float cardWidth;
    private int cardIndex;

    public void BuildPyramid(List<CardView> cards)
    {
        cardIndex = 0;
        cardWidth = cards[0].GetSpriteWidth();
        for (int i = 0; i < 3; i++)
        {
            Vector3 peakPosition = middlePeakPosition.position;
            peakPosition.x = (i - 1) * (cardWidth + horizontalSpace) * 3;
            BuildPyramidPeak(cards, peakPosition);
        }

        BuildPyramidDeck(cards, middlePeakPosition.position);
    }

    public void BuildPyramidPeak(List<CardView> cards, Vector3 peakPosition)
    {
        for (int i = 0; i < 3; i++)
        {
            float horizontalOffset = -(i * (cardWidth + horizontalSpace) )/ 2;
            for (int j = 0; j < i + 1; j++)
            {
                Vector3 cardPosition = peakPosition;
                cardPosition.y -= verticalSpace * i;
                cardPosition.x += horizontalOffset + j * (cardWidth + horizontalSpace);
                cardPosition.z = -cardIndex * 0.1f;

                CardView card = cards[cardIndex];
                card.SetCardPosition(cardPosition);
                cardIndex++;
            }
        }
    }

    public void BuildPyramidDeck(List<CardView> cards, Vector3 middlePeakPosition)
    {
        Vector3 firstCardPosition = middlePeakPosition;
        firstCardPosition.y -= verticalSpace * 3;
        firstCardPosition.x -= cardWidth * 6;
        firstCardPosition.z = -cardIndex * 0.1f;


        for (int i = 0; i < 10; i++)
        {
            CardView card = cards[cardIndex];
            card.SetCardPosition(firstCardPosition);
            firstCardPosition.x += (cardWidth + horizontalSpace);
            firstCardPosition.z = -cardIndex * 0.1f;
            cardIndex++;
        }
    }
}
