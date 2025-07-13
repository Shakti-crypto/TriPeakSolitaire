namespace TriPeakSolitaire.Cards
{
    using UnityEngine;


    [CreateAssetMenu(fileName = "Card Data",menuName = "Cards/Card Data")]
    public class CardData : ScriptableObject
    {
        public string cardName;
        public int value;
        public Sprite frontSprite;
    }
}
