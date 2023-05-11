using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Core;

namespace RPG.Item
{
    public abstract class Potion : MonoBehaviour
    {
        [SerializeField] protected float potionValue;
        [SerializeField] protected GameObject potionVFX;
        public abstract void ConsumePotionSettings();
        public abstract void EquipSettings(CharaHealth health);
    }
}