using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.UI;

namespace RPG.Core
{
    public class BossLvl1Health : BossHealth
    {
        public override void SetBossHealthStartingSettings(params object[] p)
        {
            base.SetBossHealthStartingSettings(p);
        }

        public override void BossDamageBehaviour()
        {
            base.BossDamageBehaviour();
        }

        public override void BossDeathBehaviour()
        {
            base.BossDeathBehaviour();
        }
        
    }

}