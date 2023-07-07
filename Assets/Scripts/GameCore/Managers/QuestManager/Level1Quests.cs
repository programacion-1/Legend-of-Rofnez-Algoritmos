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
        [SerializeField] QuestEnterTrigger _firstQuestEnterTrigger;
        KeyBearer _keyBearer;
        [SerializeField] EnemyHealth[] bossMobs;
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
                if(_firstQuestEnterTrigger.HasPlayerEntered) SetQuestEventText(1);
                if(_keyBearer.BearerIsDead())
                {
                    SetQuestEventText(2);
                    BossEntry();
                    keyHasAppeared = true;
                    key.SetActive(true);
                }
            }
            
            if(finalConditionCompleted)
            {
                foreach(EnemyHealth mob in bossMobs)
                {
                    mob.DisableInvencibility();
                    mob.TakeDamage(mob.GetMaxHP());
                } 
                SetQuestEventText(4);
                CompleteQuests();
            }
        }
    }
}
