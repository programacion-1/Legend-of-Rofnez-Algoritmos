using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using RPG.UI;

namespace RPG.SceneManagement
{
    public class SceneLoader : MonoBehaviour
    {
        [SerializeField] int sceneToLoad = -1;
        [SerializeField] float fadeOutTime = 1f;
        [SerializeField] float fadeInTime = 2f;
        [SerializeField] float fadeWaitTime = 0.5f;
        [SerializeField] PersistantObjectDestroyer persistantObjectDestroyer;
        [SerializeField] int lastPlayableScene;
        int previousScene;
        
        bool canSetLevelSettings;

        public void SetPersistantObjectDestroyer(PersistantObjectDestroyer newObjectDestroyer)
        {
            persistantObjectDestroyer = newObjectDestroyer;
        }

        public void SetSceneToLoad(int newScene)
        {
            sceneToLoad = newScene;
        }

        public int GetSceneToLoad()
        {
            return sceneToLoad;
        }

        public int GetCurrentScene()
        {
            return SceneManager.GetActiveScene().buildIndex;
        }

        public IEnumerator TransitionBeginCo()
        {
            UIFade fader = FindObjectOfType<UIFade>();
            yield return fader.FadeIn(fadeInTime);
            previousScene = GetCurrentScene();
            transform.parent = null;
            DontDestroyOnLoad(gameObject);
            yield return new WaitForSeconds(fadeWaitTime);
            yield return SceneManager.LoadSceneAsync(sceneToLoad);
        }

        public IEnumerator TransitionEndCo()
        {
            UIFade fader = FindObjectOfType<UIFade>();
            yield return fader.FadeOut(fadeOutTime);
            yield return new WaitForSeconds(fadeWaitTime);
            SetLevelSetChecker(true);
            Debug.Log(GetCurrentScene());
            if(GetCurrentScene() == 0 || GetCurrentScene() > lastPlayableScene)
            {
                if(GameObject.FindObjectOfType<Menu>() != null) GameObject.FindObjectOfType<Menu>().SetRetryLevel(previousScene);
                Debug.Log(GetCurrentScene());
                persistantObjectDestroyer.RestartSpawner();
                persistantObjectDestroyer.CheckIfPersistantMustBeDestroyed();
                Destroy(Singleton.instance.gameObject);
                Destroy(gameObject);
            } 
            else
            {
                transform.parent = Singleton.instance.transform;
            }
            yield return new WaitForSeconds(0.1f);
            SetLevelSetChecker(false);
        }

        private void SetLevelSetChecker(bool checker)
        {
            canSetLevelSettings = checker;
        }

        public bool GetLevelSetChecker()
        {
            return canSetLevelSettings;
        }

    }
}
