using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Item;

namespace RPG.Combat
{
    public class RangedEnemyFighter : EnemyFighter
    {
        WeaponProjectileSpawner _weaponProjectileSpawner;
        public override void ChildrenAttack()
        {
            _weaponProjectileSpawner.LaunchProjectile(_target, gameObject.layer);
        }

        public override void ChildrenDeactivateAttack()
        {
            
        }

        public override void SetSpawnedWeaponDamage(float damage)
        {
            _weaponProjectileSpawner = spawnedWeapon.GetComponent<WeaponProjectileSpawner>();
            _weaponProjectileSpawner.projectileDamage = damage;
            RangedWeapon w = (RangedWeapon) _currentWeapon;
            _weaponProjectileSpawner.projectileColorTrail = w.trailColor;
        }
    }
}