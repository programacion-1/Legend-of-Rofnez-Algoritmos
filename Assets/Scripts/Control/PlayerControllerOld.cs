using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GeneralEnums;
using RPG.Movement;
using RPG.Combat;
using RPG.Core;
using RPG.InventorySystem;
using RPG.Magic;
using RPG.UI;
using System;

namespace RPG.Control
{
    //Funcion dedicada para el control del player
    public class PlayerControllerOld : MonoBehaviour
    {
        PlayerHealth _health;
        Fighter _fighter;
        PlayerMagicCaster _magicCaster;
        ItemInventory _itemInventory;
        WeaponInventory _weaponInventory;
        MenuController _menuController;
        private bool _godMode;
        bool canUseItem = true;
        bool canChangeWeapon = true;
        
        // Start is called before the first frame update
        void Start()
        {
            _health = GetComponent<PlayerHealth>();
            _fighter = GetComponent<Fighter>();
            _magicCaster = GetComponent<PlayerMagicCaster>();
            _itemInventory = GetComponent<ItemInventory>();
            _weaponInventory = GetComponent<WeaponInventory>();
            _menuController = FindObjectOfType<MenuController>();
        }

        // Update is called once per frame
        void Update()
        {
            if(_health.CheckIfIsDead()) return;
            #region Inputs de Testing
            //if(Input.GetKeyDown(KeyCode.F1)) _health.TakeDamage(10f);
            //if(Input.GetKeyDown(KeyCode.F1)) ArrowProjectileFactory.Instance.GetObject();
            #endregion
            //Input para activar y desactivar el God Mode
            /*if(Input.GetKeyDown(KeyCode.F))
            {
                if(_godMode) DisableGodMode();
                else EnableGodMode();
            }*/
            #region Inputs para consumir pociones. Por el momento, hardcodeado
            /*if(canUseItem)
            {
                if(Input.GetKeyDown(KeyCode.Alpha1))
                {
                    UseItemOnInventory(0);
                }

                if(Input.GetKeyDown(KeyCode.Alpha2))
                {
                    UseItemOnInventory(1);
                }
            }*/
            #endregion
            //Input para cambiar de arma activa
            //if(canChangeWeapon) if(Input.GetKeyDown(KeyCode.Q)) StartCoroutine("SetPlayerActiveWeapon");
            //InteractWithInventoryMenu();
            //if(InteractWithMagic()) return;
            //if(InteractWithCombat()) return;
            //if(InteractWithMovement()) return;
        }

        #region God Mode
        private void EnableGodMode()
        {
            _godMode = true;
            _health.EnableInvencibilityCheat();
            _menuController.ShowUIObject(_menuController.GetGodModeText());
        }

        private void DisableGodMode()
        {
            _godMode = false;
            _health.DisableInvencibility();
            _menuController.HideUIObject(_menuController.GetGodModeText());
        }
        #endregion

        #region Magic
        private bool InteractWithMagic()
        {
            if(Input.GetMouseButtonDown(1) && _magicCaster.currentMagic != null)
            {
                RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());
                foreach(RaycastHit hit in hits)
                {
                    CombatTarget target = hit.transform.gameObject.GetComponent<CombatTarget>();
                    if(target == null) continue;
                    if(target == GetComponent<CombatTarget>()) _magicCaster.target = GetComponent<Health>();
                    _magicCaster.target = target.GetComponent<Health>();
                }
                _magicCaster.MagicAttack();
                return true;
            }
            else return false;
        }
        #endregion

        #region Combat
        // Busco con raycast si encuentro un objetivo para pelear, chequeo si puedo atacarlo y si hago click, lo ataco
        private bool InteractWithCombat()
        {
            RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());
            foreach(RaycastHit hit in hits)
            {
                CombatTarget target = hit.transform.gameObject.GetComponent<CombatTarget>();
                if(target == null) continue;
                if(target == GetComponent<CombatTarget>()) continue;
                if(!_fighter.CanAttack(target.gameObject)) continue;
                Debug.Log($"Encontre un objetivo");
                if(Input.GetMouseButtonDown(0))
                {
                    _fighter.Attack(target.gameObject);
                } 
                return true;
            }
            return false;
        }
        #endregion

        #region Movement
        //Chequeo con Raycast algún punto en el mundo en donde pueda hacer moverme y si hago click y tengo lugar, me muevo
        private bool InteractWithMovement()
        {
            RaycastHit hit;
            bool hasHit = Physics.Raycast(GetMouseRay(), out hit);
            if (hasHit && !CheckRaycastTags(hit))
            {
                //playerCursor.SetMoveCursor();
                if (Input.GetMouseButtonDown(0)) GetComponent<Mover>().StartMoveAction(hit.point);
                return true;
            }
            //playerCursor.SetDefaultCursor();
            return false;
        }

        //Devuelve el punto donde esté apuntando con el mouse en la posición del mundo
        private static Ray GetMouseRay()
        {
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }

        //Chequeo si el ray apunta a un objeto cuyo tag no pueda moverme
        private bool CheckRaycastTags(RaycastHit hitPoint)
        {
            switch(hitPoint.collider.gameObject.tag)
            {
                case "Prop":
                    return true;
                default:
                    return false;
            }
        }
        #endregion

        #region ItemInventory

        private IEnumerator useItem()
        {
            canUseItem = false;
            GetComponent<ActionScheduler>().CancelCurrentAction();
            yield return new WaitForSeconds(1f);
            canUseItem = true;
        }
        void UseItemOnInventory(int item)
        {
            if(item < _itemInventory.potions.Count)
            {
                _itemInventory.UsePotionOnInventory(_itemInventory.potions[item]);
                StartCoroutine(useItem());
            }
        }
        #endregion

        #region WeaponInventory
        private IEnumerator SetPlayerActiveWeapon()
        {
            canChangeWeapon = false;
            _weaponInventory.ChangeActiveWeapon();
            yield return new WaitForSeconds(0.5f);
            canChangeWeapon = true;
        }
        #endregion

        #region UI
        public void InteractWithInventoryMenu()
        {
            if(Input.GetKeyDown(KeyCode.Tab))
            {
                _menuController.ShowUIObject(_menuController.GetWeaponInventoryMenu());
                return;
            }
            if(Input.GetKeyUp(KeyCode.Tab))
            {
                _menuController.HideUIObject(_menuController.GetWeaponInventoryMenu());
            }
        }
        #endregion
    }

}