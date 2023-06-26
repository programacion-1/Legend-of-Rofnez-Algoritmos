using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.SceneManagement;
using RPG.UI;

namespace RPG.Core
{
    public abstract class QuestManager : MonoBehaviour, IObserver
    {
        [SerializeField] GameObject nextLevelPortal;
        bool allQuestsAreCompleted = false;
        #region ObserverPattern
        protected bool finalConditionCompleted = false;
        IObservable _objectObserved;
        Dictionary<string, System.Action> _notifications;
        BossHealth _boss;
        protected EventText _eventText;
        [SerializeField] protected string[] _questTexts;
        [SerializeField] GameObject _bossHealthBar;
        #endregion
        public void StartingSettings()
        {
            nextLevelPortal.SetActive(false);
            _eventText = FindObjectOfType<EventText>();
            #region Observer Pattern
            FillNotifiesDictionary();
            _objectObserved = FindObjectOfType<BossHealth>();
            _objectObserved.Subscribe(this);
            #endregion
            LevelStartingSettings();
        }

        public abstract void LevelStartingSettings();

        public bool CheckIfAllQuestsAreCompleted()
        {
            return allQuestsAreCompleted;
        }

        public void CompleteQuests()
        {
            allQuestsAreCompleted = true;
        }

        public abstract void QuestChecker();

        public void ShowNextLevelPortal()
        {
            if(!nextLevelPortal.activeInHierarchy) nextLevelPortal.SetActive(true);
        }

        public Portal GetPortal()
        {
            return nextLevelPortal.GetComponent<Portal>();
        }

        public void SetQuestEventText(string questText)
        {
            _eventText.SetEventText(questText);
        }

        public void BossEntry()
        {
            _bossHealthBar.SetActive(true);
        }

        #region Observer

        public void Notify(string action)
        {
            //Chequeo si en mi diccionario de notificaciones existe esa notificacion
            if (_notifications.ContainsKey(action))
            {
                //Ejecuto la funcion que representa a la notificacion
                _notifications[action]();
            }
        }

        #endregion

        void FillNotifiesDictionary()
        {
            _notifications = new Dictionary<string, System.Action>();

            _notifications.Add("Begin", SetBoss);
            _notifications.Add("Die", DeadBoss);
        }

        void SetBoss()
        {
            _boss = GameObject.FindObjectOfType<BossHealth>();
            _eventText = GameObject.FindObjectOfType<EventText>();
            _bossHealthBar = GameObject.Find("BossHealthBar");
            _bossHealthBar.SetActive(false);
        }

        void DeadBoss()
        {
            finalConditionCompleted = true;
            _objectObserved.Unsubscribe(this);
            _bossHealthBar.SetActive(false);
        }
    }
}
