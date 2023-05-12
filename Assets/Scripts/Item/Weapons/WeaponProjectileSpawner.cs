using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Core;

public class WeaponProjectileSpawner : MonoBehaviour
{
    Projectile _projectileToSpawn;
    public Projectile projectileToSpawn{get{return _projectileToSpawn;} set{_projectileToSpawn = value;}}
    [SerializeField] Transform _projectileSpawner;
    public Transform projectileSpawner{get{return _projectileSpawner;}}
    Health _target;
    float _projectileDamage;
    public float projectileDamage{get{return _projectileDamage;} set{_projectileDamage = value;}}

    public void LaunchProjectile(Health t)
    {
        _target = t;
        Projectile projectileInstance = Instantiate(_projectileToSpawn, _projectileSpawner.position, Quaternion.identity);
        projectileInstance.SetProjectileTarget(_target, _target.gameObject.layer);
        projectileInstance.SetProjectileDamage(_projectileDamage);
    }
}
