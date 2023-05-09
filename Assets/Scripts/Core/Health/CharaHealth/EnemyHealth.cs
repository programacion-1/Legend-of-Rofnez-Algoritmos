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

        }

        public override void CharaDeathBehaviour()
        {
            rewardDrop.CheckIfCanDropReward();
        }
    }
}