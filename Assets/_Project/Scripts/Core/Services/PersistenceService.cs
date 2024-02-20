using System;
using JetBrains.Annotations;
using UITemplate.Core.DomainEntities;
using UITemplate.Core.Interfaces;
using UnityEngine;

namespace UITemplate.Core.Services
{
    [UsedImplicitly]
    public class PersistenceService : IPersistenceService
    {
        private const string SceneDataKey = "SceneData";
        private const string PlayerDataKey = "PlayerData";
        private const string SettingsDataKey = "Settings";

        private readonly PlayerData _playerData;
        private readonly GameData _gameData;
        private readonly SettingsData _settingsData;

        public PersistenceService(PlayerData playerData, GameData gameData, SettingsData settingsData)
        {
            _playerData = playerData;
            _gameData = gameData;
            _settingsData = settingsData;
        }

        public void SaveAppState()
        {
            SavePlayerData();
            SaveSceneData();
            SaveSettings();
        }

        private void SaveSettings()
        {
            var value = JsonUtility.ToJson(_settingsData);
            PlayerPrefs.SetString(SettingsDataKey, value);
        }

        public bool LoadSettingsData()
        {
            var dataStr = PlayerPrefs.GetString(SettingsDataKey);
            if (string.IsNullOrEmpty(dataStr)) return false;

            var data = JsonUtility.FromJson<SettingsData>(dataStr);
            _settingsData.CopyFrom(data);

            return true;
        }

        private void SaveSceneData()
        {
            var value = JsonUtility.ToJson(_gameData);
            PlayerPrefs.SetString(SceneDataKey, value);
        }

        public bool LoadSceneData()
        {
            var dataStr = PlayerPrefs.GetString(SceneDataKey);
            if (string.IsNullOrEmpty(dataStr)) return false;

            var data = JsonUtility.FromJson<GameData>(dataStr);
            _gameData.CopyFrom(data);
            return true;
        }

        private void SavePlayerData()
        {
            _playerData.timer.gameExitTime = new TimeSpan(DateTime.UtcNow.Ticks).TotalSeconds;
            PlayerPrefs.SetString(PlayerDataKey, JsonUtility.ToJson(_playerData));
        }

        public bool LoadPlayerData()
        {
            var dataStr = PlayerPrefs.GetString(PlayerDataKey);
            if (string.IsNullOrEmpty(dataStr)) return false;

            var data = JsonUtility.FromJson<PlayerData>(dataStr);
            _playerData.CopyFrom(data);

            return true;
        }
    }
}