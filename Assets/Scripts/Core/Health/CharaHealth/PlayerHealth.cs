using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.UI;

namespace RPG.Core
{
    public class PlayerHealth : CharaHealth
    {
        //[Header("Player Health Stats")]
        [SerializeField] InteractiveBar _healthBar;
        [SerializeField] string _healthBarName;
        //PlayerMinMaxQuantityText _healthText;
        //[SerializeField] string _healthBarTextName;
        

        public override void CharaDamageBehaviour()
        {
            _healthBar.ChangeBarFiller(GetHP(), GetMaxHP());
        }

        public override void CharaDeathBehaviour()
        {
            
        }

        public void SetPlayerHealthStartingSettings()
        {
            CoreStartingSettings();
            InteractiveBar[] interactiveBars = GameObject.FindObjectsOfType<InteractiveBar>();
            for (int i = 0; i < interactiveBars.Length; i++)
            {
                if (interactiveBars[i].gameObject.name == _healthBarName)
                {
                    _healthBar = interactiveBars[i];
                    _healthBar.ChangeBarFiller(GetHP(), GetMaxHP());
                    break;
                }
            }
            /*PlayerMinMaxQuantityText[] playerMinMaxQuantityTexts = GameObject.FindObjectsOfType<PlayerMinMaxQuantityText>();
            for (int i = 0; i < playerMinMaxQuantityTexts.Length; i++)
            {
                if (playerMinMaxQuantityTexts[i].gameObject.name == _healthBarTextName)
                {
                    _healthText = playerMinMaxQuantityTexts[i];
                    break;
                }
            }*/
        }

        public override void HealVisualSettings()
        {
            _healthBar.ChangeBarFiller(GetHP(), GetMaxHP());
        }

        void Start()
        {
            SetPlayerHealthStartingSettings();
        }
    }
}
