using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Core;
using RPG.Magic;

namespace RPG.Item
{
    public class ManaPotion : Potion
    {
        CharaHealth _target;
        MagicPoints _magicPoints;

        public override void ConsumePotionSettings()
        {
            _magicPoints.RestoreMagicPoints(potionValue);
            _target.InstantiateVFX(potionVFX);
        }

        public override void EquipSettings(CharaHealth health)
        {
            _target = health;
            _magicPoints = _target.GetComponent<MagicPoints>();
        }
    }

}