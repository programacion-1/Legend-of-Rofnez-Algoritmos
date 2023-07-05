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
        private PlayerMagicCaster _playerMagicCaster;
        private ItemInventory _itemInventory;
        private Dictionary<KeyCode, int> _itemInventoryBar;
        private WeaponInventory _weaponInventory;
        private bool _canUseItem;
        private bool _canChangeWeapon;

        //Events
        public event Action<bool> OnActivatingGodMode = delegate { };

        public PlayerModel(PlayerMVC playerMVC)
        {
            _godMode = playerMVC.GodMode;
            _actionScheduler = playerMVC.ActionScheduler;
            _playerHealth = playerMVC.PlayerHealth;
            _playerFighter = playerMVC.PlayerFighter;
            _playerMagicCaster = playerMVC.PlayerMagicCaster;
            _itemInventory = playerMVC.ItemInventory;
            _itemInventoryBar = playerMVC.ItemInventoryBar;
            _weaponInventory = playerMVC.WeaponInventory;
            _canUseItem = playerMVC.CanUseItem;
            _canUseItem = true;
            _canChangeWeapon = playerMVC.CanChangeWeapon;
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

        #region EventManager
        void TriggerUseItemEvent()
        {
            EventManager.TriggerEvent(EventManager.Events.Event_UseItem, _canUseItem);
        }
        #endregion
    }
}
