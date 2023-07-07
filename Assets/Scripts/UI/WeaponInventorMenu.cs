using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.UI
{
    public class WeaponInventorMenu : MonoBehaviour
    {
        MenuController menuController;
        Sprite _currentMeleeWeaponSprite;
        Sprite _currentRangedWeaponSprite;
        #region New
        [Header("Variables para la barra del UI principal")]
        [SerializeField] Image _inventoryMeleeImage; 
        [SerializeField] Image _inventoryRangedImage;
        [SerializeField] Text _inventoryMeleeText;
        [SerializeField] Text _inventoryRangedText;
        [SerializeField] Image _currentInventoryWeaponImage;
        [SerializeField] Text _inventoryAmmoText;

        [Header("Variables para el Menu")]
        string _inventoryMeleeWeaponText;
        public string inventoryMeleeWeaponText {get{return _inventoryMeleeWeaponText;} set{_inventoryMeleeWeaponText = value;}}
        string _inventoryRangedWeaponText;
        public string inventoryRangedWeaponText {get{return _inventoryRangedWeaponText;} set{_inventoryRangedWeaponText = value;}}
        string _inventoryRangedWeaponAmmoText;
        public string inventoryRangedWeaponAmmoText {get{return _inventoryRangedWeaponAmmoText;} set{_inventoryRangedWeaponAmmoText = value;}}
        Sprite _inventoryMeleeWeaponSprite;
        public Sprite inventoryMeleeWeaponSprite{get{return _inventoryMeleeWeaponSprite;} set{_inventoryMeleeWeaponSprite = value;}}
        Sprite _inventoryRangedWeaponSprite;
        public Sprite inventoryRangedWeaponSprite{get{return _inventoryRangedWeaponSprite;} set{_inventoryRangedWeaponSprite = value;}}        
        #endregion
        
        void Start()
        {
            menuController = GetComponent<MenuController>();
        }

        public void SetCurrentWeaponActive(Sprite currentSprite, string ammo)
        {
            _currentInventoryWeaponImage.sprite = currentSprite;
        }
        
        #region Old

        public void SetCurrentWeaponActive(int currentWeapon)
        {
            Image activeWeaponImage = menuController.GetCurrentWeaponActive().GetComponent<Image>();
            switch(currentWeapon)
            {
                case 0:
                    activeWeaponImage.sprite = _currentMeleeWeaponSprite;
                    menuController.HideUIObject(menuController.GetAmmoText());
                    break;
                case 1:
                    activeWeaponImage.sprite = _currentRangedWeaponSprite;
                    menuController.ShowUIObject(menuController.GetAmmoText());
                    break;
                default:
                    activeWeaponImage.sprite = _currentMeleeWeaponSprite;
                    menuController.HideUIObject(menuController.GetAmmoText());
                    break;
            }
        }

        public void SetCurrentActiveWeapon(Sprite weaponSprite)
        {
            Image activeWeaponImage = menuController.GetCurrentWeaponActive().GetComponent<Image>();
            activeWeaponImage.sprite = weaponSprite;
        }

        public void SetMeleeWeaponSprite(Sprite sprite)
        {
            _currentMeleeWeaponSprite = sprite;
            SetInventoryMeleeSprite();
        }

        public void SetRangedWeaponSprite(Sprite sprite)
        {
            _currentRangedWeaponSprite = sprite;
            SetInventoryRangedSprite();
        }

        public void SetAmmoText(string ammo)
        {
            _inventoryAmmoText.text = "x"+ammo;
        }

        public void SetInventoryMeleeSprite()
        {
            _inventoryMeleeImage.sprite = _currentMeleeWeaponSprite;
        }

        public void SetInventoryRangedSprite()
        {
            _inventoryRangedImage.sprite = _currentRangedWeaponSprite;
        }

        public void SetMeleeInventoryText(string weapoName)
        {
            _inventoryMeleeText.text = weapoName;
        }

        public void SetRangedInventoryText(string weaponName)
        {
            _inventoryRangedText.text = weaponName;
        }

        public void SetAmmoInventoryText()
        {
            //inventoryAmmoText.text = menuController.GetAmmoText().GetComponent<Text>().text;
        }

         #endregion

    }

}