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
            SetQuestEventText(_questTexts[0]);
        }
        
        public override void QuestChecker()
        {
            if(!keyHasAppeared)
            {
                if(_firstQuestEnterTrigger.HasPlayerEntered) SetQuestEventText(_questTexts[1]);
                if(_keyBearer.BearerIsDead())
                {
                    SetQuestEventText(_questTexts[2]);
                    BossEntry();
                    keyHasAppeared = true;
                    key.SetActive(true);
                }
            }
            else SetQuestEventText(_questTexts[3]);
            
            if(finalConditionCompleted)
            {
                foreach(EnemyHealth mob in bossMobs)
                {
                    mob.DisableInvencibility();
                    mob.TakeDamage(mob.GetMaxHP());
                } 
                SetQuestEventText(_questTexts[4]);
                CompleteQuests();
            }
        }
    }
}
