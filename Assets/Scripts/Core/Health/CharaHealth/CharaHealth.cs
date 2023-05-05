using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core
{
    public abstract class CharaHealth : Health
    {
        [SerializeField] Renderer charaRenderer;
        [SerializeField] Transform shaderSpawnpoint;
        [SerializeField] protected GameObject hitFX;
        [SerializeField] protected GameObject bloodFX;
        [SerializeField] string deadTriggerName;
        
        public override void ChildrenStartingSettings()
        {
            
        }

        public override void ChildrenDamageBehavior()
        {
            GameObject.Instantiate(hitFX, shaderSpawnpoint.position, shaderSpawnpoint.rotation);
            GameObject.Instantiate(bloodFX, shaderSpawnpoint.position, shaderSpawnpoint.rotation);
            audioManager.TryToPlayClip(audioManager.PlayerSFXSources, impactClipSound);
            audioManager.TryToPlayClip(audioManager.PlayerSFXSources, damageClipSound);
            CharaDamageBehaviour();
        }

        public abstract void CharaDamageBehaviour();

        public override void DeathBehaviour()
        {
            audioManager.TryToPlayClip(audioManager.PlayerSFXSources, deadClipSound);
            CharaDeathBehaviour();
            GetComponent<Animator>().SetTrigger(deadTriggerName);
            GetComponent<ActionScheduler>().CancelCurrentAction();
        }

        public abstract void CharaDeathBehaviour();
    }

}