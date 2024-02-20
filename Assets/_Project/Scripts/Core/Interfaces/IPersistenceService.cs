namespace UITemplate.Core.Interfaces
{
    public interface IPersistenceService
    {
        void SaveAppState();
        bool LoadSceneData();
        bool LoadPlayerData();
        bool LoadSettingsData();
    }
}