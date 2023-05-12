using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Item;
using RPG.InventorySystem;

namespace RPG.Combat
{
    public abstract class WeaponPickup : ItemPickup
    {
        [SerializeField] Weapon weaponToEquip = null;

        public override void UseItem(GameObject player)
        {
            WeaponInventory playerWeaponInventory = player.GetComponent<WeaponInventory>();
            SetWeaponOnInventory(playerWeaponInventory, weaponToEquip);
        }

        public abstract void SetWeaponOnInventory(WeaponInventory wI, Weapon w);
    }
}
