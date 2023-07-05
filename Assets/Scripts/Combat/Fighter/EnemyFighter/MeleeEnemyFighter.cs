using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Item;
using RPG.Core;

namespace RPG.Combat
{
    public class MeleeEnemyFighter : EnemyFighter
    {
        AttackTrigger _attackTrigger;
        [SerializeField] DamageTrigger _enemyDamageTrigger;

        public override void ChildrenAttack()
        {
            _attackTrigger.ActivateWeaponCollider();
        }

        public override void ChildFighterSettings()
        {
            base.ChildFighterSettings();
            _enemyDamageTrigger.gameObject.SetActive(false);
        }

        public override void ChildrenDeactivateAttack()
        {
            _attackTrigger.DeactivateWeaponCollider();
        }

        public override void SetSpawnedWeaponDamage(float damage)
        {
            _attackTrigger = spawnedWeapon.GetComponent<AttackTrigger>();
            _attackTrigger._triggerDamage = damage;
            _attackTrigger.SetWeaponCollider(_enemyDamageTrigger);
            _enemyDamageTrigger.SetDamageTriggerColliderStats(_attackTrigger.damageTriggerCenter, _attackTrigger.damageTriggerSize);
        }
    }
}
    
