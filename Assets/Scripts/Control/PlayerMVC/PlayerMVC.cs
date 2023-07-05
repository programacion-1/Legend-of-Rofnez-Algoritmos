using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Movement;
using RPG.Combat;
using RPG.Core;
using RPG.InventorySystem;
using RPG.Magic;
using RPG.UI;
using System;

namespace RPG.MVC.Player
{
    public class PlayerMVC : MonoBehaviour
    {
        //MVC
        PlayerModel _playerModel;
        IController _controller;

        //Variables

        //Propiedades
        #region Model
        public bool GodMode{get; private set;}
        [field: SerializeField] public ActionScheduler ActionScheduler { get; private set; }
        [field: SerializeField] public PlayerHealth PlayerHealth { get; private set; }
        [field: SerializeField] public PlayerFighter PlayerFighter { get; private set; }
        [field: SerializeField] public CombatTarget PlayerCombatTarget { get; private set; }
        [field: SerializeField] public Mover PlayerMover { get; private set; }
        [field: SerializeField] public PlayerMagicCaster PlayerMagicCaster { get; private set; }
        [field: SerializeField] public ItemInventory ItemInventory { get; private set; }
        [field: SerializeField] public WeaponInventory WeaponInventory { get; private set; }
        public bool CanUseItem{get; private set;}
        public bool CanChangeWeapon{get; private set;}
        public Dictionary<KeyCode, int> ItemInventoryBar;
        #endregion

        #region View
        //Audio
        public AudioManager AudioManager{get; private set;}
        [field: SerializeField] public AudioClip RunClip{get; private set;}
        //UI
        public MenuController MenuController{get; private set;}
        #endregion

        void Awake()
        {
            SetItemInventoryBar();
            _playerModel = new PlayerModel(this);
            AudioManager = GameObject.FindObjectOfType<AudioManager>();
            MenuController = GameObject.FindObjectOfType<MenuController>();
            PlayerView playerView = new PlayerView(this);
            _playerModel.OnActivatingGodMode += playerView.CheckGodModeView;
            _playerModel.OnInteractingWithMenu += playerView.ShowOrHideMenu;
            _controller = new PlayerController(_playerModel);
        }

        void SetItemInventoryBar()
        {
            ItemInventoryBar = new Dictionary<KeyCode, int>();

            ItemInventoryBar.Add(KeyCode.Alpha1, 0);
            ItemInventoryBar.Add(KeyCode.Alpha2, 1);

        }
        // Start is called before the first frame update
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {
            if(_playerModel.CheckIfImDead()) return;
            _controller.ListenKeys();
            _controller.ListenFixedKeys();
        }

        void FixedUpdate()
        {
            if(_playerModel.CheckIfImDead()) return;
            //_controller.ListenFixedKeys();
        }

        #region EventManager
        private void OnEnable()
        {
            EventManager.SubscribeToEvent(EventManager.Events.Event_UseItem, UseItem);
            EventManager.SubscribeToEvent(EventManager.Events.Event_ChangeActiveWeapon, ChangeActiveWeapon);
            EventManager.SubscribeToEvent(EventManager.Events.Event_FindPlayerCombatTarget, FindPlayerCombatTarget);
            EventManager.SubscribeToEvent(EventManager.Events.Event_FindPlayerMagicTarget, FindPlayerMagicTarget);
        }

        private void OnDisable()
        {
            EventManager.UnsubscribeToEvent(EventManager.Events.Event_UseItem, UseItem);
            EventManager.UnsubscribeToEvent(EventManager.Events.Event_ChangeActiveWeapon, ChangeActiveWeapon);
            EventManager.UnsubscribeToEvent(EventManager.Events.Event_FindPlayerCombatTarget, FindPlayerCombatTarget);
            EventManager.UnsubscribeToEvent(EventManager.Events.Event_FindPlayerMagicTarget, FindPlayerMagicTarget);
        }

        #region UseItemEvent
        public void UseItem(params object[] p)
        {
            bool canUseItem = (bool) p[0];
            if(canUseItem) 
            {
                _playerModel.SetCanIUseItem(false);
                StartCoroutine(useItemCo());
            }           
        }

        private IEnumerator useItemCo()
        {
            yield return new WaitForSeconds(1f);
            _playerModel.SetCanIUseItem(true);
        }
        #endregion

        #region ChangeActiveWeaponEvent
        public void ChangeActiveWeapon(params object[] p)
        {
            bool canChangeWeapon = (bool) p[0];
            if(canChangeWeapon)
            {
                _playerModel.SetCanIChangeWeapon(false);
                StartCoroutine(changeWeaponCo());
            }
        }

        private IEnumerator changeWeaponCo()
        {
            yield return new WaitForSeconds(0.5f);
            _playerModel.SetCanIChangeWeapon(true);
        }
        #endregion

        #region FindPlayerMagicTarget
        public void FindPlayerMagicTarget(params object[] p)
        {
            RaycastHit hit = (RaycastHit) p[0];
            CombatTarget target = hit.transform.gameObject.GetComponent<CombatTarget>();
            if(target == null) return;
            else if(target == PlayerCombatTarget) PlayerMagicCaster.target = PlayerHealth;
            else PlayerMagicCaster.target = target.GetComponent<Health>();
            _playerModel.SetMagicTarget(PlayerMagicCaster.target);
        }
        #endregion

        #region FindPlayerCombatTarget
        public void FindPlayerCombatTarget(params object[] p)
        {
            RaycastHit hit = (RaycastHit) p[0];
            CombatTarget target = hit.transform.gameObject.GetComponent<CombatTarget>();
            _playerModel.SetCombatTarget(target);
        }
        #endregion

        #endregion
    }

}