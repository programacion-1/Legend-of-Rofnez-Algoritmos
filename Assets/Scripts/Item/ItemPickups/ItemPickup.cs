using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Core;

namespace RPG.Item
{
    public abstract class ItemPickup : MonoBehaviour
    {
        AudioManager audioManager;
        [SerializeField] AudioClip itemClip;
        [SerializeField] int playerLayer = 6;

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer == playerLayer)
            {
                audioManager = GameObject.FindObjectOfType<AudioManager>();
                audioManager.PlayClip(audioManager.ItemSource, itemClip);
                UseItem(other.gameObject);
                Destroy(this.gameObject);
            }    
        }

        public abstract void UseItem(GameObject player);
    }
}
