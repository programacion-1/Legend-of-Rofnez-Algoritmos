using System;
using RPG.Core;
using UnityEngine;

namespace RPG.Item
{
    [CreateAssetMenu(fileName = "MeleeWeapon", menuName = "My Scriptable Objects/Weapons/Make New Melee Weapon", order = 0)]
    public class MeleeWeapon : Weapon
    {
        AttackTrigger _weaponTrigger;
        public override void SetEquippedWeaponDamage()
        {
            _weaponTrigger._triggerDamage = _weaponDamage;
        }

        public override void WeaponAttack(Health t)
        {
            _weaponTrigger.ActivateWeaponCollider();    
        }

        public override void StopAttack()
        {
            _weaponTrigger.DeactivateWeaponCollider();    
        }

        public override void SpawnSettings(GameObject spawnedWeapon)
        {
            _weaponTrigger = spawnedWeapon.GetComponent<AttackTrigger>();
        }
    }
}
