using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.UI
{
    public class InteractiveBar : MonoBehaviour
    {
        [SerializeField]Image healBar;
        
        #region EventManager
        private void OnEnable()
        {
            EventManager.SubscribeToEvent(EventManager.Events.Event_ChangeInteractiveBarValues, ChangeInteractiveBarValues);
        }

        public void OnDisable()
        {
            EventManager.UnsubscribeToEvent(EventManager.Events.Event_ChangeInteractiveBarValues, ChangeInteractiveBarValues);
        }
        #endregion

        public void ChangeBarFiller(float amount, float maxAmount)
        {
            healBar.fillAmount = amount / maxAmount;
        }

        public void ChangeInteractiveBarValues(params object[] p)
        {
            string barName = (string) p[0];
            float amount = (float) p[1];
            float maxAmount = (float) p[2];
            if(barName == gameObject.name) healBar.fillAmount = amount / maxAmount;
        }

    }
}