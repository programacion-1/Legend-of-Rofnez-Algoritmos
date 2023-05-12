using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core
{
    public class QuestEnterTrigger : MonoBehaviour
    {
        bool _hasPlayerEntered;
        public bool HasPlayerEntered{get{return _hasPlayerEntered;}}
        [SerializeField] int playerLayer = 6;
        
        private void OnTriggerEnter(Collider other)
        {
            if(other.gameObject.layer == playerLayer && other.gameObject.tag == "Player") _hasPlayerEntered = true;
        }
    }
}
