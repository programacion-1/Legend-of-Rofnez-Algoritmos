using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Core;


namespace RPG.Magic
{
    public class MagicProjectileSpawner : MagicSpawner
    {
        Health _target;
        public Health target{get{return _target;} set{_target = value;}}
        float _magicProjectileDamage;
        public float magicProjectileDamage{get{return _magicProjectileDamage;} set{_magicProjectileDamage = value;}}
        
        public override void CastMagic(params object[] p)
        {
            _target = (Health) p[0];
            int layerToSet = (int) p[1];
            base.CastMagic();
            FireballProjectile projectileInstance = FireballProjectileFactory.Instance.GetObject();
            projectileInstance.transform.position = _spawnerTransform.transform.position;
            projectileInstance.transform.rotation = Quaternion.identity;
            projectileInstance.gameObject.layer = layerToSet;
            projectileInstance.SetProjectileTarget(_target, _target.gameObject.layer);
            projectileInstance.SetProjectileDamage(_magicProjectileDamage);
        }
    }
}
