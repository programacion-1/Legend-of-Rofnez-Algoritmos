using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GeneralEnums;
using RPG.Movement;
//using RPG.Combat;
using RPG.Core;
//using RPG.UI;
using System;

namespace RPG.Control
{
    //Funcion dedicada para el control del player
    public class PlayerController : MonoBehaviour
    {
        PlayerHealth _health;
        private bool _godMode;
        bool canHealHP = true;
        bool canHealMP = true;
        
        // Start is called before the first frame update
        void Start()
        {
            _health = GetComponent<PlayerHealth>();
        }

        // Update is called once per frame
        void Update()
        {
            if(_health.CheckIfIsDead()) return;
            #region Inputs de Testing
            if(Input.GetKeyDown(KeyCode.F1)) _health.TakeDamage(10f);
            #endregion
            //Input para activar y desactivar el God Mode
            if(Input.GetKeyDown(KeyCode.F))
            {
                if(_godMode) DisableGodMode();
                else EnableGodMode();
            }

            if(InteractWithMovement()) return;
        }

        #region God Mode
        private void EnableGodMode()
        {
            _godMode = true;
            _health.EnableInvencibility();
        }

        private void DisableGodMode()
        {
            _godMode = false;
            _health.DisableInvencibility();
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
    }

}