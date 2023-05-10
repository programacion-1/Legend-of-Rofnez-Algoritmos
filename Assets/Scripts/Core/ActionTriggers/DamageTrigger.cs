using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core
{
    public class DamageTrigger : MonoBehaviour
    {
        Health _target;
        
        public float _damageToDeal {get; set;}
        //public float _damageToDeal;

        public void SetDamageTarget(Health health)
        {
            _target = health;
        }

        public void DealDamage()
        {
            _target.TakeDamage(_damageToDeal);
        }
    }
}
