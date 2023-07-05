using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.MVC.Player
{
    public class PlayerController : IController
    {
        PlayerModel _playerModel;
        public PlayerController(PlayerModel model)
        {
            _playerModel = model;
        }
        void IController.ListenFixedKeys()
        {
            if(InteractWithMagicController()) return;
            if(InteractWithCombatController()) return;
            if(InteractWithMovementController()) return;
        }

        void IController.ListenKeys()
        {
            //God Mode
            if(Input.GetKeyDown(KeyCode.F)) _playerModel.CheckGodMode();

            //Use Items. Aun hardcodeado xd
            if(Input.GetKeyDown(KeyCode.Alpha1)) _playerModel.UseItemOnInventory(KeyCode.Alpha1);
            if(Input.GetKeyDown(KeyCode.Alpha2)) _playerModel.UseItemOnInventory(KeyCode.Alpha2);

            //Cambiar Arma activa
            if(Input.GetKeyDown(KeyCode.Q)) _playerModel.ChangeActiveWeapon();

            //Interact with InventoryMenu
            if(Input.GetKeyDown(KeyCode.Tab)) _playerModel.InteractWithInventoryMenu(true);
            if(Input.GetKeyUp(KeyCode.Tab)) _playerModel.InteractWithInventoryMenu(false);
        }

        private bool InteractWithMagicController()
        {
            if(Input.GetMouseButtonDown(1)) // Se seteará en el model la condición de magic
            {
                RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());
                if(_playerModel.InteractWithMagic(hits)) return true;
                else return false;
            }
            else return false;
        }

        private bool InteractWithCombatController()
        {
            RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());
            if(_playerModel.FindCombatTarget(hits))
            {
                Debug.Log($"Encontre un objetivo");
                if(Input.GetMouseButtonDown(0))
                {
                    _playerModel.InteractWithCombat();
                }
                return true; 
            }
            return false;
        }

        private bool InteractWithMovementController()
        {
            RaycastHit hit;
            bool hasHit = Physics.Raycast(GetMouseRay(), out hit);
            if (hasHit && !CheckRaycastTags(hit))
            {
                //playerCursor.SetMoveCursor();
                if (Input.GetMouseButtonDown(0))
                {
                    _playerModel.Move(hit);
                    Debug.Log($"Move");
                } 
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
    }
}
