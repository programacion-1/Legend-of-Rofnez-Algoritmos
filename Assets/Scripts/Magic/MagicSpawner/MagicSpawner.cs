using System.Collections;
using System.Collections.Generic;
using UnityEngine;

<<<<<<< HEAD
namespace RPG.Magic
{
    public abstract class MagicSpawner : MonoBehaviour
    {
        protected Transform _spawnerTransform;
        protected GameObject _magicToSpawn;
        protected Magic _equippedMagic;

        public abstract void CastMagic();

        public void EquipMagicOnSpawner(Magic m)
        {
            _equippedMagic = m;
        }

        public void SetSpawnerTransform(Transform t)
        {
            _spawnerTransform = t;
            transform.position = _spawnerTransform.position;
        }
    }

}
=======
public class MagicSpawner : MonoBehaviour
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
