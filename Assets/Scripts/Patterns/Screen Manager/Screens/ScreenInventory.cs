using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using RPG.UI;

public class ScreenInventory : MonoBehaviour, IScreen
{
    WeaponInventorMenu _weaponInventoryMenu;
    //[SerializeField] Canvas _canvas;
    [SerializeField] Image _meleeWeaponImage;
    [SerializeField] Image _rangedWeaponImage;
    [SerializeField] Text _meleeWeaponText;
    [SerializeField] Text _rangedWeaponText;
    [SerializeField] Text _rangedWeaponAmmoText;
    public void Activate()
    {
        _weaponInventoryMenu = GameObject.FindObjectOfType<WeaponInventorMenu>();
        _meleeWeaponImage.sprite = _weaponInventoryMenu.inventoryMeleeWeaponSprite;
        _rangedWeaponImage.sprite = _weaponInventoryMenu.inventoryRangedWeaponSprite;
        _meleeWeaponText.text = _weaponInventoryMenu.inventoryMeleeWeaponText;
        _rangedWeaponText.text = _weaponInventoryMenu.inventoryRangedWeaponText;
        _rangedWeaponAmmoText.text = _weaponInventoryMenu.inventoryRangedWeaponAmmoText;
        TriggerSetScreenActiveEvent(true);
        Debug.Log("Activate Inventory Screen");
    }

    public void Deactivate()
    {
        Debug.Log("Deactivate Inventory Screen");
        TriggerSetScreenActiveEvent(false);
    }

    public void Free()
    {
        Destroy(gameObject);
        TriggerSetScreenActiveEvent(false);
    }

    #region EventManager
    void TriggerSetScreenActiveEvent(bool active)
    {
        EventManager.TriggerEvent(EventManager.Events.Event_SetIfIsAScreenActiveOnScene, active);
    }
    #endregion
}
