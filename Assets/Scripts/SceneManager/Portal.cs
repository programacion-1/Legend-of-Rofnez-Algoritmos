﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AI;
using RPG.Core;
using RPG.GameCore;

namespace RPG.SceneManagement
{
    public class Portal : MonoBehaviour
    {
        [SerializeField] int sceneDestination = -1;
        [SerializeField] Transform spawnPoint;
        [SerializeField] bool canBeUsed;
        [SerializeField] bool isOnPortal;
        AudioManager audioManager;
        [SerializeField] AudioClip portalClip;

        private void Start()
        {
            audioManager = GameObject.FindObjectOfType<AudioManager>();
            if(!canBeUsed)
            {
                transform.GetChild(0).gameObject.SetActive(false);
            }
        }

        public bool IsPlayerOnPortal()
        {
            return isOnPortal;
        }
        private void OnTriggerEnter(Collider other)
        {
            if(other.tag == "Player" && canBeUsed)    
            {
                StartCoroutine(TransitionCo());
                isOnPortal = true;
            }
        }

        private IEnumerator TransitionCo()
        {
            GameManager.instance.SavePlayerStats();
            GameObject.FindWithTag("Player").GetComponent<ActionScheduler>().CancelCurrentAction();
            audioManager.StopSource(audioManager.BGM);
            audioManager.StopSource(audioManager.Ambience);
            audioManager.PlayClip(audioManager.ItemSource, portalClip);
            SceneLoader sceneLoader = GameObject.FindObjectOfType<SceneLoader>();
            sceneLoader.SetSceneToLoad(sceneDestination);
            DontDestroyOnLoad(gameObject);
            yield return sceneLoader.TransitionBeginCo();

            /*Portal otherPortal = GetOtherPortal();
            if(otherPortal != null)
            {
                UpdatePlayer(otherPortal);
                GameManager.instance.LoadPlayerStats();
            }*/

            yield return sceneLoader.TransitionEndCo();
            Destroy(gameObject);
        }

        private Portal GetOtherPortal()
        {
            foreach(Portal portal in FindObjectsOfType<Portal>())
            {
                if(portal == this) continue;
                return portal;
            }
            return null;
        }

        private void UpdatePlayer(Portal otherPortal)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            player.GetComponent<NavMeshAgent>().Warp(otherPortal.spawnPoint.position);
            player.transform.rotation = otherPortal.spawnPoint.rotation;
        }
    }
}
