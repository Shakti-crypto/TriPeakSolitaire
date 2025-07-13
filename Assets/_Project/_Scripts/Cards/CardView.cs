namespace TriPeakSolitaire.Cards
{
    using UnityEngine;
    using UnityEngine.Events;
    using UnityEngine.UI;

    public class CardView : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer cardSprite;
        public Transform cardRect;
        [SerializeField] private Button button;

        public void SetupSprite(Sprite sprite, bool faceUp)
        {
            if (faceUp) cardSprite.sprite = sprite;
            else cardSprite.sprite = GameAssetsContainer.Instance.cardBackSprite;
        }

        public void SetInteractable(bool interactable)
        {
            button.interactable = interactable;
        }

        public void SetupButtonAction(UnityAction onButtonClick)
        {
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(onButtonClick);
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



    }
}
