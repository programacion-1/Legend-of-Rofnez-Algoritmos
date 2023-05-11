using System;
using RPG.Core;
using UnityEngine;

namespace RPG.Item
{
    [CreateAssetMenu(fileName = "RangedWeapon", menuName = "My Scriptable Objects/Weapons/Make New Ranged Weapon", order = 1)]
    public class RangedWeapon : Weapon
    {
        [Header("Ranged Weapon Properties")]
        [SerializeField] Projectile _projectile;
        float _projectileDamage;
        [SerializeField] int _ammo;
        public int ammo{get{return _ammo;} set{_ammo = value;}}

        public override void SetEquippedWeaponDamage()
        {
            _projectileDamage = _weaponDamage;
        }

        public override void WeaponAttack(Health t)
        {
            LaunchProjectile(_rightHand, _leftHand, t);
        }

        public override void StopAttack()
        {
            
        }

        public void LaunchProjectile(Transform rightHand, Transform leftHand, Health t)
        {
            Projectile projectileInstance = Instantiate(_projectile, GetTransform(rightHand, leftHand).position, Quaternion.identity);
            projectileInstance.SetProjectileTarget(t, t.gameObject.layer);
            projectileInstance.SetProjectileDamage(_projectileDamage);
        }

        public override void SpawnSettings(GameObject spawnedWeapon)
        {
            
        }
    }
}
