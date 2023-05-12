using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.UI
{
    public class EventText : MonoBehaviour
    {
        [SerializeField] Text eventText;
        void Awake()
        {
            eventText = GetComponent<Text>();
            SetEventText("");
        }

        public string GetEventText()
        {
            return eventText.text;
        }

        public void SetEventText(string newText)
        {
            eventText.text = newText;
        }
    }
}
