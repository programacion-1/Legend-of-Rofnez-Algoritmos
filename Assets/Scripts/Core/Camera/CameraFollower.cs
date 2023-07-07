using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core
{
        public class CameraFollower : MonoBehaviour
    {
        [SerializeField] float camSpeed;
        [SerializeField] private Transform target;
        [SerializeField] private string targetName;
        
        private void Start()
        {
            SetCameraStartingSettings();
        }

        public void SetCameraStartingSettings()
        {
            target = GameObject.Find(targetName).transform;
            transform.position = target.position;
        }

        // Update is called once per frame
        void Update()
        {
            if(target != null) transform.position = target.position;
        }
    }

}