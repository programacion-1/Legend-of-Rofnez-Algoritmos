using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Movement;
using RPG.Core;
using RPG.Item;

namespace RPG.Combat
{
    public abstract class Fighter : MonoBehaviour, IAction
    {
        [SerializeField] protected Transform _rightHandTransform = null;
        [SerializeField] protected Transform _leftHandTransform = null;
        [SerializeField] protected Weapon _defaultWeapon = null;
        Mover _mover;
        protected Animator _anim;
        protected RuntimeAnimatorController _defaultRuntimeAnimatorController;
        protected Health _target;
        protected float _timeSinceLastAttack = Mathf.Infinity;        
        
        // Start is called before the first frame update
        void Start()
        {
            _mover = GetComponent<Mover>();  
            _anim = GetComponent<Animator>();
            _defaultRuntimeAnimatorController = _anim.runtimeAnimatorController;
            ChildFighterSettings();
        }

        public abstract void ChildFighterSettings();

        // Update is called once per frame
        void Update()
        {
            _timeSinceLastAttack += Time.deltaTime;

            //Chequeo si tengo un objetivo y si el objetivo está muerto            
            if (_target == null) return;
            if(_target.CheckIfIsDead()) return;

            //Si estoy en el rango de ataque, ataco y sino me muevo hasta el objetivo
            if(!GetIsInRange())
            {
                _mover.MoveTo(_target.transform.position);
            }
            else
            {
                AttackBehaviour();
                _mover.Cancel();
            }
        }

        //Evento en la animación de Attack. Activa el ataque del arma
        void ActivateAttack()
        {
            if(_target == null) return;
            ChildrenAttack();
        }

        public abstract void ChildrenAttack();

        void DeactivateAttack()
        {
            ChildrenDeactivateAttack();
        }

        public abstract void ChildrenDeactivateAttack();

        //Lo que hago al atacar
        private void AttackBehaviour()
        {
            transform.LookAt(_target.transform); //Roto hacia mi objetivo
            if(AttackAvaivalbleByTimer())
            {
                //Esto invoca el evento Hit()
                _timeSinceLastAttack = 0f;
                AttackTriggers("StopAttack", "Attack");                
            }
        }

        public abstract bool AttackAvaivalbleByTimer();

        //Reinicio un trigger e inicio el otro. Esto se realizó para evitar un bug cuando se cancela un ataque con el trigger StopAttack
        private void AttackTriggers(string triggerToReset, string triggerToSet)
        {
            _anim.ResetTrigger(triggerToReset);
            _anim.SetTrigger(triggerToSet);
        }

        //Devuelve la distancia entre mi posición y la del objetivo y chequea que sea menor al rango del arma
        public abstract bool GetIsInRange();
        /*{
            //return Vector3.Distance(transform.position, _target.transform.position) < _currentWeapon.weaponRange;
        }*/

        //Inicio la acción de ataque y defino mi objetivo
        public void Attack(GameObject combatTarget)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            _target = combatTarget.GetComponent<Health>();
        }

        //Chequeo si puedo atacar
        public bool CanAttack(GameObject combatTarget)
        {
            if(combatTarget == null) return false; //Chequeo si el objetivo es null
            Health targetToTest =  combatTarget.GetComponent<Health>();
            return !targetToTest.CheckIfIsDead(); //Chequeo si el objetivo no está muerto
        }

        //Cancelo el ataque
        public void Cancel()
        {
            _target = null;
            AttackTriggers("Attack", "StopAttack");
            DeactivateAttack();
        }
    }

}