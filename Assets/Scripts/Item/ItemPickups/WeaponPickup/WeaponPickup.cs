﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Item;
using RPG.Core;

namespace RPG.Combat
{
    public abstract class WeaponPickup : ItemPickup
    {
        [SerializeField] Weapon weaponToEquip = null;

        public override void UseItem(GameObject player)
        {
            WeaponInventory playerWeaponInventory = player.GetComponent<WeaponInventory>();
            SetWeaponOnInventory(playerWeaponInventory, weaponToEquip);
            player.GetComponent<Fighter>().EquipWeapon(weaponToEquip);
        }

        public abstract void SetWeaponOnInventory(WeaponInventory wI, Weapon w);
    }
}
