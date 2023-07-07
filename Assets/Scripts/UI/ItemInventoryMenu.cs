using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.UI
{
    public class ItemInventoryMenu : MonoBehaviour
    {
        [SerializeField] ItemContainerUI[] _itemContainerUIs;

        void Start()
        {
            foreach(ItemContainerUI itemContainerUI in _itemContainerUIs) itemContainerUI.SetDefaultValues();
        }
        
        public void SetItemOnContainer(int item, Sprite itemSprite, string text)
        {
            if(item < _itemContainerUIs.Length)
            {
                _itemContainerUIs[item].ActivateItemContainer();
                _itemContainerUIs[item].SetItemValues(itemSprite, text);
            }
        }

        public void SetTextQuantityOnContainer(int item, string text)
        {
            if(item < _itemContainerUIs.Length) _itemContainerUIs[item].SetItemQuantityText(text);
        }

        public void SetTextQuantityColorOnContainer(int item, Color newColor)
        {
            if(item < _itemContainerUIs.Length) _itemContainerUIs[item].SetItemQuantityTextColor(newColor);
        }

        


    }
}


