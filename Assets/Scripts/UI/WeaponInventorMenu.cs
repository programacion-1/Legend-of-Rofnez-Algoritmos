using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace RPG.UI
{
    public class WeaponInventorMenu : MonoBehaviour
    {
        MenuController menuController;
        Sprite _currentMeleeWeaponSprite;
        public Sprite CurrentMeleeWeaponSprite{set{_currentMeleeWeaponSprite = value;}}
        Sprite _currentRangedWeaponSprite;
        public Sprite CurrentRangedWeaponSprite{set{_currentRangedWeaponSprite = value;}}
        #region New
        [Header("Variables para la barra del UI principal")]
        [SerializeField] Image _inventoryMeleeImage; 
        [SerializeField] Image _inventoryRangedImage;
        [SerializeField] Image _currentInventoryWeaponImage;
        [SerializeField] GameObject _inventoryAmmoContainer;
        TextMeshProUGUI _inventoryAmmoText;

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
            _inventoryMeleeImage.color = new Color(0,0,0,0);
            _inventoryRangedImage.color = new Color(0,0,0,0);
            _inventoryAmmoText = _inventoryAmmoContainer.GetComponentInChildren<TextMeshProUGUI>();
        }

        public void SetCurrentActiveWeapon(params object[] p)
        {
            Sprite currentSprite = (Sprite) p[0];
            _currentInventoryWeaponImage.sprite = currentSprite;
            if(p.Length == 1)
            {
                _inventoryAmmoContainer.SetActive(false);
            }
            else
            {
                string ammo = p[1].ToString();
                _inventoryAmmoContainer.SetActive(true);
                _inventoryAmmoText.text = ammo;
            }           
        }

        public void SetMeleeWeaponSprite()
        {
            _inventoryMeleeImage.sprite = _inventoryMeleeWeaponSprite;
            _inventoryMeleeImage.color = Color.white;
        }
      
        public void SetRangedWeaponSprite()
        {
            _inventoryRangedImage.sprite = _inventoryRangedWeaponSprite;
            _inventoryRangedImage.color = Color.white;
        }

        public void SetAmmoText(string ammo)
        {
            _inventoryAmmoText.text = ammo;
        }
    }
}