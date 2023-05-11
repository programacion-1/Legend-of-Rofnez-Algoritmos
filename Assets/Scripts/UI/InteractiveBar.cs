using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.UI
{
    public class InteractiveBar : MonoBehaviour, IObserver
    {
        [SerializeField]Image _interactiveBar;
        public void ChangeBarFiller(float amount, float maxAmount)
        {
            _interactiveBar.fillAmount = amount / maxAmount;
        }

        public void Notify(string action)
        {
            throw new System.NotImplementedException();
        }
    }
}