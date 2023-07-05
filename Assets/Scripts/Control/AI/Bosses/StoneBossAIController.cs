using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using RPG.Core;
using RPG.Combat;
using RPG.Magic;

namespace RPG.Control
{
    public class StoneBossAIController : AIController
    {
        EnemyMagicCaster _enemyMagicCaster;
        [SerializeField] float _fireballAttackTime = 5f;
        float _timeSinceThrewFireball = Mathf.Infinity;
        bool _castedFireball;
        [SerializeField] float _attackBehaviourTime = 1f;
        float _timeUntilICanAttack = 0;

        void Start()
        {
            ParentStartingSettings();
            _enemyMagicCaster = GetComponent<EnemyMagicCaster>();
            _chaseDistance = EnemyFlyweightPointer.StoneBossAIController.chaseDistance; //Seteo la distancia de chase con la base del flyweight de StoneBossAIController
            _AIhealth.CoreStartingSettings(EnemyFlyweightPointer.StoneBossAIController.damageRate, EnemyFlyweightPointer.StoneBossAIController.returnToWhiteColorTimer, EnemyFlyweightPointer.AIController.DeadTriggerName); //Seteo los stats a modificar del Health desde el Flyweight con distintos pointers dependiendo lo que necesito
            _AImover.SetNaveMeshSpeed(EnemyFlyweightPointer.StoneBossAIController.movementSpeed); //Seteo la velocidad del AgentNaveMesh desde el Flyweight
        }

        public override void UpdateBehaviour()
        {
            
            if(_timeSinceThrewFireball >= _fireballAttackTime)
            {
                if (InAttackRangeOfPlayer() || _hasBeenAttackedByTarget)
                {
                    if(InAttackRangeOfPlayer()) _hasBeenAttackedByTarget = false;
                    CastFireball(); 
                    return;
                } 
            }

            if(_timeUntilICanAttack >= _attackBehaviourTime && !_enemyMagicCaster.CheckIfIsCastingMagic())
            {
                //Chequeo si puedo atacar y si el player esta en mi rango de ataque o si he sido atacado por el mismo
                if(_AIfighter.CanAttack(_AIplayerTarget) && (InAttackRangeOfPlayer() || _hasBeenAttackedByTarget))
                {
                    if(InAttackRangeOfPlayer()) _hasBeenAttackedByTarget = false; //En el caso de que el player este en mi rango de ataque desactivo el bool de que fui atacado
                    AttackBehaviour();
                } 
            }
            
        }

        public override void UpdateTimers()
        {
            _timeSinceThrewFireball += Time.deltaTime;
            _timeUntilICanAttack += Time.deltaTime;
        }

        public override void AttackBehaviour()
        {
            _AIfighter.Attack(_AIplayerTarget);
        }

        //Llama Unity para mostrar los gizmos que se dibujen en esta sección
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, aiChaseDistance);
        }

        // Funciones Propias del Stone Boss
        public void CastFireball()
        {        
            _timeSinceThrewFireball = 0;
            _timeUntilICanAttack = 0;
            _enemyMagicCaster.target = _AIplayerTarget.GetComponent<PlayerHealth>();
            _enemyMagicCaster.MagicAttack();
        }

        public override void DetectTargetByHit(params object[] p)
        {
            base.DetectTargetByHit(p);
            _timeUntilICanAttack = Mathf.Infinity;
        }
    }
}
