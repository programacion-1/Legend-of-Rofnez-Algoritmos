using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Core;
using RPG.InventorySystem;
using RPG.Item;

namespace RPG.GameCore
{
    public class PlayerStatsManager : MonoBehaviour
    {
        #region PlayerClasses
        private PlayerHealth playerHealth;
        private ItemInventory playerItemInventory;
        private WeaponInventory playerWeaponInventory;
        #endregion

        #region PlayerStats
        //Health
        public float _psHealth = 0;
        public float _psMaxHealth = 0;

        //Item Inventory
        public Dictionary<string, PotionContainer> _psPotionCollection;
        public List<string> _psPotionKeys;

        //Weapon Inventory
        public MeleeWeapon _psCurrentMeleeWeapon;
        public RangedWeapon _psCurrentRangedWeapon;
        public int _psRangedWeaponAmmo;
        public Weapon _psActiveWeapon;
        public Transform _psRightHand;
        public Transform _psLeftHand;
        #endregion

        public void SetMainPlayerStats(PlayerHealth player)
        {
            playerHealth = player;
            playerItemInventory = player.GetComponent<ItemInventory>();
            playerWeaponInventory = player.GetComponent<WeaponInventory>();
        }

        public void SaveMainPlayerStats()
        {
            #region Health
            _psHealth = playerHealth.GetHP();
            _psMaxHealth = playerHealth.GetMaxHP();
            #endregion

            #region ItemInventory
            _psPotionCollection = playerItemInventory.potionCollection;
            _psPotionKeys = playerItemInventory.potions;
            #endregion

            #region WeaponInventory
            _psCurrentMeleeWeapon = playerWeaponInventory.equippedMeleeWeapon;
            _psCurrentRangedWeapon = playerWeaponInventory.equippedRangedWeapon;
            _psRangedWeaponAmmo = playerWeaponInventory.rangedWeaponAmmo;
            _psActiveWeapon = playerWeaponInventory.activeWeapon;
            _psRightHand = playerWeaponInventory.rightHand;
            _psLeftHand = playerWeaponInventory.leftHand;
            #endregion
        }

        public void LoadMainPlayerStats(PlayerHealth player)
        {
            SetMainPlayerStats(player);

            #region Health
            playerHealth.SetHP(_psHealth);
            playerHealth.SetMaxHP(_psMaxHealth);
            #endregion

            #region ItemInventory
            playerItemInventory.potionCollection = _psPotionCollection;
            playerItemInventory.potions = _psPotionKeys;
            #endregion

            #region WeaponInventory
            playerWeaponInventory.equippedMeleeWeapon = _psCurrentMeleeWeapon;
            playerWeaponInventory.equippedRangedWeapon = _psCurrentRangedWeapon;
            playerWeaponInventory.rangedWeaponAmmo = _psRangedWeaponAmmo;
            playerWeaponInventory.SetActiveWeapon(_psActiveWeapon);
            playerWeaponInventory.rightHand = _psRightHand;
            playerWeaponInventory.leftHand = _psLeftHand;
            #endregion
        }
    }

}