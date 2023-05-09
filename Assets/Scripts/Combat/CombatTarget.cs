using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Core;

namespace RPG.Combat
{
    [RequireComponent(typeof(Health))] //Cada vez que agregues el componente CombatTarget, Health se agregará automáticamente. Y si intentas borrar Health de un componente con CombatTarget, Unity no te va a dejar
    public class CombatTarget : MonoBehaviour
    {
        Health _health;
        [SerializeField] int[] attackers;
        private void Start()
        {
            _health = GetComponent<Health>();
        }
        
        private void OnTriggerEnter(Collider other)
        {
            for(int i = 0; i < attackers.Length; i++)
            {
                if(other.gameObject.layer == attackers[i])
                {
                    if(!_health.CheckIfIsInvencible()) _health.TakeDamage(other.transform.parent.GetComponent<AttackTrigger>().GetTriggerDamage());
                    break;                    
                }
           }
        }
    }
}