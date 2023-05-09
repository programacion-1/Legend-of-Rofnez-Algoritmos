using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Combat;
using RPG.Core;
using RPG.Movement;
using System;

namespace RPG.Control
{
    public abstract class AIController: MonoBehaviour
    {
        [SerializeField] float chaseDistance = 5f;

        ActionScheduler actionScheduler;
        Fighter fighter;
        Health health;
        GameObject player;
        Mover mover;
        public void ParentStartingSettings()
        {
            actionScheduler = GetComponent<ActionScheduler>();
            fighter = GetComponent<Fighter>();
            health = GetComponent<Health>();
            player = GameObject.FindObjectOfType<PlayerController>().gameObject;
            mover = GetComponent<Mover>();
        }

        // Getters Principales

        public ActionScheduler aiActionScheduler
        {
            get{return actionScheduler;}
        }

        public Fighter aiFighter
        {
            get{return fighter;}
        }

        public Health aiHealth
        {
            get{return health;}
        }

        public Mover aiMover
        {
            get{return mover;}
        }
        
        public GameObject playerTarget
        {
            get{return player;}
        }

        public float aiChaseDistance
        {
            get{return chaseDistance;}
        }

        //Update del Padre
        private void Update()
        {
            //Chequeo si está muerto
            if (health.CheckIfIsDead()) return;
            UpdateBehaviour();
            UpdateTimers();
        }

        public abstract void UpdateTimers();
        public abstract void AttackBehaviour();
        public abstract void UpdateBehaviour();

         //Devuelve un booleano al calcular la distancia del player con el enemigo y si dicha distancia es menor a la distancia para perseguir al player.
        public bool InAttackRangeOfPlayer()
        {
            float distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);
            return distanceToPlayer < chaseDistance;
        }
    }
}