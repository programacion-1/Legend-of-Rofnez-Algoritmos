using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Magic
{
    public class MagicPoints : MonoBehaviour
    {
        [SerializeField] float _magicPoints;
        public float magicPoints{get{return _magicPoints;}}        
        [SerializeField] float maxMagicPoints;
        
        /*HealBar bar;
        [SerializeField] string magicBarName;
        PlayerMinMaxQuantityText magicBarText;
        [SerializeField] string magicBarTextName;*/
        // Start is called before the first frame update
        
        void Start()
        {
            SetStartingMagicPointsSettings();
        }

        public void SetStartingMagicPointsSettings()
        {
            _magicPoints = maxMagicPoints;
            /*HealBar[] healbars = GameObject.FindObjectsOfType<HealBar>();
            for(int i = 0; i < healbars.Length; i++)
            {
                if(healbars[i].gameObject.name == magicBarName)
                {
                    bar = healbars[i];
                    break;
                }
            }
            PlayerMinMaxQuantityText[] playerMinMaxQuantityTexts = GameObject.FindObjectsOfType<PlayerMinMaxQuantityText>();
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
            /*if(bar != null) bar.ChangeBarFiller(_magicPoints, maxMagicPoints);
            if(magicBarText != null) magicBarText.SetQuantityText(_magicPoints, maxMagicPoints);*/
        }

        public void RestoreMagicPoints(float mptToRestore)
        {
            _magicPoints = Mathf.Min(_magicPoints + mptToRestore, maxMagicPoints);
            //bar.ChangeBarFiller(_magicPoints, maxMagicPoints);
        }
    }

}