using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Core;

namespace RPG.Magic
{
    [CreateAssetMenu(fileName = "Weapon", menuName = "My Scriptable Objects/Magics/New Magic Area", order = 0)]
    public class MagicArea : Magic
    {
        MagicAreaTrigger _magicAreaTrigger;
        const string _magicTypeName = "Area";
        public override void InstantiateMagic(Transform t, Health h)
        {
            GameObject magicInstance = Instantiate(equippedPrefab, t.position, t.rotation);
            _magicAreaTrigger = magicInstance.GetComponent<MagicAreaTrigger>();
            _magicAreaTrigger.SetAreaDamage(magicDamage);
        }

        public override void SetMagicType()
        {
            _magicType = _magicTypeName;
        }
    }

}