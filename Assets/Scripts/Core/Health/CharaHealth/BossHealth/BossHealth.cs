using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.UI;

namespace RPG.Core
{
    public abstract class BossHealth : CharaHealth, IObservable
    {
        [SerializeField] string _bossHealthBarName;
        const string barName = "BossHealthBar";
        List<IObserver> _allObservers = new List<IObserver>();
        public void SetBossHealthStartingSettings()
        {
            CoreStartingSettings();
            _bossHealthBarName = barName;
            TriggerChangeInteractiveBarValuesEvent();
            BossChildHealthStartingSettings();
            NotifyToObservers("Begin");
        }

        public virtual void BossChildHealthStartingSettings()
        {

        }

        void Start()
        {
            SetBossHealthStartingSettings();
        }
        public override void CharaDamageBehaviour()
        {
            base.CharaDamageBehaviour();
            TriggerChangeInteractiveBarValuesEvent();
            BossDamageBehaviour();
        }

        public virtual void BossDamageBehaviour()
        {

        }

        public override void CharaDeathBehaviour()
        {
            base.CharaDeathBehaviour();
            NotifyToObservers("Die");
            BossDeathBehaviour();
        }

        public virtual void BossDeathBehaviour()
        {
            
        }

        public override void HealVisualSettings()
        {
            TriggerChangeInteractiveBarValuesEvent();
        }

        #region Observable

        public void Subscribe(IObserver obs)
        {
            //Si en mi lista no tengo a ese observer
            if (!_allObservers.Contains(obs))
            {
                //Lo agrego
                _allObservers.Add(obs);
            }
        }

        public void NotifyToObservers(string action)
        {
            //Recorro de atras hacia el principio mis observers registrados y los notifico
            for (int i = _allObservers.Count - 1; i >= 0; i--)
            {
                _allObservers[i].Notify(action);
            }
        }

        public void Unsubscribe(IObserver obs)
        {
            //Remuevo el observer de mi lista
            _allObservers.Remove(obs);
        }

        #endregion

        #region EventManager
        void TriggerChangeInteractiveBarValuesEvent()
        {
            EventManager.TriggerEvent(EventManager.Events.Event_ChangeInteractiveBarValues, _bossHealthBarName, GetHP(), GetMaxHP());
        }
        #endregion
    }
}
