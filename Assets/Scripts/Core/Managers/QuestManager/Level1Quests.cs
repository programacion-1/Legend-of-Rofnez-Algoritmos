using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.UI;

namespace RPG.Core
{
    public class Level1Quests : QuestManager
    {
        [SerializeField] GameObject key;
        bool keyHasAppeared = false;
        //[SerializeField] int deathsNeeded = 7;
        KeyBearer _keyBearer;
        //Deathcounter deathcounter;

        public override void LevelStartingSettings()
        {
            _keyBearer = FindObjectOfType<KeyBearer>();
            key.SetActive(false);
        }
        
        public override void QuestChecker()
        {
            if(!keyHasAppeared)
            {
                if(_keyBearer.BearerIsDead())
                {
                    SetQuestEventText();
                    BossEntry();
                    keyHasAppeared = true;
                    key.SetActive(true);
                }
            }
            
            if(finalConditionCompleted)
            {
                CompleteQuests();
            }
        }
    }
}
