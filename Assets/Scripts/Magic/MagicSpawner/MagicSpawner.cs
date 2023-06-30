using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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