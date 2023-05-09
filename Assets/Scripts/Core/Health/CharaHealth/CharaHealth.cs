using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core
{
    public abstract class CharaHealth : Health
    {
        [Header("Character Health Stats")]
        [SerializeField] float _damageRate;
        [SerializeField] float _returnToWhiteColourTimer;
        [SerializeField] Renderer charaRenderer;
        [SerializeField] Transform shaderSpawnpoint;
        [SerializeField] protected GameObject hitFX;
        [SerializeField] protected GameObject bloodFX;
        [SerializeField] string deadTriggerName;

        public override void ChildrenDamageBehavior()
        {
            CharaDamageBehaviour();
            GameObject.Instantiate(hitFX, shaderSpawnpoint.position, shaderSpawnpoint.rotation);
            GameObject.Instantiate(bloodFX, shaderSpawnpoint.position, shaderSpawnpoint.rotation);
            audioManager.TryToPlayClip(audioManager.PlayerSFXSources, impactClipSound);
            audioManager.TryToPlayClip(audioManager.PlayerSFXSources, damageClipSound);            
            EnableInvencibility(_damageRate);
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

        //Clase para definir el color del material del renderer
        public void ChangeRendererColor(Color color)
        {
            if(charaRenderer.material.color != color)
            {
                charaRenderer.material.color = color;
                if(charaRenderer.material.color != Color.white) StartCoroutine("BackToNormalRendererColor");
            } 
        }

        //Corrutina para devolver el color normal <blanco> al material del renderer
        protected IEnumerator BackToNormalRendererColor()
        {
            yield return new WaitForSeconds(_returnToWhiteColourTimer);
            ChangeRendererColor(Color.white);
        }
    }

}