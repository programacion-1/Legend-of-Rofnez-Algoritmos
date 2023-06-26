using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core
{
    public class PlayerHealth : CharaHealth
    {
        //[Header("Player Health Stats")]
        [SerializeField] string _healthBarName;
        //PlayerMinMaxQuantityText _healthText;
        //[SerializeField] string _healthBarTextName;
        

        public override void CharaDamageBehaviour()
        {
            base.CharaDamageBehaviour();
            TriggerChangeInteractiveBarValuesEvent();
        }

        public override void CharaDeathBehaviour()
        {
            base.CharaDeathBehaviour();
        }

        public void SetPlayerHealthStartingSettings()
        {
            CoreStartingSettings();
            TriggerChangeInteractiveBarValuesEvent();
        }

        public override void HealVisualSettings()
        {
            TriggerChangeInteractiveBarValuesEvent();
        }

        #region EventManager
        void TriggerChangeInteractiveBarValuesEvent()
        {
            EventManager.TriggerEvent(EventManager.Events.Event_ChangeInteractiveBarValues, _healthBarName, GetHP(), GetMaxHP());
        }
        #endregion

        void Start()
        {
            SetPlayerHealthStartingSettings();
        }
    }
}
