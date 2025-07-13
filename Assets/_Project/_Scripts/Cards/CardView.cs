namespace TriPeakSolitaire.Cards
{
    using UnityEngine;
    using UnityEngine.Events;
    using UnityEngine.UI;

    public class CardView : MonoBehaviour
    {
        public Transform cardRect;
        [SerializeField] private SpriteRenderer cardSprite;
        private CardController controller;

        public void Init(CardController _controller)
        {
            controller = _controller;
        }

        public void SetupSprite(Sprite sprite, bool faceUp)
        {
            if (faceUp) cardSprite.sprite = sprite;
            else cardSprite.sprite = GameAssetsContainer.Instance.cardBackSprite;
        }

        public void SetCardPosition(Vector3 position)
        {
            cardRect.position = position;
        }

        public float GetSpriteWidth()
        {
            float spriteWidthInUnits = cardSprite.sprite.bounds.size.x;
            float worldWidth = spriteWidthInUnits * transform.localScale.x;
            return worldWidth;
        }

        private void OnMouseDown()
        {
            controller.OnCardClicked();
        }

    }
}
