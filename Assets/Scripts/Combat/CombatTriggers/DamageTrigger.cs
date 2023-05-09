using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Core;

namespace RPG.Combat
{
    public class DamageTrigger : MonoBehaviour
    {
        Health _target;
        
        public float _damageToDeal
        {
            get{return _damageToDeal;}
            set{_damageToDeal = value;}
        }

        public void SetTarget(Health health)
        {
            _target = health;
        }

        public void DealDamage()
        {
            _target.TakeDamage(_damageToDeal);
        }
    }
}
