using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Item;
using RPG.Core;
using RPG.UI;

namespace RPG.InventorySystem
{
    public class WeaponInventory : MonoBehaviour
    {
        [SerializeField] MeleeWeapon _equippedMeleeWeapon;
        public MeleeWeapon equippedMeleeWeapon{get{return _equippedMeleeWeapon;} set{_equippedMeleeWeapon = value;}}
        [SerializeField] RangedWeapon _equippedRangedWeapon;
        public RangedWeapon equippedRangedWeapon{get{return _equippedRangedWeapon;} set{_equippedRangedWeapon = value;}}
        [SerializeField] int _rangedWeaponAmmo;
        public int rangedWeaponAmmo{get{return _rangedWeaponAmmo;} set{_rangedWeaponAmmo = value;}}
        [SerializeField] Weapon _activeWeapon;
        GameObject currentWeapon;
        public Weapon activeWeapon{get{return _activeWeapon;}}
        Transform _rightHand;
        public Transform rightHand{get{return _rightHand;} set{_rightHand = value;}}
        Transform _leftHand;
        public Transform leftHand{get{return _leftHand;} set{_leftHand = value;}}
        Transform _rangedWeaponProjectileSpawner;
        [SerializeField] AttackTrigger _attackTrigger;
        [SerializeField] DamageTrigger _playerDamageTrigger;
        [SerializeField] WeaponProjectileSpawner _weaponProjectileSpawner;
        RuntimeAnimatorController _defaultAnimatorController;
        public RuntimeAnimatorController defaultAnimatorController{get{return _defaultAnimatorController;} set{_defaultAnimatorController = value;}}
        WeaponInventorMenu _weaponInventorMenu;

        void Start()
        {
            _weaponInventorMenu = FindObjectOfType<WeaponInventorMenu>();
            _playerDamageTrigger.gameObject.SetActive(false);
        }

        public void SetActiveWeapon(Weapon weapon)
        {
            GetComponent<Animator>().runtimeAnimatorController = _defaultAnimatorController;
            GetComponent<ActionScheduler>().CancelCurrentAction();
            if(_activeWeapon == null) _activeWeapon = _equippedMeleeWeapon;
            else _activeWeapon = weapon;
            currentWeapon = _activeWeapon.Spawn(_rightHand, _leftHand, this.GetComponent<Animator>());
        }

        public void EquipMeeleWeapon(MeleeWeapon weapon)
        {
            _equippedMeleeWeapon = weapon;
            _weaponInventorMenu.inventoryMeleeWeaponSprite = weapon.weaponSprite;
            _weaponInventorMenu.inventoryMeleeWeaponText = weapon.name;
            _weaponInventorMenu.SetMeleeWeaponSprite();
            SetActiveWeapon(_equippedMeleeWeapon);
            //_weaponInventorMenu.SetCurrentWeaponActive(0);
            _weaponInventorMenu.CurrentMeleeWeaponSprite = weapon.weaponSprite;
            _weaponInventorMenu.SetCurrentActiveWeapon(weapon.weaponSprite);
            _attackTrigger = currentWeapon.GetComponent<AttackTrigger>();
            _attackTrigger.SetWeaponCollider(_playerDamageTrigger);
            _playerDamageTrigger.SetDamageTriggerColliderStats(_attackTrigger.damageTriggerCenter, _attackTrigger.damageTriggerSize);
            _attackTrigger._triggerDamage = _activeWeapon.weaponDamage;
        }

        public void EquipRangedWeapon(RangedWeapon weapon)
        {
            _equippedRangedWeapon = weapon;
            _weaponInventorMenu.inventoryRangedWeaponSprite = weapon.weaponSprite;
            _weaponInventorMenu.inventoryRangedWeaponText = weapon.name;
            _weaponInventorMenu.SetRangedWeaponSprite();
            SetActiveWeapon(_equippedRangedWeapon);
            RangedWeapon w = (RangedWeapon) _activeWeapon;
            _rangedWeaponAmmo = w.ammo;
            _weaponProjectileSpawner = currentWeapon.GetComponent<WeaponProjectileSpawner>();
            _weaponProjectileSpawner.projectileDamage = _activeWeapon.weaponDamage;
            _weaponProjectileSpawner.projectileColorTrail = w.trailColor;
            _weaponInventorMenu.CurrentRangedWeaponSprite = weapon.weaponSprite;
            _weaponInventorMenu.SetCurrentActiveWeapon(weapon.weaponSprite, w.ammo.ToString());
            _weaponInventorMenu.inventoryRangedWeaponAmmoText = _rangedWeaponAmmo.ToString();
        }

        public void SelectAttack(Health target = null)
        {
            if(_activeWeapon == _equippedMeleeWeapon) MeleeWeaponAttack();
            else RangedWeaponAttack(target);
        }

        public void StopAttack()
        {
            if(_activeWeapon == _equippedMeleeWeapon) _attackTrigger.DeactivateWeaponCollider();
        }

        public void MeleeWeaponAttack()
        {
            _attackTrigger.ActivateWeaponCollider();
        }

        public void RangedWeaponAttack(Health t)
        {
            if(CheckIfICanShoot())
            {
                _weaponProjectileSpawner.LaunchProjectile(t, gameObject.layer);
                _rangedWeaponAmmo -= 1;
                if(_rangedWeaponAmmo <= 0)
                {
                    _weaponInventorMenu.SetAmmoText("X");
                    _weaponInventorMenu.inventoryRangedWeaponAmmoText = "X";
                } 
                else
                {
                    _weaponInventorMenu.SetAmmoText(_rangedWeaponAmmo.ToString());
                    _weaponInventorMenu.inventoryRangedWeaponAmmoText = _rangedWeaponAmmo.ToString();
                } 
            }
        }

        public bool CheckIfICanShoot()
        {
            if(_rangedWeaponAmmo > 0) return true;
            else return false;
        }
        public void ChangeActiveWeaponToMelee()
        {
            if(_activeWeapon == _equippedRangedWeapon && _equippedMeleeWeapon != null) EquipMeeleWeapon(_equippedMeleeWeapon);
        }

        public void ChangeActiveWeaponToRanged()
        {
            if (_activeWeapon == _equippedMeleeWeapon && _equippedRangedWeapon != null) EquipRangedWeapon(_equippedRangedWeapon);
        }
    }
}