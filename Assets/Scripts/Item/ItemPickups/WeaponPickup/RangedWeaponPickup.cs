using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Item;
using RPG.Core;

namespace RPG.Combat
{
    public class RangedWeaponPickup : WeaponPickup
    {
        public override void SetWeaponOnInventory(WeaponInventory wI, Weapon w)
        {
            wI.equippedRangedWeapon = (RangedWeapon)w;
            wI.SetActiveWeapon(wI.equippedRangedWeapon);
            wI.GetComponent<RangedWeaponAmmoInventory>().SetAmmo(wI.equippedRangedWeapon.ammo);
        }
    }

}