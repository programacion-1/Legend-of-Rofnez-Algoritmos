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
            _weaponCollider.gameObject.SetActive(true);
            _weaponCollider._damageToDeal = _triggerDamage;
        }

        public void DeactivateWeaponCollider()
        {
            _weaponCollider.gameObject.SetActive(false);
            _weaponCollider._damageToDeal = 0;
        }

    }
}
