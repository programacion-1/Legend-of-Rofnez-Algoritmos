using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core
{
    public class PlayerHealth : CharaHealth, IObservable
    {
        //[Header("Player Health Stats")]
        List<IObserver> _allObservers = new List<IObserver>();
        

        public override void CharaDamageBehaviour()
        {
            NotifyToObservers("ChangeHealthBarValue");
        }

        public override void CharaDeathBehaviour()
        {
            
        }


        public void SetPlayerHealthStartingSettings()
        {
            CoreStartingSettings();
        }


        void Start()
        {
            SetPlayerHealthStartingSettings();
        }

        #region Observable
        public void NotifyToObservers(string action)
        {
             //Recorro de atras hacia el principio mis observers registrados y los notifico
            for (int i = _allObservers.Count - 1; i >= 0; i--)
            {
                _allObservers[i].Notify(action);
            }
        }
        public void Subscribe(IObserver obs)
        {
            //Si en mi lista no tengo a ese observer
            if (!_allObservers.Contains(obs))
            {
                //Lo agrego
                _allObservers.Add(obs);
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
