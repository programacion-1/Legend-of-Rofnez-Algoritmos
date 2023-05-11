using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core
{
    public abstract class BossHealth : CharaHealth
    {
        public void SetBossHealthStartingSettings()
        {
            CoreStartingSettings();
            BossChildHealthStartingSettings();
        }

        public abstract void BossChildHealthStartingSettings();

        void Start()
        {
            SetBossHealthStartingSettings();
        }
        public override void CharaDamageBehaviour()
        {
            BossDamageBehaviour();
        }

        public abstract void BossDamageBehaviour();

        public override void CharaDeathBehaviour()
        {
            BossDeathBehaviour();
        }

        public abstract void BossDeathBehaviour();

    }
}
