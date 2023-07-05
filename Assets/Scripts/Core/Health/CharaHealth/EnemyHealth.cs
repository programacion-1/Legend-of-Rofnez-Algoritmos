using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core
{
    public class EnemyHealth : CharaHealth
    {
        [Header("Enemy Health Stats")]
        private RewardDrop rewardDrop;

        // Start is called before the first frame update
        public override void CoreStartingSettings(params object[] p)
        {
            base.CoreStartingSettings(p);
            rewardDrop = GetComponent<RewardDrop>();
            SetHealthEnemyStats((float)p[0], (float)p[1], (string)p[2]);
        }
        
        public void SetHealthEnemyStats(float damageRate, float returonToWhiteColourTimer, string deadTriggerName)
        {
            _damageRate = damageRate;
            _returnToWhiteColourTimer = returonToWhiteColourTimer;
            _deadTriggerName = deadTriggerName;
        }
        public override void CharaDamageBehaviour()
        {
            base.CharaDamageBehaviour();
            TriggerAIHasBeenAttacked();
        }

        public override void CharaDeathBehaviour()
        {
            base.CharaDeathBehaviour();
            rewardDrop.CheckIfCanDropReward();
        }

        public override void HealVisualSettings()
        {
            base.HealVisualSettings();
        }

        #region EventManager
        void TriggerAIHasBeenAttacked()
        {
            EventManager.TriggerEvent(EventManager.Events.Event_AIHasBeenAttacked, _healthID);
        }
        #endregion

    }
}