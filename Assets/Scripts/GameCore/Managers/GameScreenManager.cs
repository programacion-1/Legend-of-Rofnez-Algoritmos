using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.GameCore
{
    public class GameScreenManager : MonoBehaviour
    {
        [SerializeField] Transform mainGame;

        // Start is called before the first frame update
        void Start()
        {
            ScreenManager.Instance.Push(new ScreenGameplay(mainGame));
        }

        // Update is called once per frame
        void Update()
        {
            if(Input.GetKeyDown(KeyCode.Tab))
            {
                var screenInventory = Instantiate(Resources.Load<ScreenInventory>("Inventory Screen"));
                ScreenManager.Instance.Push(screenInventory);
            }
            if(Input.GetKeyUp(KeyCode.Tab))
            {
                ScreenManager.Instance.Pop();
            }
        }
    }
}
