using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Core;
using RPG.InventorySystem;
using RPG.Item;
using RPG.Magic;

namespace RPG.GameCore
{
    public class PlayerStatsManager : MonoBehaviour
    {
        #region PlayerClasses
        private PlayerHealth playerHealth;
        private MagicPoints playerMagicPoints;
        private ItemInventory playerItemInventory;
        private WeaponInventory playerWeaponInventory;
        #endregion

        #region PlayerStats
        //Health
        public float _psHealth = 0;
        public float _psMaxHealth = 0;

        //Magic Points
        public float _psMagicPoints = 0;
        public float _psMaxMagicPoints = 0;

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

        //Misc
        public Vector3 _psPos;

        #endregion

        public void SetMainPlayerStats(PlayerHealth player)
        {
            playerHealth = player;
            playerMagicPoints = player.GetComponent<MagicPoints>();
            playerItemInventory = player.GetComponent<ItemInventory>();
            playerWeaponInventory = player.GetComponent<WeaponInventory>();
        }

        public void SaveMainPlayerStats()
        {
            #region Health
            _psHealth = playerHealth.GetHP();
            _psMaxHealth = playerHealth.GetMaxHP();
            #endregion

            #region MagicPoints
            _psMagicPoints = playerMagicPoints.magicPoints;
            _psMaxMagicPoints = playerMagicPoints.maxMagicPoints;
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

            #region Misc
            _psPos = playerHealth.transform.position;
            #endregion
        }

        public void LoadMainPlayerStats(PlayerHealth player)
        {
            SetMainPlayerStats(player);

            #region Health
            playerHealth.SetHP(_psHealth);
            playerHealth.SetMaxHP(_psMaxHealth);
            #endregion

            #region MagicPoints
            playerMagicPoints.magicPoints = _psMagicPoints;
            playerMagicPoints.maxMagicPoints = _psMaxMagicPoints;
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

            #region Misc
            playerHealth.transform.position = _psPos;
            #endregion
        }
    }

}