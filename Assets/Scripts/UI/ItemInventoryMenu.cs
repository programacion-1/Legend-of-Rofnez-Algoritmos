using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.UI
{
    public class ItemInventoryMenu : MonoBehaviour
    {
        [SerializeField] string _healthPotKey;
        public string healthPotKey{get{return _healthPotKey;}}
        [SerializeField] Text _healthPotQuantityText;
        public Text healthPotQuantityText{get{return _healthPotQuantityText;}}
        [SerializeField] string _magicPotKey;
        public string magicPotKey{get{return _magicPotKey;}}
        [SerializeField] Text _magicPotQuantityText;
        public Text magicPotQuantityText{get{return _magicPotQuantityText;}}

        public void SetHealthPotQuantityText(string quantity)
        {
            _healthPotQuantityText.text = "X" + quantity;
        }

        public void SetMagicPotQuantityText(string quantity)
        {
            _magicPotQuantityText.text = "X" + quantity;
        }

        public void SetTextColor(Color newColor, Text myText)
        {
            myText.color = newColor;
        }
    }
}


