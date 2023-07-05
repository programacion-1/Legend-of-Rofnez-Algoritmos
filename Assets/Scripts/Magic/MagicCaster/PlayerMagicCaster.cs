using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Magic
{
    public class PlayerMagicCaster : MagicCaster
    {
        MagicPoints _magicPoints;


        public void SetPlayerMagicCasterStartSettings()
        {
            SetParentMagicCasterStartSettings();
            _magicPoints = GetComponent<MagicPoints>();
        }

        void Start()
        {
            SetPlayerMagicCasterStartSettings();
        }
        public override void MagicAttackChildSettings()
        {
            _magicPoints.ConsumeMagicPoints(_currentMagic.mpToConsume);
        }

        public override void ChildSetCurrentMagicSettings()
        {
            UpdateCurrentMagicSprite();
        }

        public void UpdateCurrentMagicSprite()
        {
            /*magicInventoryMenu.SetCurrentMagicSprite(currentMagic.GetMagicSprite());
            magicInventoryMenu.SetCurrentMagicImage();*/
        }

        public override bool MagicAttackChildCondition()
        {
            return _currentMagic.mpToConsume <= _magicPoints.magicPoints;
        }

        public override void ActionsAfterActivatingMagic()
        {
            base.ActionsAfterActivatingMagic();
        }
    }

}