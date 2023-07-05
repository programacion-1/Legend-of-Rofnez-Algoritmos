using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Magic
{
    public class EnemyMagicCaster : MagicCaster
    {
        private bool _isCasting = false; //Bool para chequear si el enemigo esta casteando la magia
        void Start()
        {
            SetParentMagicCasterStartSettings();
        }

        public bool CheckIfIsCastingMagic()
        {
            return _isCasting;
        }
        public override void ChildSetCurrentMagicSettings()
        {
            base.ChildSetCurrentMagicSettings();
            _isCasting = false;
        }

        public override bool MagicAttackChildCondition()
        {
            return !_isCasting; //Como necesito que la condición sea true pero para que pueda castear la magia no tiene que ser falso, le mando !_isCasting (por lo que si no esta casteando automaticamente se devuelve como true, de lo contrario se devolvería como false por lo que no castearía ya que ya estaría casteando)
        }

        public override void MagicAttackChildSettings()
        {
            _isCasting = true;
        }

        public override void ActionsAfterActivatingMagic()
        {
            base.ActionsAfterActivatingMagic();
            _isCasting = false;
        }
    }

}