using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.UI
{
    public class InteractiveBar : MonoBehaviour
    {
        [SerializeField]Image _interactiveBar;
        public void ChangeBarFiller(float amount, float maxAmount)
        {
            _interactiveBar.fillAmount = amount / maxAmount;
        }

    }
}