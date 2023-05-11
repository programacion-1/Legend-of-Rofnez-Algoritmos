using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core
{
    public class RangedWeaponAmmoInventory : MonoBehaviour
    {
        [SerializeField] int _ammo;
        public int ammo{get{return _ammo;}}
        int maxAmmo = 99;

        public void SetAmmo(int newAmmo)
        {
            if (newAmmo > maxAmmo) _ammo = maxAmmo;
            else _ammo = newAmmo;
        }
    }

}
