namespace UITemplate.Core.Interfaces
{
    public interface IPersistenceService
    {
        void SaveGameState();
        bool LoadSceneData();
        bool LoadPlayerData();
        bool LoadSettingsData();
    }
}