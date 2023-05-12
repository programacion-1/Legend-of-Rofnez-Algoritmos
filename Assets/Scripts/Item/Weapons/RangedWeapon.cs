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
        public Projectile projectile{get{return _projectile;}}
        float _projectileDamage;
        [SerializeField] int _ammo;
        public int ammo{get{return _ammo;} set{_ammo = value;}}
    }
}
