using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.GameCore
{
    public class GameScreenManager : MonoBehaviour
    {
        [SerializeField] Transform _mainGame;
        bool _hasScreenActive;
        //Transform UICanvas; => pensado para que los screen se setearan en el canvas principal pero al hacer esto no se muestra nada por lo que queda inutilizada de momento

        // Start is called before the first frame update
        public void StartSettings()
        {
            ScreenManager.Instance.ResetManager();
            _mainGame = GameObject.Find("Main Game").transform;
            ScreenManager.Instance.Push(new ScreenGameplay(_mainGame));
        }

        // Update is called once per frame
        public void ListenScreenManagerKeys()
        {
            if(!_hasScreenActive) ScreenManager.Instance.Pop();
            if(Input.GetKeyDown(KeyCode.Tab))
            {
                var screenInventory = Instantiate(Resources.Load<ScreenInventory>("Inventory Screen"));
                ScreenManager.Instance.Push(screenInventory);
            }
            if(Input.GetKeyUp(KeyCode.Tab))
            {
                ScreenManager.Instance.Pop();
            }
            if(Input.GetKeyDown(KeyCode.Escape))
            {
                if(!_hasScreenActive)
                {
                    var screenPause = Instantiate(Resources.Load<ScreenPause>("Pause Menu"));
                    ScreenManager.Instance.Push(screenPause);
                }
                else ScreenManager.Instance.Pop();
            }
        }

        #region EventManager
        private void OnEnable()
        {
            EventManager.SubscribeToEvent(EventManager.Events.Event_SetIfIsAScreenActiveOnScene, SetScreenActive);
        }

        private void OnDisable()
        {
            EventManager.UnsubscribeToEvent(EventManager.Events.Event_SetIfIsAScreenActiveOnScene, SetScreenActive);
        }

        private void SetScreenActive(params object[] p)
        {
            _hasScreenActive = (bool) p[0];            
        }

        #endregion
    }
}
