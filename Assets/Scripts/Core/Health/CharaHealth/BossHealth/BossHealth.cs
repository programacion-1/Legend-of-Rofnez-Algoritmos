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

        public override void CoreStartingSettings(params object[] p)
        {
            base.CoreStartingSettings(p);
            SetBossHealthStartingSettings((float) p[0], (float) p[1], (string) p[2]);
        }

        public virtual void SetBossHealthStartingSettings(params object[] p)
        {
            _bossHealthBarName = barName;
            TriggerChangeInteractiveBarValuesEvent();
            NotifyToObservers("Begin");
            _damageRate = (float) p[0];
            _returnToWhiteColourTimer = (float) p[1];
            _deadTriggerName = (string) p[2];
        }
        public override void CharaDamageBehaviour()
        {
            base.CharaDamageBehaviour();
            BossDamageBehaviour();
        }

        
        public virtual void BossDamageBehaviour()
        {
            TriggerChangeInteractiveBarValuesEvent();
            TriggerAIHasBeenAttacked();
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

        void TriggerAIHasBeenAttacked()
        {
            EventManager.TriggerEvent(EventManager.Events.Event_AIHasBeenAttacked, _healthID);
        }
        #endregion
    }
}
