using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.UI
{
    public class WeaponInventorMenu : MonoBehaviour
    {
        MenuController menuController;
        [SerializeField] Sprite currentMeleeWeaponSprite;
        [SerializeField] Sprite currentRangedWeaponSprite;
        #region New
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
        
        #region Old
        [SerializeField] Image inventoryMeleeSprite;
        [SerializeField] Image inventoryRangedSprite;
        [SerializeField] Text inventoryMeleeText;
        [SerializeField] Text inventoryRangedText;
        [SerializeField] Text inventoryAmmoText;
        #endregion
        // Start is called before the first frame update
        void Start()
        {
            menuController = GetComponent<MenuController>();
            SetAmmoInventoryText();
        }

        public void SetCurrentWeaponActive(int currentWeapon)
        {
            Image activeWeaponImage = menuController.GetCurrentWeaponActive().GetComponent<Image>();
            switch(currentWeapon)
            {
                case 0:
                    activeWeaponImage.sprite = currentMeleeWeaponSprite;
                    menuController.HideUIObject(menuController.GetAmmoText());
                    break;
                case 1:
                    activeWeaponImage.sprite = currentRangedWeaponSprite;
                    menuController.ShowUIObject(menuController.GetAmmoText());
                    break;
                default:
                    activeWeaponImage.sprite = currentMeleeWeaponSprite;
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
            currentMeleeWeaponSprite = sprite;
            SetInventoryMeleeSprite();
        }

        public void SetRangedWeaponSprite(Sprite sprite)
        {
            currentRangedWeaponSprite = sprite;
            SetInventoryRangedSprite();
        }

        public void SetAmmoText(string ammo)
        {
            inventoryAmmoText.text = "x"+ammo;
        }

        public void SetInventoryMeleeSprite()
        {
            inventoryMeleeSprite.sprite = currentMeleeWeaponSprite;
        }

        public void SetInventoryRangedSprite()
        {
            inventoryRangedSprite.sprite = currentRangedWeaponSprite;
        }

        public void SetMeleeInventoryText(string weapoName)
        {
            inventoryMeleeText.text = weapoName;
        }

        public void SetRangedInventoryText(string weaponName)
        {
            inventoryRangedText.text = weaponName;
        }

        public void SetAmmoInventoryText()
        {
            inventoryAmmoText.text = menuController.GetAmmoText().GetComponent<Text>().text;
        }

    }

}