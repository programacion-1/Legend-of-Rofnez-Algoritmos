using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Item;
using RPG.InventorySystem;

namespace RPG.Combat
{
    public class MeleeWeaponPickup : WeaponPickup
    {
        public override void SetWeaponOnInventory(WeaponInventory wI, Weapon w)
        {
            wI.equippedMeleeWeapon = (MeleeWeapon)w;
            wI.SetActiveWeapon(wI.equippedMeleeWeapon);
        }
    }

}