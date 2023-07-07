using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Control
{
    public class AiIDSetter : MonoBehaviour
    {
        AIController[] _aiControllers;
        
        public void SetControllers(AIController[] controllers)
        {
            _aiControllers = controllers;
            for(int i = 0; i < _aiControllers.Length; i++)
            {
                _aiControllers[i].AiID = i;
            }
        }

    }
}