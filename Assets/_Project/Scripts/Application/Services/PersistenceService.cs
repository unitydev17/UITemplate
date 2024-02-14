using System.Collections.Generic;
using JetBrains.Annotations;
using UITemplate.Core.DomainEntities;
using UITemplate.Core.Interfaces;
using UnityEngine;

namespace UITemplate.Application.Services
{
    [UsedImplicitly]
    public class PersistenceService : IPersistenceService
    {
        private const string SceneDataKey = "SceneData";
        private const string PlayerDataKey = "PlayerData";
        private PlayerData _playerData;

        public PersistenceService(PlayerData playerData)
        {
            _playerData = playerData;
        }

        public void SaveGameState(IEnumerable<Building> buildings)
        {
            SavePlayerData();
            SaveSceneData(buildings);
        }

        private static void SaveSceneData(IEnumerable<Building> buildings)
        {
            var data = (List<Building>)buildings;
            var value = JsonUtility.ToJson(data);
            PlayerPrefs.SetString(SceneDataKey, value);
        }

        public void LoadSceneData()
        {
            var data = PlayerPrefs.GetString(SceneDataKey);
            JsonUtility.FromJson<IEnumerable<Building>>(data);
        }

        private void SavePlayerData()
        {
            PlayerPrefs.SetString(PlayerDataKey, JsonUtility.ToJson(_playerData));
        }

        public void LoadPlayerData()
        {
            var data = PlayerPrefs.GetString(PlayerDataKey);
            _playerData = JsonUtility.FromJson<PlayerData>(data);
        }

    }
}