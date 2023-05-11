using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.InventorySystem;
using RPG.Item;

public class PotionPickup : ItemPickup
{
    [SerializeField] PotionContainer _potionContainer;
    public override void UseItem(GameObject player)
    {
        ItemInventory itemInventory = player.GetComponent<ItemInventory>();
        itemInventory.TryAddPotion(_potionContainer);
    }
}
