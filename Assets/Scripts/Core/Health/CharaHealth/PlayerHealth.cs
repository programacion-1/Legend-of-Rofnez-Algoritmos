using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core
{
    public class PlayerHealth : CharaHealth
    {
        public override void CharaDamageBehaviour()
        {
            EnableInvencibilityCo(0.05f);
        }

        public override void CharaDeathBehaviour()
        {
            
        }
    }
}
