﻿using System.Collections;
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
        public void ResumeButton()
        {
            pauseManager.SetPause(false);
        }

        public void ReturnToMainMenuButton(int sceneToLoad)
        {
            sceneLoader.SetSceneToLoad(sceneToLoad);
            pauseManager.SetPause(false);
            StartCoroutine(ReturnToMainMenuCo());
        }

        public void ReturnToMainMenu()
        {
            StartCoroutine(ReturnToMainMenuCo());
        }

        private IEnumerator ReturnToMainMenuCo()
        {
            ResumeButton();
            yield return sceneLoader.TransitionBeginCo();
            yield return sceneLoader.TransitionEndCo();
        }

        public void QuitGameButton()
        {
            pauseManager.SetPause(false);
            StartCoroutine(QuitCo());
        }

        public void QuitGame()
        {
            StartCoroutine(QuitCo());
        }
        
        private IEnumerator QuitCo()
        {
            ResumeButton();
            UIFade fader = GameObject.FindObjectOfType<UIFade>();
            yield return fader.FadeOut(2f);
            Application.Quit();
        }
    }
}
