using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Obstacle;

namespace RPG.Item
{
    public class PlankPickup : ItemPickup
    {
        public override void UseItem(GameObject player)
        {
            GameObject.FindObjectOfType<BridgeObstacle>().CollectPlank();
        }
    }

}