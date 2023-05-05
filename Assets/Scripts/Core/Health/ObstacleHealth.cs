using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace RPG.Core
{
    public class ObstacleHealth : Health
    {
        public override void ChildrenStartingSettings()
        {
           
        }
        public override void ChildrenDamageBehavior()
        {
            audioManager.TryToPlayClip(audioManager.obstacleSources, impactClipSound);
        }

        public override void DeathBehaviour()
        {
            audioManager.TryToPlayClip(audioManager.obstacleSources, deadClipSound);
            GetComponent<NavMeshObstacle>().enabled = false;
            Destroy(this.gameObject);
        }
    }

}