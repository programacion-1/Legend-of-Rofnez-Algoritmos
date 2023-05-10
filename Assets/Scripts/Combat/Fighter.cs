using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Movement;
using RPG.Core;
using RPG.Item;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour, IAction
    {
        [SerializeField] Transform _rightHandTransform = null;
        [SerializeField] Transform _leftHandTransform = null;
        [SerializeField] Weapon _defaultWeapon = null;
        [SerializeField] Weapon _currentWeapon = null;
        Mover _mover;
        Animator _anim;
        RuntimeAnimatorController _defaultRuntimeAnimatorController;
        Health _target;
        float _timeSinceLastAttack = Mathf.Infinity;
        AttackTrigger _weaponAttackTrigger;
        
        
        // Start is called before the first frame update
        void Start()
        {
            _mover = GetComponent<Mover>();  
            _anim = GetComponent<Animator>();
            _defaultRuntimeAnimatorController = _anim.runtimeAnimatorController;
            EquipWeapon(_defaultWeapon);
        }

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

        public void EquipWeapon(Weapon weapon)
        {
            if(weapon == null) return;
            _currentWeapon = weapon;
            _anim.runtimeAnimatorController = _defaultRuntimeAnimatorController;
            weapon.Spawn(_rightHandTransform, _leftHandTransform, _anim);
            _currentWeapon.SetEquippedWeaponDamage();
        }

        public Weapon GetCurrentWeapon()
        {
            return _currentWeapon;
        }

        //Evento en la animación de Attack. Activa el ataque del arma
        void ActivateAttack()
        {
            if(_target == null) return;
            _currentWeapon.WeaponAttack(_target);
        }

        void DeactivateAttack()
        {
            _currentWeapon.StopAttack();
        }

        //Lo que hago al atacar
        private void AttackBehaviour()
        {
            transform.LookAt(_target.transform); //Roto hacia mi objetivo
            if(_timeSinceLastAttack >= _currentWeapon.timeBetweenAttacks)
            {
                //Esto invoca el evento Hit()
                _timeSinceLastAttack = 0f;
                AttackTriggers("StopAttack", "Attack");                
            }
        }

        //Reinicio un trigger e inicio el otro. Esto se realizó para evitar un bug cuando se cancela un ataque con el trigger StopAttack
        private void AttackTriggers(string triggerToReset, string triggerToSet)
        {
            _anim.ResetTrigger(triggerToReset);
            _anim.SetTrigger(triggerToSet);
        }

        //Devuelve la distancia entre mi posición y la del objetivo y chequea que sea menor al rango del arma
        private bool GetIsInRange()
        {
            return Vector3.Distance(transform.position, _target.transform.position) < _currentWeapon.weaponRange;
        }

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