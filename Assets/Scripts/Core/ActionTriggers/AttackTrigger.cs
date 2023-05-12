using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core
{
    public class AttackTrigger : MonoBehaviour
    {
        [SerializeField] DamageTrigger _weaponCollider;
        public float _triggerDamage{get; set;}


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
