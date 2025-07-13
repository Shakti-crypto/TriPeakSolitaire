namespace TriPeakSolitaire.Cards
{
    using UnityEngine;
    using UnityEngine.Events;
    using UnityEngine.UI;

    public class CardView : MonoBehaviour
    {
        [SerializeField] private Image cardSpriteUI;
        [SerializeField] private Button button;

        public void SetupSprite(Sprite sprite, bool faceUp)
        {
            if (faceUp) cardSpriteUI.sprite = sprite;
            else cardSpriteUI.sprite = GameAssetsContainer.Instance.cardBackSprite;
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

    }
}
