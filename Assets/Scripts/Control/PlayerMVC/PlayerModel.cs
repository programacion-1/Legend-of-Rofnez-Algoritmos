using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Movement;
using RPG.Combat;
using RPG.Core;
using RPG.InventorySystem;
using RPG.Magic;
using System;

namespace RPG.MVC.Player
{
    public class PlayerModel : MonoBehaviour
    {
        private bool _godMode;
        private ActionScheduler _actionScheduler;
        private PlayerHealth _playerHealth;
        private PlayerFighter _playerFighter;
        private CombatTarget _playerCombatTarget;
        private PlayerMagicCaster _playerMagicCaster;
        private Mover _playerMover;
        private ItemInventory _itemInventory;
        private Dictionary<KeyCode, int> _itemInventoryBar;
        private WeaponInventory _weaponInventory;
        private bool _canUseItem;
        private bool _canChangeWeapon;

        //Events
        public event Action<bool> OnActivatingGodMode = delegate { };
        public event Action<bool> OnInteractingWithMenu = delegate { };

        //Model Only Variables
        private CombatTarget _target;

        public PlayerModel(PlayerMVC playerMVC)
        {
            _godMode = playerMVC.GodMode;
            _actionScheduler = playerMVC.ActionScheduler;
            _playerHealth = playerMVC.PlayerHealth;
            _playerFighter = playerMVC.PlayerFighter;
            _playerCombatTarget = playerMVC.PlayerCombatTarget;
            _playerMagicCaster = playerMVC.PlayerMagicCaster;
            _playerMover = playerMVC.PlayerMover;
            _itemInventory = playerMVC.ItemInventory;
            _itemInventoryBar = playerMVC.ItemInventoryBar;
            _weaponInventory = playerMVC.WeaponInventory;
            _canUseItem = playerMVC.CanUseItem;
            _canUseItem = true;
            _canChangeWeapon = playerMVC.CanChangeWeapon;
            _canChangeWeapon = true;
        }

        #region GodMode_Model

        public void CheckGodMode()
        {            
            if(_godMode) DisableGodMode();
            else EnableGodMode();
            OnActivatingGodMode(_godMode);
        }

        private void EnableGodMode()
        {
            _godMode = true;
            _playerHealth.EnableInvencibilityCheat();
        }

        private void DisableGodMode()
        {
            _godMode = false;
            _playerHealth.DisableInvencibility();
        }
        #endregion

        #region Health_Model
        public bool CheckIfImDead()
        {
            return _playerHealth.CheckIfIsDead();
        }
        #endregion

        #region Magic_Model
        public bool InteractWithMagic(RaycastHit[] hits)
        {
            if(_playerMagicCaster.currentMagic != null)
            {
                FindMagicTarget(hits);
                _playerMagicCaster.MagicAttack();
                return true;            
                /*foreach(RaycastHit hit in hits)
                {
                    CombatTarget target = hit.transform.gameObject.GetComponent<CombatTarget>();
                    if(target == null) continue;
                    if(target == GetComponent<CombatTarget>()) _playerMagicCaster.target = GetComponent<Health>();
                    _playerMagicCaster.target = target.GetComponent<Health>();
                }*/
                
            }
            else return false;
        }

        public void FindMagicTarget(RaycastHit[] hits)
        {
            foreach(RaycastHit hit in hits) TriggerFindPlayerCombatTargetEvent(hit);
        }

        public void SetMagicTarget(Health t)
        {
            _playerMagicCaster.target = t;
        }

        #endregion
        
        #region Combat_Model
        public void InteractWithCombat()
        {
            _playerFighter.Attack(_target.gameObject);
        }

        public bool FindCombatTarget(RaycastHit[] hits)
        {
            _target = null;
            foreach(RaycastHit hit in hits)
            {
                TriggerFindPlayerCombatTargetEvent(hit);
                if(_target != null) return true;
            }
            return false;
        }

        public void SetCombatTarget(CombatTarget t)
        {
            _target = null;
            if(t == null) return;
            if(t == _playerCombatTarget) return;
            if(!_playerFighter.CanAttack(t.gameObject)) return;
            _target = t;
        }

        #endregion

        #region Mover_Model
        public void Move(RaycastHit location)
        {
            _playerMover.StartMoveAction(location.point);
        }
        #endregion

        #region ItemInventory_Model
        public void UseItemOnInventory(KeyCode key)
        {
            if(_canUseItem) if(_itemInventoryBar.ContainsKey(key))
            {
                int item = _itemInventoryBar[key];
                if(item < _itemInventory.potions.Count) _itemInventory.UsePotionOnInventory(_itemInventory.potions[item]);
                TriggerUseItemEvent();
            }
        }

        public void SetCanIUseItem(bool value)
        {
            _canUseItem = value;
        }
        #endregion

        #region WeaponInventory_Model
        public void ChangeActiveWeapon()
        {
            if(_canChangeWeapon)
            {
                _weaponInventory.ChangeActiveWeapon();
                TriggerChangeActiveWeaponEvent();
            }
        }

        public void SetCanIChangeWeapon(bool value)
        {
            _canChangeWeapon = value;
        }
        #endregion

        #region InventoryMenu_Model
        public void InteractWithInventoryMenu(bool value)
        {
            OnInteractingWithMenu(value);
        }
        #endregion
        #region EventManager
        void TriggerUseItemEvent()
        {
            EventManager.TriggerEvent(EventManager.Events.Event_UseItem, _canUseItem);
        }

        void TriggerChangeActiveWeaponEvent()
        {
            EventManager.TriggerEvent(EventManager.Events.Event_ChangeActiveWeapon, _canChangeWeapon);
        }

        void TriggerFindPlayerMagicTargetEvent(RaycastHit hit)
        {
            EventManager.TriggerEvent(EventManager.Events.Event_FindPlayerMagicTarget, hit);
        }

        void TriggerFindPlayerCombatTargetEvent(RaycastHit hit)
        {
            EventManager.TriggerEvent(EventManager.Events.Event_FindPlayerCombatTarget, hit);
        }
        #endregion
    }
}
