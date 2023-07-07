using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.SceneManagement;
using RPG.GameCore;

namespace RPG.UI
{    
    public class PauseMenu : MonoBehaviour
    {
        PauseManager pauseManager;
        SceneLoader sceneLoader;
        MenuController menuController;

        private void Start()
        {
            pauseManager = GameObject.FindObjectOfType<PauseManager>();
            sceneLoader = GameObject.FindObjectOfType<SceneLoader>();
            menuController = GameObject.FindObjectOfType<MenuController>();
        }

        public void ReturnToMainMenu(int sceneToLoad)
        {
            sceneLoader.SetSceneToLoad(sceneToLoad);
            StartCoroutine(ReturnToMainMenuCo());
        }

        private IEnumerator ReturnToMainMenuCo()
        {
            yield return sceneLoader.TransitionBeginCo();
            yield return sceneLoader.TransitionEndCo();
        }

        public void QuitGame()
        {
            StartCoroutine(QuitCo());
        }
        
        private IEnumerator QuitCo()
        {
            UIFade fader = GameObject.FindObjectOfType<UIFade>();
            yield return fader.FadeOut(2f);
            Application.Quit();
        }
    }
}
