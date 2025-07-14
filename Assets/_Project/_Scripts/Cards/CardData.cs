namespace TriPeakSolitaire.Cards
{
    using UnityEngine;

    /// <summary>
    /// A ScriptableObject that defines static data for a card, including its value,
    /// display name, and associated front sprites. Used during card creation.
    /// </summary>
    [CreateAssetMenu(fileName = "Card Data",menuName = "Cards/Card Data")]
    public class CardData : ScriptableObject
    {
        public string cardName;

        /// <summary>
        /// The numerical value of the card, used for gameplay logic and comparisons.
        /// Ranges from 1 (Ace) to 13 (King).
        /// </summary>
        public int value;

        public Sprite frontSprite;
    }
}
