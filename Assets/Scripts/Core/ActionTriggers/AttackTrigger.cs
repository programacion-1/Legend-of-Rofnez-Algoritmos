using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core
{
    public class AttackTrigger : MonoBehaviour
    {
        [SerializeField] DamageTrigger _weaponCollider;
        public float _triggerDamage{get; set;}
        [SerializeField] Vector3 _damageTriggerCenter = Vector3.zero;
        public Vector3 damageTriggerCenter{get{return _damageTriggerCenter;}}
        [SerializeField] Vector3 _damageTriggerSize = Vector3.zero;
        public Vector3 damageTriggerSize{get{return _damageTriggerSize;}}

        public void SetWeaponCollider(DamageTrigger trigger)
        {
            _weaponCollider = trigger;
        }


        public void ActivateWeaponCollider()
        {
            _weaponCollider._damageToDeal = _triggerDamage;
            _weaponCollider.gameObject.SetActive(true);
        }

        public void DeactivateWeaponCollider()
        {
            _weaponCollider._damageToDeal = 0;
            _weaponCollider.gameObject.SetActive(false);
        }

    }
}
