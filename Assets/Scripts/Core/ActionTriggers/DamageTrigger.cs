using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core
{
    public class DamageTrigger : MonoBehaviour
    {
        Health _target;
        BoxCollider boxCollider;
        public float _damageToDeal {get; set;}
        void OnEnable()
        {
            boxCollider = GetComponent<BoxCollider>();
        }

        public void SetDamageTriggerColliderStats(Vector3 center, Vector3 size)
        {
            boxCollider.center = center;
            boxCollider.size = size;
        }

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
