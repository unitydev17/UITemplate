using JetBrains.Annotations;
using UITemplate.Core.Controller;
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

        public AppBoot(UIManager ui, GameManager gameManager)
        {
            _ui = ui;
            _gameManager = gameManager;
        }

        public void Start()
        {
            AppSetup();

            _gameManager.Run();
            _ui.Run();
        }

        private void AppSetup()
        {
            Register(Observable.EveryApplicationFocus(), SaveGameState);
            UnityEngine.Device.Application.targetFrameRate = 60;
        }

        private void SaveGameState()
        {
            _gameManager.SaveGameState();
        }
    }
}