using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Core;
using RPG.UI;

namespace RPG.MVC.Player
{
    public class PlayerView : MonoBehaviour
    {
        //Audio
        AudioManager _audioManager;
        AudioClip _runClip;
        
        //UI
        MenuController _menuController;
        // Start is called before the first frame update

        public PlayerView(PlayerMVC playerMVC)
        {
            _audioManager = playerMVC.AudioManager;
            _runClip = playerMVC.RunClip;
            _menuController = playerMVC.MenuController;
        }

        // Update is called once per frame
        void Update()
        {
            
        }

        #region Audio
        public void PlayRunClip()
        {
             _audioManager.PlayClip(_audioManager.PlayerRunSource, _runClip);
        }

        public void StopRunClip()
        {
            _audioManager.StopClipFromSource(_audioManager.PlayerRunSource, _runClip);
        }

        public void TryPlayAudio(AudioClip clip)
        {
            _audioManager.TryToPlayClip(_audioManager.PlayerSFXSources, clip);
        }

        #endregion

        #region UI

        public void CheckGodModeView(bool godMode)
        {
            if(godMode) _menuController.ShowUIObject(_menuController.GetGodModeText());
            else _menuController.HideUIObject(_menuController.GetGodModeText());
        }

        #endregion
    }
}
