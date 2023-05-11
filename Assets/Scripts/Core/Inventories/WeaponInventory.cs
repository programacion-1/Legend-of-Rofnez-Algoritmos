using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Item;

namespace RPG.Core
{
    public class WeaponInventory : MonoBehaviour
    {
        [SerializeField] MeleeWeapon _equippedMeleeWeapon;
        public MeleeWeapon equippedMeleeWeapon{get{return _equippedMeleeWeapon;} set{_equippedMeleeWeapon = value;}}
        [SerializeField] RangedWeapon _equippedRangedWeapon;
        public RangedWeapon equippedRangedWeapon{get{return _equippedRangedWeapon;} set{_equippedRangedWeapon = value;}}
        [SerializeField] Weapon _activeWeapon;
        public Weapon activeWeapon{get{return _activeWeapon;}}

        public void SetActiveWeapon(Weapon weapon)
        {
            if(_activeWeapon == null)
            {
                _activeWeapon = _equippedMeleeWeapon;
                return;
            }
            else
            {
                _activeWeapon = weapon;
            }
        }
    }
}