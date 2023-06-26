using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.GameCore
{
    public class AutoSavePlayerStatsDataManager : MonoBehaviour
    {
        private bool _canSave = true;
        [SerializeField] float _autoSaveTimer;
        private PlayerStatsManager _playerStatsManager;

        public void SetPlayerStatsManager(PlayerStatsManager psManager)
        {
            _playerStatsManager = psManager;
        }
        public bool CheckIfICanAutoSave()
        {
            return _canSave;
        }

        public IEnumerator autoSave()
        {
            _canSave = false;
            _playerStatsManager.SaveMainPlayerStats();
            yield return new WaitForSeconds(_autoSaveTimer);
            _canSave = true;
        }
    }

}