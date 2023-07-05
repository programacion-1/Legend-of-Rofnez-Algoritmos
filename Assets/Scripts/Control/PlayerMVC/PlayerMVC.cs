using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Movement;
using RPG.Combat;
using RPG.Core;
using RPG.InventorySystem;
using RPG.Magic;
using RPG.UI;

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
        }

        void FixedUpdate()
        {
            if(_playerModel.CheckIfImDead()) return;
            _controller.ListenFixedKeys();
        }

         #region EventManager
        private void OnEnable()
        {
            EventManager.SubscribeToEvent(EventManager.Events.Event_UseItem, UseItem);
        }

        private void OnDisable()
        {
            EventManager.UnsubscribeToEvent(EventManager.Events.Event_UseItem, UseItem);
        }

        public void UseItem(params object[] p)
        {
            bool canUseItem = (bool) p[0];
            if(canUseItem) 
            {
                _playerModel.SetCanIUseItem(false);
                StartCoroutine(useItem());
            }
            
        }

        private IEnumerator useItem()
        {
            yield return new WaitForSeconds(1f);
            _playerModel.SetCanIUseItem(true);
        }
        #endregion
    }

}