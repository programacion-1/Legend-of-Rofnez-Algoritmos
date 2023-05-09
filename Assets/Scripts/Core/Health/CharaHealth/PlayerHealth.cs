using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core
{
    public class PlayerHealth : CharaHealth
    {
        //[Header("Player Health Stats")]
        

        public override void CharaDamageBehaviour()
        {
            
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
    }
}
