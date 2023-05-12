using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.UI;

namespace RPG.Core
{
    public abstract class BossHealth : CharaHealth, IObservable
    {
        [SerializeField] InteractiveBar _bossHealthBar;
        [SerializeField] string _bossHealthBarName;
        const string barName = "BossHealthBar";
        List<IObserver> _allObservers = new List<IObserver>();
        public void SetBossHealthStartingSettings()
        {
            CoreStartingSettings();
            _bossHealthBarName = barName;
            InteractiveBar[] interactiveBars = GameObject.FindObjectsOfType<InteractiveBar>();
            for (int i = 0; i < interactiveBars.Length; i++)
            {
                if (interactiveBars[i].gameObject.name == _bossHealthBarName)
                {
                    _bossHealthBar = interactiveBars[i];
                    _bossHealthBar.ChangeBarFiller(GetHP(), GetMaxHP());
                    break;
                }
            }
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
            _bossHealthBar.ChangeBarFiller(GetHP(), GetMaxHP());
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
            _bossHealthBar.ChangeBarFiller(GetHP(), GetMaxHP());
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

    }
}
