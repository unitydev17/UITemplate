using JetBrains.Annotations;
using UITemplate.Core.Controller;
using UITemplate.Core.Interfaces;
using UITemplate.UI.Managers;
using UITemplate.Utils;
using UniRx;
using VContainer.Unity;

namespace UITemplate.Application
{
    [UsedImplicitly]
    public class AppBoot : Registrable, IStartable
    {
        private readonly UIManager _ui;
        private readonly GameManager _gameManager;
        private readonly IPersistenceService _persistenceService;

        public AppBoot(UIManager ui, GameManager gameManager, IPersistenceService persistenceService)
        {
            _ui = ui;
            _gameManager = gameManager;
            _persistenceService = persistenceService;
        }

        public void Start()
        {
            AppSetup();

            _ui.Run();
            _gameManager.Run();
        }

        private void AppSetup()
        {
            RegisterSaveGameDataHandler();
            UnityEngine.Device.Application.targetFrameRate = 60;
        }

        private void RegisterSaveGameDataHandler()
        {
            Register(Observable.EveryApplicationFocus(), value =>
            {
                if (value == false) _persistenceService.SaveGameState();
            });
        }
    }
}