using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Core;

namespace RPG.Obstacle
{
    public class FireTrap : Trap
    {
        private ParticleSystem particle;
        BoxCollider objCollider;
        DamageTrigger _damageTrigger;
        [Header("Fire Trap Settings")]
        [SerializeField] float _trapFireRank;
        [SerializeField] float _trapDamage;

        public override void ChildStartSettings()
        {
            particle = GetComponentInChildren<ParticleSystem>();
            objCollider = GetComponent<BoxCollider>();
            _damageTrigger = GetComponent<DamageTrigger>();
        }

        public override void TrapActivatedBehaviour()
        {
            //Se activa el efecto de la trampa
            TrapEffect();
            _currentTime -=Time.deltaTime;

            if(_currentTime<=0)
            {
                //Al llegar el tiempo a 0, apago la partícula, seteo el fireRank de la trampa, apago el trigger y arranco la corrutina para reactivar la trampa
                DeactivatedSettings();
                StartCoroutine("waitToReactivate");
            }
        }

        public void DeactivatedSettings()
        {
            particle.enableEmission = false;
            fireRank = _trapFireRank;
            objCollider.enabled = false;
        }

        public override void TrapDeactivatedBehaviour()
        {
            DeactivatedSettings();
            StopCoroutine("waitToReactivate");
        }

        public override void TrapEffect()
        {
            _damageTrigger._damageToDeal = _trapDamage;
        }

        //Corrutina para reactivar la trampa
        public override IEnumerator waitToReactivate()
        {
            yield return new WaitForSeconds(waitTimeTrapDeactivater);
            objCollider.enabled = true;
            particle.enableEmission = true;
            fireRank += Time.deltaTime;
            Mathf.Clamp(fireRank, 0, 4);
            particle.startLifetime = fireRank;
            _currentTime = time;
        }
    }

}