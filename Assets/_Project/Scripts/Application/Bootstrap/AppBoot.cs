using System.Threading;
using Cysharp.Threading.Tasks;
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
    public class AppBoot : Registrable, IAsyncStartable
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

        public async UniTask StartAsync(CancellationToken cancellation)
        {
            AppSetup();

            await _ui.Run();
            await _gameManager.Run();
        }

        private void AppSetup()
        {
            UnityEngine.Device.Application.targetFrameRate = 60;
            RegisterSaveGameDataHandler();
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