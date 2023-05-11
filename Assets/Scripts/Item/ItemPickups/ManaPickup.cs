using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Core;

namespace RPG.Item
{
    public class ManaPickup : ItemPickup
    {
        public GameObject manaVFX;
        public override void UseItem(GameObject player)
        {
            /*ItemInventory itemInventory = player.GetComponent<ItemInventory>();
            if(itemInventory.GetCurrentHPpotions() < itemInventory.GetMaxHPpotions())
            {
                int newQuantity = itemInventory.GetCurrentHPpotions() + 1;
                itemInventory.SetCurrentHPpotions(newQuantity);
                itemInventory.SetHPPotQuantity();
            }*/
        }
    }

}