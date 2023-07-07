using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.UI;
using RPG.SceneManagement;
using RPG.Obstacle;
using RPG.Magic;
using RPG.InventorySystem;
using RPG.Core;
using RPG.Control;

namespace RPG.GameCore
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager instance;
        PlayerHealth player;
        QuestManager questManager;
        AiIDSetter aiIDSetter;
        [SerializeField] GameScreenManager gameScreenManager;
        PlayerStatsManager playerStatsManager;
        AutoSavePlayerStatsDataManager autoSavePlayerStatsDataManager;
        PauseManager pauseManager;
        AudioManager audioManager;
        LevelMusic levelMusic;
        [SerializeField] int loseScene;
        SceneLoader sceneLoader;
        int currentScene;
        bool failCondiction;
        bool isLoadingScene;

        void Start()
        {
            if(instance == null) instance = this;
            else Destroy(gameObject);
            player = GameObject.FindObjectOfType<PlayerHealth>();
            playerStatsManager = GetComponent<PlayerStatsManager>();
            SavePlayerStats();
            autoSavePlayerStatsDataManager = GetComponent<AutoSavePlayerStatsDataManager>();
            autoSavePlayerStatsDataManager.SetPlayerStatsManager(playerStatsManager);
            aiIDSetter = GetComponent<AiIDSetter>();
            pauseManager = GetComponent<PauseManager>();
            sceneLoader = GetComponent<SceneLoader>();
            audioManager = GameObject.FindObjectOfType<AudioManager>();
            SetLevelSettings();
        }

        void Update()
        {
            /*if(Input.GetButtonDown("Cancel"))
            {
                if(pauseManager.GetPauseState()) pauseManager.SetPause(false);
                else pauseManager.SetPause(true);
            }*/
        }
        
        void LateUpdate()
        {
            if(!isLoadingScene)
            {
                if(autoSavePlayerStatsDataManager.CheckIfICanAutoSave()) StartCoroutine(autoSavePlayerStatsDataManager.autoSave());
                gameScreenManager.ListenScreenManagerKeys();
                if(!failCondiction)
                {
                    if(!player.CheckIfIsDead()) // Si el personaje no está muerto, chequeo las quests del nivel para saber si puedo acceder al siguiente nivel
                    {
                        if(questManager.CheckIfAllQuestsAreCompleted())
                        {
                            questManager.ShowNextLevelPortal();
                            if(questManager.GetPortal().IsPlayerOnPortal()) isLoadingScene = true;
                        }  //Si las quests están completas, se mostrará el portal para acceder al siguiente nivel
                        else questManager.QuestChecker();
                    }
                    else // En caso de que el personaje esté muerto, Game Over
                    {
                        failCondiction = true;
                    }
                }

                else // Corrutina que carga la pantalla de Game Over
                {
                    isLoadingScene = true;
                    StartCoroutine(LoseSceneCo());
                    return;
                }
            }
            else
            {
                if(currentScene != sceneLoader.GetCurrentScene())
                {
                    if(sceneLoader.GetLevelSetChecker()) SetLevelSettings();
                }
            }

        }

        public IEnumerator LoseSceneCo()
        {
            sceneLoader.SetSceneToLoad(loseScene);
            yield return sceneLoader.TransitionBeginCo();
            yield return sceneLoader.TransitionEndCo();
        }

        void SetLevelSettings()
        {
            failCondiction = false;
            isLoadingScene = false;
            currentScene = sceneLoader.GetCurrentScene();
            player = GameObject.FindObjectOfType<PlayerHealth>();
            player.SetPlayerHealthStartingSettings();
            player.GetComponent<MagicPoints>().SetStartingMagicPointsSettings();
            //player.GetComponent<ItemInventory>().SetStartingInventorySettings();
            CameraFollower camaraFollower = GameObject.FindObjectOfType<CameraFollower>();
            questManager = GameObject.FindObjectOfType<QuestManager>();
            gameScreenManager.StartSettings();
            aiIDSetter.SetControllers(GameObject.FindObjectsOfType<AIController>());
            GameObject.FindObjectOfType<QuestManager>().StartingSettings();
            //GetComponent<Deathcounter>().RestartCounter();
            GameObject.FindObjectOfType<ArenaObstacle>().SetEventText();
            PlayLevelMusic();
        }

        private void PlayLevelMusic()
        {
            levelMusic = GameObject.FindObjectOfType<LevelMusic>();
            audioManager.PlayClip(audioManager.BGM, levelMusic.GetBGMClip());
            audioManager.PlayClip(audioManager.Ambience, levelMusic.GetAmbienceClip());
        }

        public void SavePlayerStats()
        {
            playerStatsManager.SetMainPlayerStats(player);
            playerStatsManager.SaveMainPlayerStats();
        }

        public void LoadPlayerStats()
        {
            playerStatsManager.LoadMainPlayerStats(player);
        }

        private void OnDestroy()
        {
            StopAllCoroutines();
        }
    }
}
