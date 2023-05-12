using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core
{
    public class KeyBearer : MonoBehaviour
    {
        Health _health;
        void Start()
        {
            _health = GetComponent<Health>();
        }

        public bool BearerIsDead()
        {
            return _health.CheckIfIsDead();
        }
    }
}
