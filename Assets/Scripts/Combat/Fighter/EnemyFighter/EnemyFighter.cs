using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Item;

namespace RPG.Combat
{
    public abstract class EnemyFighter : Fighter
    {
        [SerializeField] protected Weapon _currentWeapon = null;
        protected GameObject spawnedWeapon;
        public override bool AttackAvaivalbleByTimer()
        {
            return _timeSinceLastAttack >= _currentWeapon.timeBetweenAttacks;
        }

        public override void ChildFighterSettings()
        {
            EquipWeapon(_defaultWeapon);
        }
        public override bool GetIsInRange()
        {
            return Vector3.Distance(transform.position, _target.transform.position) < _currentWeapon.weaponRange;
        }

        public void EquipWeapon(Weapon weapon)
        {
            if(weapon == null) return;
            _currentWeapon = weapon;
            _anim.runtimeAnimatorController = _defaultRuntimeAnimatorController;
            spawnedWeapon = weapon.Spawn(_rightHandTransform, _leftHandTransform, _anim);
            SetSpawnedWeaponDamage(_currentWeapon.weaponDamage);
        }

        public abstract void SetSpawnedWeaponDamage(float damage);

    }
}
