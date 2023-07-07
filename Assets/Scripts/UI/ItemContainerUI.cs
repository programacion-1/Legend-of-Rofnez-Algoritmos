using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace RPG.UI
{
    public class ItemContainerUI : MonoBehaviour
    {
        [SerializeField] Image _itemImage;
        [SerializeField] TextMeshProUGUI _itemQuantityText;
        // Start is called before the first frame update

        public void SetDefaultValues()
        {
            _itemImage.sprite = null;
            DeactivateItemContainer();
        }

        public void SetItemValues(Sprite itemSprite, string text)
        {
            _itemImage.sprite = itemSprite;
            SetItemQuantityText(text);
        }

        public void SetItemQuantityText(string text)
        {
            _itemQuantityText.text = text;
        }

        public void SetItemQuantityTextColor(Color newColor)
        {
            _itemQuantityText.color = newColor;
        }

        public void ActivateItemContainer()
        {
            _itemImage.color = Color.white;
            _itemQuantityText.gameObject.SetActive(true);
        }

        public void DeactivateItemContainer()
        {
            _itemImage.color = new Color(0,0,0,0);
            _itemQuantityText.gameObject.SetActive(false);
        }
    }
}
