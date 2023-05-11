using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Core;

namespace RPG.Item
{
    public class HealPotion : Potion
    {
        CharaHealth _target;

        public override void ConsumePotionSettings()
        {
            _target.Heal(potionValue);
            _target.InstantiateVFX(potionVFX);
        }

        public override void EquipSettings(CharaHealth health)
        {
            _target = health;
        }
    }

}