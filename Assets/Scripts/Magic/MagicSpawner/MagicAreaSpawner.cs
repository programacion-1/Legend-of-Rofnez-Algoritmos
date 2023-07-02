using System.Collections;
using System.Collections.Generic;
using UnityEngine;
<<<<<<< HEAD
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
=======

public class MagicAreaSpawner : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
>>>>>>> parent of 9053899 (Revert "actualizacion de assets")
