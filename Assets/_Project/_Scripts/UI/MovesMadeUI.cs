namespace TriPeakSolitaire.UI
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using TMPro;
    using TriPeakSolitaire.Gameplay;
    using UnityEngine;

    public class MovesMadeUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI movesMadeText;

        private void OnEnable()
        {
            GameManager.onMoveMade += MoveMade;
        }

        private void MoveMade(int moveNumber)
        {
            movesMadeText.text =$"{moveNumber:00}";
        }

        private void OnDisable()
        {
            GameManager.onMoveMade -= MoveMade;
        }
    }
}
