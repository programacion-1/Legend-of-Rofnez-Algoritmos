using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GeneralEnums;

namespace RPG.Core
{
    public abstract class Health : MonoBehaviour
    {
        [SerializeField] float _healthPoints;
        [SerializeField] float _maxHealthPoints = 100f;
        private bool _isDead;
        [SerializeField] private bool _isInvencible;
        private float _invencibiltyTimer;

        [Header("Audio Clips")]
        public AudioManager audioManager;
        public AudioClip deadClipSound;
        public AudioClip damageClipSound;
        public AudioClip impactClipSound;

        protected int _healthID;

        #region StartingSettings
        //Seteo las configuraciones iniciales que usarán
        public void CoreStartingSettings()
        {
            audioManager = GameObject.FindObjectOfType<AudioManager>();
            _healthPoints = _maxHealthPoints;
        }
        //Función abstracta para las configuraciones iniciales que usarán las clases que hereden de ésta
        //public abstract void ChildrenStartingSettings();

        #endregion

        #region Checkers
        public bool CheckIfIsDead()
        {
            return _isDead;
        }

        public bool CheckIfIsInvencible()
        {
            return _isInvencible;
        }        
        #endregion
        
        #region Getters y Setters
        public float HealthPoint
        {
            get{return _healthPoints;}
        }
        public float GetHP()
        {
            return _healthPoints;
        }
        public void SetHP(float hp)
        {
            _healthPoints = hp;
        }
        public float GetMaxHP()
        {
            return _maxHealthPoints;
        }
        public void SetMaxHP(float maxHP)
        {
            _maxHealthPoints = maxHP;
        }
        #endregion
        
        #region Invencibility
        public void EnableInvencibility(float seconds)
        {
            if(!CheckIfIsInvencible())
            {
                _invencibiltyTimer = seconds;
                StartCoroutine("EnableInvencibilityCo");
            }
        }

        //Corrutina que habilita la invencibilidad por una cantidad de segundos definidas por parámetro
        public IEnumerator EnableInvencibilityCo()
        {
            _isInvencible = true;
            yield return new WaitForSeconds(_invencibiltyTimer);
            DisableInvencibility();
        }

        //ATENCION: función únicamente para utilizar al activar el God Mode
        public void EnableInvencibilityCheat()
        {
            _isInvencible = true;
        }

        //Función para desabilitar la invencibilidad
        public void DisableInvencibility()
        {
            _isInvencible = false;
        }

        #endregion
        
        #region Damage
        //Realizo los chequeos necesarios para saber si recibo daño o no
        public void TakeDamage(float damage)
        {
            if(!CheckIfIsDead() && !CheckIfIsInvencible()) DamageBehaviour(damage);
        }

        //Comportamiento core al recibir daño
        private void DamageBehaviour(float damage)
        {
            float currentHealthPoints = Mathf.Max(_healthPoints - damage, 0);
            SetHP(currentHealthPoints);
            ChildrenDamageBehavior();
            if (_healthPoints == 0) Die();
        }

        //Comportamiento que harán las clases que hereden de Health al recibir daño
        public abstract void ChildrenDamageBehavior();

        #endregion

        #region Death
        //Función de Muerte
        public void Die()
        {
            if (CheckIfIsDead()) return;
            _isDead = true;            
            DeathBehaviour();
        }

        //Comportamiento de la clase al morir
        public abstract void DeathBehaviour();
        #endregion

        public void SetID(int id)
        {
            _healthID = id;
        }
    }
}