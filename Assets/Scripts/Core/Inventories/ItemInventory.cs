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
        Dictionary<string, PotionContainer> _potionCollection;
        public Dictionary<string, PotionContainer> potionCollection{get{return _potionCollection;} set{_potionCollection = value;}}
        
        //List<PotionContainer> _potionCollection = new List<PotionContainer>();
        public List<string> potions = new List<string>(); //Mantengo la lista de string de potions por ahora para usarla como referencia a la hora de llamar por teclado desde el playerController (pendiente a cambio)
        ItemInventoryMenu _itemInventoryMenu;

        void Start()
        {
            _health = GetComponent<CharaHealth>();
            _itemInventoryMenu = FindObjectOfType<ItemInventoryMenu>();
            _potionCollection = new Dictionary<string, PotionContainer>();
        }

        
        public void TryAddPotion(PotionContainer newPotion)
        {
            #region Método con listas
            /*PotionContainer potionToAdd = null;
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
            SetPotionQuantity(_potionCollection[potToModifyQuantity], _potionCollection[potToModifyQuantity].potionQuantity + 1);*/

            #endregion
            
            //Método con diccionario
            string potionKey = newPotion.potionName;
            int oldQuantity = 0;
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

            //eso pasa porque en el caso de que se intente agregar una pota existente lo que se haga es actualizar la  cantidad de potas
            
            _potionCollection[potionKey].potion.EquipSettings(_health);
            _itemInventoryMenu.SetItemOnContainer(potions.IndexOf(potionKey), _potionCollection[potionKey].potionSprite, _potionCollection[potionKey].potionQuantity.ToString());
            SetPotionQuantity(_potionCollection[potionKey], oldQuantity + 1);
            Debug.Log(_potionCollection.Keys);
        }

        #region Metodos con Diccionario 
        
        public void SetPotionQuantity(PotionContainer potionToModify, int quantity)
        {
            potionToModify.potionQuantity = Mathf.Min(quantity, potionToModify.maxQuantity);
            Debug.Log(potionToModify.potionName+" : "+potionToModify.potionQuantity);
            
            //Hardcodeado full, pendiente a cambio para el segundo parcial
            /*if(potionToModify.potionName == _itemInventoryMenu.healthPotKey)
            {
                _itemInventoryMenu.SetHealthPotQuantityText(potionToModify.potionQuantity.ToString());
                _itemInventoryMenu.SetTextColor(SetQuantityColor(potionToModify), _itemInventoryMenu.healthPotQuantityText);
            }
            else if(potionToModify.potionName == _itemInventoryMenu.magicPotKey)
            {
                _itemInventoryMenu.SetMagicPotQuantityText(potionToModify.potionQuantity.ToString());
                _itemInventoryMenu.SetTextColor(SetQuantityColor(potionToModify), _itemInventoryMenu.magicPotQuantityText);
            }*/

            for(int i = 0; i < potions.Count; i++)
            {
                if(potions[i] == potionToModify.potionName)
                {
                    _itemInventoryMenu.SetTextQuantityOnContainer(i, potionToModify.potionQuantity.ToString());
                    _itemInventoryMenu.SetTextQuantityColorOnContainer(i, SetQuantityColor(potionToModify));
                }
            }
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
            if(_potionCollection.ContainsKey(key))
            {
                if(_potionCollection[key].potionQuantity > 0)
                {
                    int newQuantity = _potionCollection[key].potionQuantity - 1;
                    SetPotionQuantity(_potionCollection[key], newQuantity);
                    _potionCollection[key].potion.ConsumePotionSettings();
                    Debug.Log(_potionCollection[key].potionName+" : "+_potionCollection[key].potionQuantity);    
                }
            }
        }

        #endregion

        #region Metodos Con Listas (Deprecated)

        /*public void AddPotion(PotionContainer newPotion)
        {
            _potionCollection.Add(newPotion);
            potions.Add(newPotion.potionName);
            _potionCollection[_potionCollection.IndexOf(newPotion)].potion.EquipSettings(_health);
        }*/

        /*public PotionContainer GetPotion(string key)
        {
            return _potionCollection[key];
        }

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
        }/*

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
        }*/

        #endregion
    }
}
