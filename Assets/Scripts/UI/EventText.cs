using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.UI
{
    public class EventText : MonoBehaviour
    {
        Text eventText;
        [SerializeField] string beginningText;
        void Awake()
        {
            eventText = GetComponent<Text>();
            ResetEventText();
        }

        public string GetEventText()
        {
            return eventText.text;
        }

        public void SetEventOnText(string newText)
        {
            eventText.text += "\r\n~" + newText;
        }

        public void ResetEventText()
        {
            eventText.text = beginningText;
            Debug.Log($"reset");
        }
    }
}
