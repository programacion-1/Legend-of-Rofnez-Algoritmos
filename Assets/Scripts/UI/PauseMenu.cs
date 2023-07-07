using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.SceneManagement;
using RPG.GameCore;
using RPG.Core;

namespace RPG.UI
{    
    public class PauseMenu : MonoBehaviour
    {
        SceneLoader _sceneLoader;
        MenuController _menuController;
        AudioManager _audioManager;

        private void Start()
        {
            _sceneLoader = GameObject.FindObjectOfType<SceneLoader>();
            _audioManager = GameObject.FindObjectOfType<AudioManager>();
            _menuController = GameObject.FindObjectOfType<MenuController>();
        }

        public void ReturnToMainMenu(int sceneToLoad)
        {
            _sceneLoader.SetSceneToLoad(sceneToLoad);
            _audioManager.StopSource(_audioManager.BGM);
            _audioManager.StopSource(_audioManager.Ambience);
            UIFade fader = GameObject.FindObjectOfType<UIFade>();
            StartCoroutine(ReturnToMainMenuCo());
        }

        private IEnumerator ReturnToMainMenuCo()
        {
            yield return _sceneLoader.TransitionBeginCo();
            yield return _sceneLoader.TransitionEndCo();
        }

        public void QuitGame()
        {
            _audioManager.StopSource(_audioManager.BGM);
            _audioManager.StopSource(_audioManager.Ambience);
            StartCoroutine(QuitCo());
        }
        
        private IEnumerator QuitCo()
        {
            UIFade fader = GameObject.FindObjectOfType<UIFade>();
            yield return fader.FadeIn(2f);
            Application.Quit();
        }
    }
}
