using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Obstacle
{
    public abstract class Trap : MonoBehaviour
    {
        //protected no aparece en el editor de unity aunque sí puedo usarla entre herencias, por ende las variables que tienen serialize field y se usen entre herencias tendrán dos variables a utilizar
        protected float _currentTime{get; set;}        
        [SerializeField] private float _time;
        protected float time{get; set;}       
        protected float fireRank{get; set;}
        [SerializeField] private float _waitTimeTrapDeactivater;
        protected float waitTimeTrapDeactivater{get; set;} 
        //Trigger para saber si la trampa está activa o no
        [SerializeField] private bool onOffTrigger;

        #region Clase para prender y apagar el trigger de la trampa
        public void SetTriggerOn()
        {
            onOffTrigger = true;
        }

        public void SetTriggerOff()
        {
            onOffTrigger = false;
        }
        #endregion
        
        private void Start()
        {
            _currentTime = 0;
            time = _time;
            waitTimeTrapDeactivater = _waitTimeTrapDeactivater;
            SetTriggerOn();
            ChildStartSettings();
        }
        //Comportamiento general de la trampa
        private void FixedUpdate()
        {
            if(onOffTrigger) TrapActivatedBehaviour();
            else TrapDeactivatedBehaviour();
        }

        //Corrutina para reactivar la trampa
        public abstract IEnumerator waitToReactivate();
        //Settings de sus hijos para el start
        public abstract void ChildStartSettings();
        public abstract void TrapActivatedBehaviour();
        public abstract void TrapDeactivatedBehaviour();
        public abstract void TrapEffect();
    }

}