using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Control
{
    public class GuardAIController : AIController
    {
        [Header("Guard AI Variables")]
        [SerializeField] float _suspicionTime = 5f;
        [SerializeField] PatrolPath _patrolPath;
        Vector3 _guardPosition;
        [SerializeField] float _waypointTolerance = 1f;
        [SerializeField] float _waypointDwellTime = 5f;
        float _timeSinceLastSawPlayer = Mathf.Infinity;
        int _currentWaypointIndex = 0;
        float _timeSinceMovedToNextWaypoint = Mathf.Infinity;
        void Start()
        {
            ParentStartingSettings();
            _guardPosition = transform.position;
        }

        public override void UpdateBehaviour()
        {
            if ((InAttackRangeOfPlayer() && _AIfighter.CanAttack(_AIplayerTarget)) || _hasBeenAttackedByTarget) AttackBehaviour(); //Chequeo si el player está en mi rango de ataque y si puedo atacar. También chequeo si fui golpeado por el player
            else if (_timeSinceLastSawPlayer <= _suspicionTime) SuspicionBehaviour(); //Chequeo si aún está en guardia o vuelvo a patrullar
            else PatrolBehaviour(); //Patrulla
            #region Codigo viejo para cuando se vuelva a implementar el hechizo de congelamiento
            /*if(!GetHealth().CheckIfIsFreezed())
            {
                if (InAttackRangeOfPlayer() && GetFighter().CanAttack(GetPlayer())) AttackBehaviour(); //Chequeo si el player está en mi rango de ataque y si puedo atacar.
                else if (timeSinceLastSawPlayer <= suspicionTime) SuspicionBehaviour(); //Chequeo si aún está en guardia o vuelvo a patrullar
                else PatrolBehaviour(); //Patrulla
            }
            else
            {
                GetActionScheduler().CancelCurrentAction();
            }*/
            #endregion
        }
        public override void AttackBehaviour()
        {
            _timeSinceLastSawPlayer = 0;
            _AIfighter.Attack(_AIplayerTarget);
        }

        public override void UpdateTimers()
        {
            _timeSinceLastSawPlayer += Time.deltaTime;
            _timeSinceMovedToNextWaypoint += Time.deltaTime;
        }

        private void SuspicionBehaviour()
        {
            _AIactionScheduler.CancelCurrentAction();
        }


        private void PatrolBehaviour()
        {
            Vector3 nextPosition = _guardPosition;
            if(_patrolPath != null)
            {
                if(AtWaypoint())
                {
                    _timeSinceMovedToNextWaypoint = 0f;
                    CycleWaypoint();
                }

                nextPosition = GetCurrentWaypoint();
            }
            if(_timeSinceMovedToNextWaypoint > _waypointDwellTime)
            {
                _AImover.StartMoveAction(nextPosition);
            }
            
        }

        private bool AtWaypoint()
        {
            float distanceToWaypoint = Vector3.Distance(transform.position, GetCurrentWaypoint());
            return distanceToWaypoint < _waypointTolerance;
        }

        private void CycleWaypoint()
        {
            _currentWaypointIndex = _patrolPath.GetNextIndex(_currentWaypointIndex);
        }

        private Vector3 GetCurrentWaypoint()
        {
            return _patrolPath.GetWaypoint(_currentWaypointIndex);
        }

        //Llama Unity para mostrar los gizmos que se dibujen en esta sección
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, aiChaseDistance);
        }
    }
}
