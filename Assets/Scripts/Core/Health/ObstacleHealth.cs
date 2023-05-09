using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace RPG.Core
{
    public class ObstacleHealth : Health
    {
        [Header ("Obstacle Health Stats")]
        [SerializeField] float _damageRate;
        [SerializeField] Transform shaderSpawnpoint;
        [SerializeField] protected GameObject hitFX;

        public override void ChildrenDamageBehavior()
        {
            audioManager.TryToPlayClip(audioManager.obstacleSources, impactClipSound);
            GameObject.Instantiate(hitFX, shaderSpawnpoint.position,shaderSpawnpoint.rotation);
            EnableInvencibility(_damageRate);
        }

        public override void DeathBehaviour()
        {
            audioManager.TryToPlayClip(audioManager.obstacleSources, deadClipSound);
            GetComponent<NavMeshObstacle>().enabled = false;
            Destroy(this.gameObject);
        }

        void Start()
        {
            CoreStartingSettings();
        }
    }

}