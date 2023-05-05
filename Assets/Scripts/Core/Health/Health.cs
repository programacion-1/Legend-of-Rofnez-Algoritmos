using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GeneralEnums;

namespace RPG.Core
{
    public abstract class Health : MonoBehaviour
    {
        [SerializeField] float healthPoints;
        [SerializeField] float maxHealthPoints = 100f;
        private bool _isDead;
        [SerializeField] private bool _isInvencible;

        [Header("Audio Clips")]
        public AudioManager audioManager;
        public AudioClip deadClipSound;
        public AudioClip damageClipSound;
        public AudioClip impactClipSound;

        #region StartingSettings
        //Seteo las configuraciones iniciales que usarán
        public void CoreStartingSettings()
        {
            audioManager = GameObject.FindObjectOfType<AudioManager>();
            healthPoints = maxHealthPoints;
        }
        //Función abstracta para las configuraciones iniciales que usarán las clases que hereden de ésta
        public abstract void ChildrenStartingSettings();

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
        public float GetHP()
        {
            return healthPoints;
        }
        public void SetHP(float hp)
        {
            healthPoints = hp;
        }
        public float GetMaxHP()
        {
            return maxHealthPoints;
        }
        public void SetMaxHP(float maxHP)
        {
            maxHealthPoints = maxHP;
        }
        #endregion
        
        #region Invencibility
        //Corrutina que habilita la invencibilidad por una cantidad de segundos definidas por parámetro
        public IEnumerator EnableInvencibilityCo(float seconds)
        {
            _isInvencible = true;
            yield return new WaitForSeconds(seconds);
            DisableInvencibility();
        }

        //ATENCION: función únicamente para utilizar al activar el God Mode
        public void EnableInvencibility()
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
            float currentHealthPoints = Mathf.Max(healthPoints - damage, 0);
            SetHP(currentHealthPoints);
            ChildrenDamageBehavior();
            if (healthPoints == 0) Die();
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
        
        void Start()
        {
            CoreStartingSettings();
            ChildrenStartingSettings();
        }
    }
}