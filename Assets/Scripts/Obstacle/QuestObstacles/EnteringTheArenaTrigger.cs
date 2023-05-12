using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Obstacle
{
    public class EnteringTheArenaTrigger : MonoBehaviour
    {
        [SerializeField] ArenaObstacle _arenaObstacle;
        [SerializeField] int playerLayer = 6;
        private void OnTriggerEnter(Collider other)
        {
            if(other.gameObject.layer == playerLayer && other.gameObject.tag == "Player") _arenaObstacle.EnterArena();
        }
    }
}
