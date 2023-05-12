using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Item;
using RPG.InventorySystem;

namespace RPG.Combat
{
    public class PlayerFighter : Fighter
    {
        WeaponInventory _weaponInventory;

        public override bool AttackAvaivalbleByTimer()
        {
            return _timeSinceLastAttack >= _weaponInventory.activeWeapon.timeBetweenAttacks;
        }

        public override void ChildFighterSettings()
        {
            _weaponInventory = GetComponent<WeaponInventory>();
            _weaponInventory.rightHand = _rightHandTransform;
            _weaponInventory.leftHand = _leftHandTransform;
            _weaponInventory.defaultAnimatorController = _defaultRuntimeAnimatorController;
            _weaponInventory.EquipMeeleWeapon((MeleeWeapon) _defaultWeapon);
        }

        public override void ChildrenAttack()
        {
            _weaponInventory.SelectAttack(_target);
        }

        public override void ChildrenDeactivateAttack()
        {
            _weaponInventory.StopAttack();
        }

        public override bool GetIsInRange()
        {
            return Vector3.Distance(transform.position, _target.transform.position) < _weaponInventory.activeWeapon.weaponRange;
        }
    }

}