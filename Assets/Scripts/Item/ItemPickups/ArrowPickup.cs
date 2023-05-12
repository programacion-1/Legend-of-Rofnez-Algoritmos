using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Core;
using RPG.UI;
using RPG.InventorySystem;

namespace RPG.Combat
{
    public class ArrowPickup : ItemPickup
    {
        int arrowQuantity = 0;
        [SerializeField] int arrowMaxQuantity;

        public override void UseItem(GameObject player)
        {
            arrowQuantity = Random.Range(1, arrowMaxQuantity);
            if(player.GetComponent<WeaponInventory>().equippedRangedWeapon != null)
            {
                player.GetComponent<WeaponInventory>().rangedWeaponAmmo += arrowQuantity;
                GameObject.FindObjectOfType<WeaponInventorMenu>().SetAmmoText(player.GetComponent<WeaponInventory>().rangedWeaponAmmo.ToString());
            }

            else print("No ranged weapon equipped.");
        }
    }

}