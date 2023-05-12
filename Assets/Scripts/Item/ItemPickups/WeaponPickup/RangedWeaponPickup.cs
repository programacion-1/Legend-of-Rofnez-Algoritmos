using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Item;
using RPG.InventorySystem;

namespace RPG.Combat
{
    public class RangedWeaponPickup : WeaponPickup
    {
        public override void SetWeaponOnInventory(WeaponInventory wI, Weapon w)
        {
            RangedWeapon rangedWeapon = (RangedWeapon) w;
            wI.EquipRangedWeapon(rangedWeapon);
            //wI.GetComponent<RangedWeaponAmmoInventory>().SetAmmo(wI.equippedRangedWeapon.ammo);
        }
    }
}