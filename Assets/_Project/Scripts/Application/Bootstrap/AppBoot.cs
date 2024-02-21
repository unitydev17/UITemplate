using System.Threading;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using UITemplate.Core.Controller;
using UITemplate.UI.Managers;
using VContainer.Unity;

namespace UITemplate.Application
{
    [UsedImplicitly]
    public class AppBoot : IAsyncStartable
    {
        private readonly UIManager _ui;
        private readonly GameManager _gameManager;

        public AppBoot(UIManager ui, GameManager gameManager)
        {
            _ui = ui;
            _gameManager = gameManager;
        }

        public async UniTask StartAsync(CancellationToken cancellation)
        {
            AppSetup();

            await _ui.Run();
            await _gameManager.Run();
        }

        private static void AppSetup()
        {
            UnityEngine.Device.Application.targetFrameRate = 60;
        }
    }
}