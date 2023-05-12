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

        public abstract void BossChildHealthStartingSettings();

        void Start()
        {
            SetBossHealthStartingSettings();
        }
        public override void CharaDamageBehaviour()
        {
            TriggerChangeInteractiveBarValuesEvent();
            BossDamageBehaviour();
        }

        public abstract void BossDamageBehaviour();

        public override void CharaDeathBehaviour()
        {
            NotifyToObservers("Die");
            BossDeathBehaviour();
        }

        public abstract void BossDeathBehaviour();

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
            Debug.Log(_allObservers.Count);
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
