using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Core;
using GeneralEnums;

namespace RPG.Magic
{
    [CreateAssetMenu(fileName = "Weapon", menuName = "My Scriptable Objects/Magics/New Magic Projectile Shooter", order = 1)]
    public class MagicShooter : Magic
    {
        [SerializeField] Projectile _projectile;
        [SerializeField] MagicProjectileSpawner _magicProjectileSpawner;

        public override void InstantiateMagic(Transform t, Health h)
        {
            LaunchProjectile(t, h);
        }

        public void LaunchProjectile(Transform t, Health h)
        {
            Debug.Log(t.position);
            Projectile projectileInstance = Instantiate(_projectile, t.position, Quaternion.identity);
            Debug.Log(projectileInstance.transform.position);
            projectileInstance.SetProjectileTarget(h, t.gameObject.layer);
            projectileInstance.SetProjectileDamage(magicDamage);
        }

        public override void SetMagicType()
        {
            _magicType = MagicType.Projectile;
            _magicTypeName = MagicType.Projectile.ToString();
        }

    }
}
