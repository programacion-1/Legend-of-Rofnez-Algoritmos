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
        protected float _chaseDistance;
        [SerializeField] int _AiID;
        public int AiID{get{return _AiID;} set{_AiID = value;}}
        protected ActionScheduler _AIactionScheduler{get; set;}
        protected Fighter _AIfighter{get; set;}
        protected Health _AIhealth{get; set;}
        protected GameObject _AIplayerTarget{get; set;}
        protected Mover _AImover{get; set;}
        CombatTarget _AICombatTarget;
        public bool _hasBeenAttackedByTarget;
        protected EnemyFlyweight _AIStats;
        //[SerializeField] protected bool _hasBeenAttackedByTarget{get; set;}
        public void ParentStartingSettings()
        {
            _AIactionScheduler = GetComponent<ActionScheduler>();
            _AIfighter = GetComponent<Fighter>();
            _AIhealth = GetComponent<Health>();
            _AIhealth.SetID(_AiID);
            _AICombatTarget = GetComponent<CombatTarget>();
            _AICombatTarget.SetAttackers(EnemyFlyweightPointer.AIController.attackers);
            _AIplayerTarget = GameObject.Find("Player");
            _AImover = GetComponent<Mover>();
            _hasBeenAttackedByTarget = false;
        }

        public float aiChaseDistance
        {
            get{return _chaseDistance;}
        }

        //Update del Padre
        private void Update()
        {
            //Chequeo si está muerto
            if (_AIhealth.CheckIfIsDead())
            {
                EventManager.UnsubscribeToEvent(EventManager.Events.Event_AIHasBeenAttacked, DetectTargetByHit);
                return;
            } 
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

        #region EventManager
        private void OnEnable()
        {
            EventManager.SubscribeToEvent(EventManager.Events.Event_AIHasBeenAttacked, DetectTargetByHit);
        }

        private void OnDisable()
        {
            EventManager.UnsubscribeToEvent(EventManager.Events.Event_AIHasBeenAttacked, DetectTargetByHit);
        }

        public virtual void DetectTargetByHit(params object[] p)
        {
            int id = (int) p[0];
            if(id == _AiID) _hasBeenAttackedByTarget = true;
            if(_hasBeenAttackedByTarget) Debug.Log("Enemy " + _AiID + " is being attacked!");
        }
        #endregion
    }
}