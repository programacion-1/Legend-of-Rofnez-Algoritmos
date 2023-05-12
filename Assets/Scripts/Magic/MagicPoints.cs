using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.UI;

namespace RPG.Magic
{
    public class MagicPoints : MonoBehaviour
    {
        [SerializeField] float _magicPoints;
        public float magicPoints{get{return _magicPoints;}}        
        [SerializeField] float maxMagicPoints;
        
        [SerializeField] InteractiveBar _manaBar;
        [SerializeField] string magicBarName;
        PlayerMinMaxQuantityText magicBarText;
        /*[SerializeField] string magicBarTextName;*/
        // Start is called before the first frame update
        
        void Start()
        {
            SetStartingMagicPointsSettings();
        }

        public void SetStartingMagicPointsSettings()
        {
            _magicPoints = maxMagicPoints;
            InteractiveBar[] interactivebars = GameObject.FindObjectsOfType<InteractiveBar>();
            for(int i = 0; i < interactivebars.Length; i++)
            {
                if(interactivebars[i].gameObject.name == magicBarName)
                {
                    _manaBar = interactivebars[i];
                    _manaBar.ChangeBarFiller(_magicPoints, maxMagicPoints);
                    break;
                }
            }
            /*PlayerMinMaxQuantityText[] playerMinMaxQuantityTexts = GameObject.FindObjectsOfType<PlayerMinMaxQuantityText>();
            for (int i = 0; i < playerMinMaxQuantityTexts.Length; i++)
            {
                if (playerMinMaxQuantityTexts[i].gameObject.name == magicBarTextName)
                {
                    magicBarText = playerMinMaxQuantityTexts[i];
                    break;
                }
            }
            bar.ChangeBarFiller(magicPoints, maxMagicPoints);
            magicBarText.SetQuantityText(magicPoints, maxMagicPoints);*/
        }

        public void ConsumeMagicPoints(float mpToConsume)
        {
            _magicPoints = Mathf.Max(_magicPoints - mpToConsume, 0);
            _manaBar.ChangeBarFiller(_magicPoints, maxMagicPoints);
        }

        public void RestoreMagicPoints(float mptToRestore)
        {
            _magicPoints = Mathf.Min(_magicPoints + mptToRestore, maxMagicPoints);
            _manaBar.ChangeBarFiller(_magicPoints, maxMagicPoints);
        }
    }

}