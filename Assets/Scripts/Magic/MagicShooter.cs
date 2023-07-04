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

        public override MagicSpawner InstantiateMagicSpawner(params object[] p)
        {
            Transform t = (Transform) p[0];
            GameObject magicSpawner = Instantiate(_magicProjectileSpawner.gameObject, t.position, t.rotation);
            magicSpawner.GetComponent<MagicProjectileSpawner>().magicProjectileDamage = magicDamage;
            return magicSpawner.GetComponent<MagicProjectileSpawner>();
        }

        public override void SetMagicType()
        {
            _magicType = MagicType.Projectile;
            _magicTypeName = MagicType.Projectile.ToString();
        }

    }
}
