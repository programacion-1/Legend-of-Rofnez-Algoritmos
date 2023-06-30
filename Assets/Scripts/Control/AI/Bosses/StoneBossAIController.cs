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
        //MagicPoints magicPoints;
        //Special special;
        void Start()
        {
            ParentStartingSettings();
            _enemyMagicCaster = GetComponent<EnemyMagicCaster>();
            //magicPoints = GetComponent<MagicPoints>();
            //special = GetComponent<Special>();
        }

        public override void UpdateBehaviour()
        {
            if(_timeSinceThrewFireball >= _fireballAttackTime) _castedFireball = false;
            
            if(!_castedFireball)
            {
                if (InAttackRangeOfPlayer() || _hasBeenAttackedByTarget) CastFireball(); 
            }
            else
            {
                if(_timeUntilICanAttack >= _attackBehaviourTime)
                {
                    //Chequeo si puedo atacar y si el player esta en mi rango de ataque o si he sido atacado por el mismo
                    if(_AIfighter.CanAttack(_AIplayerTarget) && (InAttackRangeOfPlayer() || _hasBeenAttackedByTarget))
                    {
                        Debug.Log("Attack");
                        if(InAttackRangeOfPlayer()) _hasBeenAttackedByTarget = false; //En el caso de que el player este en mi rango de ataque desactivo el bool de que fui atacado
                        AttackBehaviour();
                    } 
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
            _castedFireball = true;
            _timeSinceThrewFireball = 0;
            if(!_hasBeenAttackedByTarget) _timeUntilICanAttack = 0;
            /* Se da un bug a la hora de instanciar la bola de fuego en donde no se ejecuta nada pasado el Awake. Pendiente a corregir en el segundo parcial
            _enemyMagicCaster.target = _AIplayerTarget.GetComponent<PlayerHealth>();
            _enemyMagicCaster.MagicAttack();*/
        }

        public override void DetectTargetByHit(params object[] p)
        {
            base.DetectTargetByHit(p);
            _timeUntilICanAttack = Mathf.Infinity;
        }
    }
}
