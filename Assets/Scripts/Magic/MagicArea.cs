using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Core;
using GeneralEnums;

namespace RPG.Magic
{
    [CreateAssetMenu(fileName = "Weapon", menuName = "My Scriptable Objects/Magics/New Magic Area", order = 0)]
    public class MagicArea : Magic
    {
        MagicAreaTrigger _magicAreaTrigger;
        [SerializeField] MagicAreaSpawner _magicAreaSpawner;
        public MagicAreaSpawner magicAreaSpawner{get{return _magicAreaSpawner;}}
        public override MagicSpawner InstantiateMagicSpawner(params object[] p)
        {
            Transform t = (Transform) p[0];
            GameObject magicSpawner = Instantiate(_magicAreaSpawner.gameObject, t.position, t.rotation);
            return magicSpawner.GetComponent<MagicAreaSpawner>();
        }

        
        public override void SetMagicType()
        {
            _magicType = MagicType.Area;
            _magicTypeName = MagicType.Area.ToString();
        }

    }

}