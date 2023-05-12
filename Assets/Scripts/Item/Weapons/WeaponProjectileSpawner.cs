using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Core;

public class WeaponProjectileSpawner : MonoBehaviour
{
    [SerializeField] Transform _projectileSpawner;
    public Transform projectileSpawner{get{return _projectileSpawner;}}
    Health _target;
    float _projectileDamage;
    public float projectileDamage{get{return _projectileDamage;} set{_projectileDamage = value;}}
    Color _projectileColorTrail;
    public Color projectileColorTrail{get{return _projectileColorTrail;} set{_projectileColorTrail = value;}}

    public void LaunchProjectile(Health t, int layerToSet)
    {
        _target = t;
        Projectile projectileInstance = ArrowProjectileFactory.Instance.GetObject();
        projectileInstance.transform.position = _projectileSpawner.transform.position;
        projectileInstance.transform.rotation = Quaternion.identity;
        projectileInstance.gameObject.layer = layerToSet;
        Debug.Log(projectileInstance.gameObject.layer);
        projectileInstance.SetProjectileTarget(_target, _target.gameObject.layer);
        projectileInstance.SetProjectileDamage(_projectileDamage);
        projectileInstance.SetProjectileTrail(projectileColorTrail);
    }
}
