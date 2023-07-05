using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core
{
    public abstract class CharaHealth : Health
    {
        [Header("Character Health Stats")]
        [SerializeField] protected float _damageRate;
        [SerializeField] protected float _returnToWhiteColourTimer;
        [SerializeField] Renderer charaRenderer;
        [SerializeField] Transform _shaderSpawnpoint;
        [SerializeField] protected GameObject hitFX;
        [SerializeField] protected GameObject bloodFX;
        [SerializeField] protected string _deadTriggerName;
        
        public override void ChildrenDamageBehavior()
        {
            CharaDamageBehaviour();
        }

        public void InstantiateVFX(GameObject vfx)
        {
            GameObject.Instantiate(vfx, _shaderSpawnpoint.position, _shaderSpawnpoint.rotation);
        }

        public virtual void CharaDamageBehaviour()
        {
            InstantiateVFX(hitFX);
            InstantiateVFX(bloodFX);
            audioManager.TryToPlayClip(audioManager.PlayerSFXSources, impactClipSound);
            audioManager.TryToPlayClip(audioManager.PlayerSFXSources, damageClipSound);            
            EnableInvencibility(_damageRate);
        }

        public override void DeathBehaviour()
        {
            CharaDeathBehaviour();
        }

        public virtual void CharaDeathBehaviour()
        {
            audioManager.TryToPlayClip(audioManager.PlayerSFXSources, deadClipSound);
            GetComponent<Animator>().SetTrigger(_deadTriggerName);
            GetComponent<ActionScheduler>().CancelCurrentAction();
        }

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

        
        public void Heal(float healPoints)
        {
            SetHP(Mathf.Min(GetHP() + healPoints, GetMaxHP()));
            HealVisualSettings();
        }

        public virtual void HealVisualSettings()
        {
            Debug.Log($"Heal!");
        }

        
    }
}