using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Core;

namespace RPG.Magic
{
    public class MagicAreaSpawner : MagicSpawner
    {
        MagicAreaTrigger _magicAreaTrigger;
        public override void CastMagic()
        {
            GameObject magicInstance = Instantiate(_magicToSpawn, _spawnerTransform.position, _spawnerTransform.rotation);
            _magicAreaTrigger = magicInstance.GetComponent<MagicAreaTrigger>();
            _magicAreaTrigger.SetAreaDamage(_equippedMagic.magicDamage);
        }

    }

}