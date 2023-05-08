using Infrastructure.Entities.Progress;
using Infrastructure.Services.Progress;
using Shared;
using UnityEngine;

namespace Infrastructure.SaveLoad
{
    public class SaveLoadProgressService : ISaveLoadProgressService
    {
        private const string SAVED_KEY = "PLAYER_PROGRESS_DATA_KEY";

        private readonly IPersistentProgressService progressService;

        public PlayerProgress Load()
        {
            return PlayerPrefs.GetString(SAVED_KEY).ToDeserialized<PlayerProgress>();
        }

        public void Save()
        {
            PlayerProgress progress = progressService.Progress;
            string json = progress.ToJson();
            PlayerPrefs.SetString(SAVED_KEY, json);
        }
    }
}