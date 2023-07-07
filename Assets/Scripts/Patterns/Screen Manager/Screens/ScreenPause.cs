using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using RPG.UI;

public class ScreenPause : MonoBehaviour, IScreen
{
    PauseMenu _pauseMenu;
    public void Activate()
    {
        Debug.Log("Activate Inventory Screen");
        TriggerSetScreenActiveEvent(true);
        _pauseMenu = GameObject.FindObjectOfType<PauseMenu>();
    }

    public void Deactivate()
    {
        Debug.Log("Deactivate Inventory Screen");
        TriggerSetScreenActiveEvent(false);
    }

    public void Free()
    {
        Destroy(gameObject);
    }

    public void ContinueButton()
    {        
        TriggerSetScreenActiveEvent(false);
    }

    public void QuitMenuButton(int mainMenuScene)
    {
        _pauseMenu.ReturnToMainMenu(mainMenuScene);
        TriggerSetScreenActiveEvent(false);
    }

    public void QuitGameButton()
    {
        _pauseMenu.QuitGame();
        TriggerSetScreenActiveEvent(false);
    }

    #region EventManager
    void TriggerSetScreenActiveEvent(bool active)
    {
        EventManager.TriggerEvent(EventManager.Events.Event_SetIfIsAScreenActiveOnScene, active);
    }
    #endregion
}
