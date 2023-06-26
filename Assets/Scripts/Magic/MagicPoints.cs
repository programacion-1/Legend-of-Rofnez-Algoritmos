using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.UI;

namespace RPG.Magic
{
    public class MagicPoints : MonoBehaviour
    {
        [SerializeField] float _magicPoints;
        public float magicPoints{get{return _magicPoints;} set{_magicPoints = value;}}        
        [SerializeField] float _maxMagicPoints;
        public float maxMagicPoints{get{return _maxMagicPoints;} set{_maxMagicPoints = value;}}        
        
        [SerializeField] InteractiveBar _manaBar;
        [SerializeField] string _magicBarName;
        PlayerMinMaxQuantityText magicBarText;
        /*[SerializeField] string magicBarTextName;*/
        // Start is called before the first frame update
        
        void Start()
        {
            SetStartingMagicPointsSettings();
        }

        public void SetStartingMagicPointsSettings()
        {
            _magicPoints = _maxMagicPoints;
            TriggerChangeInteractiveBarValuesEvent();
        }

        public void ConsumeMagicPoints(float mpToConsume)
        {
            _magicPoints = Mathf.Max(_magicPoints - mpToConsume, 0);
            TriggerChangeInteractiveBarValuesEvent();
        }

        public void RestoreMagicPoints(float mptToRestore)
        {
            _magicPoints = Mathf.Min(_magicPoints + mptToRestore, _maxMagicPoints);
            TriggerChangeInteractiveBarValuesEvent();
        }

        #region EventManager
        void TriggerChangeInteractiveBarValuesEvent()
        {
            EventManager.TriggerEvent(EventManager.Events.Event_ChangeInteractiveBarValues, _magicBarName, _magicPoints, _maxMagicPoints);
        }
        #endregion
    }

}