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
        [SerializeField] float _chaseDistance = 5f;

        protected ActionScheduler _AIactionScheduler{get; set;}
        protected Fighter _AIfighter{get; set;}
        protected Health _AIhealth{get; set;}
        protected GameObject _AIplayerTarget{get; set;}
        protected Mover _AImover{get; set;}
        protected bool _hasBeenAttackedByTarget{get; set;}
        public void ParentStartingSettings()
        {
            _AIactionScheduler = GetComponent<ActionScheduler>();
            _AIfighter = GetComponent<Fighter>();
            _AIhealth = GetComponent<Health>();
            _AIplayerTarget = GameObject.FindObjectOfType<PlayerController>().gameObject;
            _AImover = GetComponent<Mover>();
        }

        public float aiChaseDistance
        {
            get{return _chaseDistance;}
        }

        //Update del Padre
        private void Update()
        {
            //Chequeo si está muerto
            if (_AIhealth.CheckIfIsDead()) return;
            UpdateBehaviour();
            UpdateTimers();
        }

        public abstract void UpdateTimers();
        public abstract void AttackBehaviour();
        public abstract void UpdateBehaviour();

         //Devuelve un booleano al calcular la distancia del player con el enemigo y si dicha distancia es menor a la distancia para perseguir al player.
        public bool InAttackRangeOfPlayer()
        {
            float distanceToPlayer = Vector3.Distance(_AIplayerTarget.transform.position, transform.position);
            return distanceToPlayer < _chaseDistance;
        }
    }
}