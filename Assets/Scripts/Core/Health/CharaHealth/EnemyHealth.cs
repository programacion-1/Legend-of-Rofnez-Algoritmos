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
        void Start()
        {
            rewardDrop = GetComponent<RewardDrop>();
            CoreStartingSettings();
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