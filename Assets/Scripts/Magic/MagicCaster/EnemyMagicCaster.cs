using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Magic
{
    public class EnemyMagicCaster : MagicCaster
    {
        void Start()
        {
            SetParentMagicCasterStartSettings();
        }
        public override void ChildSetCurrentMagicSettings()
        {
            base.ChildSetCurrentMagicSettings();
        }

        public override bool MagicAttackChildCondition()
        {
            return base.MagicAttackChildCondition();
        }

        public override void MagicAttackChildSettings()
        {
            
        }
    }

}