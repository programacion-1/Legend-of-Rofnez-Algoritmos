using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Item;
using RPG.Core;
using RPG.UI;

namespace RPG.InventorySystem
{
    public class ItemInventory : MonoBehaviour
    {
        [SerializeField] CharaHealth _health;
        //Dictionary<string, PotionContainer> potionCollection; Eliminado porque no se guardan los datos en el diccionario
        List<PotionContainer> _potionCollection = new List<PotionContainer>();
        public List<string> potions = new List<string>();
        ItemInventoryMenu _itemInventoryMenu;

        void Start()
        {
            _health = GetComponent<CharaHealth>();
            _itemInventoryMenu = FindObjectOfType<ItemInventoryMenu>();
        }

        
        public void TryAddPotion(PotionContainer newPotion)
        {
            PotionContainer potionToAdd = null;
            if(_potionCollection.Count == 0)
            {
                potionToAdd = newPotion;
                AddPotion(potionToAdd);
            }
            else
            {
                string potionKey = newPotion.potionName;
                for(int i = 0; i < potions.Count; i++)
                {
                    if(potions[i] == newPotion.name)
                    {
                        potionToAdd = _potionCollection[i];
                        break;
                    }
                }
                if(potionToAdd == null)
                {
                    potionToAdd = newPotion;
                    AddPotion(potionToAdd);
                } 
            }

            int potToModifyQuantity = potions.IndexOf(potionToAdd.name);
            SetPotionQuantity(_potionCollection[potToModifyQuantity], _potionCollection[potToModifyQuantity].potionQuantity + 1);
            
            /*
            if(!_potionCollection.ContainsKey(potionKey))
            {
                _potionCollection.Add(potionKey, newPotion);
                potions.Add(potionKey);
                oldQuantity = 0;
            }
            else
            {
                //Settings necesarios para evitar un nullReferenceException respecto al potionContainer y que no se actualice correctamente la cantidad de potiones
                oldQuantity = _potionCollection[potionKey].potionQuantity;
                _potionCollection[potionKey] = newPotion;
            }
            
            _potionCollection[potionKey].potion.EquipSettings(_health);
            SetPotionQuantity(_potionCollection[potionKey], oldQuantity + 1);*/
        }

        public void AddPotion(PotionContainer newPotion)
        {
            _potionCollection.Add(newPotion);
            potions.Add(newPotion.potionName);
            _potionCollection[_potionCollection.IndexOf(newPotion)].potion.EquipSettings(_health);
        }

        /*public PotionContainer GetPotion(string key)
        {
            return _potionCollection[key];
        }*/

        public void SetPotionQuantity(PotionContainer potionToModify, int quantity)
        {
            potionToModify.potionQuantity = Mathf.Min(quantity, potionToModify.maxQuantity);
            //Hardcodeado full, pendiente a cambio para el segundo parcial
            if(potionToModify.potionName == _itemInventoryMenu.healthPotKey)
            {
                _itemInventoryMenu.SetHealthPotQuantityText(potionToModify.potionQuantity.ToString());
                _itemInventoryMenu.SetTextColor(SetQuantityColor(potionToModify), _itemInventoryMenu.healthPotQuantityText);
            }
            else if(potionToModify.potionName == _itemInventoryMenu.magicPotKey)
            {
                _itemInventoryMenu.SetMagicPotQuantityText(potionToModify.potionQuantity.ToString());
                _itemInventoryMenu.SetTextColor(SetQuantityColor(potionToModify), _itemInventoryMenu.magicPotQuantityText);
            }
            Debug.Log(potionToModify.potionName+" : "+potionToModify.potionQuantity);
        }

        public Color SetQuantityColor(PotionContainer potion)
        {
            Color textColor = new Color();
            if(potion.potionQuantity == potion.maxQuantity) textColor = Color.green;
            else if(potion.potionQuantity <= 0) textColor = Color.red;
            else textColor = Color.white;
            return textColor;
        }

        public void UsePotionOnInventory(string key)
        {
            int index = SetPotionCollectionIndex(key);
            if(CheckIfIHavePotions(index))
            {
                int newQuantity = _potionCollection[index].potionQuantity - 1;
                SetPotionQuantity(_potionCollection[index], newQuantity);
                _potionCollection[index].potion.ConsumePotionSettings();
                Debug.Log(_potionCollection[index].potionName+" : "+_potionCollection[index].potionQuantity);    
            }
        }

        public int SetPotionCollectionIndex(string potion)
        {
            int key = -1;
            for(int i = 0; i < _potionCollection.Count; i++)
            {
                if(_potionCollection[i].potionName == potion)
                {
                    key = i;
                    break;
                } 
            }
            return key;
        }

        public bool CheckIfIHavePotions(int potion)
        {
            if(_potionCollection[potion].potionQuantity > 0) return true;
            else return false;
        }
    }
}
