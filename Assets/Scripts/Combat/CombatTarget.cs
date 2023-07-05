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

        public void SetAttackers(int[] newAttackers)
        {
            attackers = newAttackers;
        }
        
        private void OnTriggerEnter(Collider other)
        {
            for(int i = 0; i < attackers.Length; i++)
            {
                if(other.gameObject.layer == attackers[i] && other.GetComponent<DamageTrigger>() != null)
                {
                    if(!_health.CheckIfIsInvencible())
                    {
                        other.GetComponent<DamageTrigger>().SetDamageTarget(_health);
                        other.GetComponent<DamageTrigger>().DealDamage();
                    } 
                    break;                    
                }
           }
        }
    }
}