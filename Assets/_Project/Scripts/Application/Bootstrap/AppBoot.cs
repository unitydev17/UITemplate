using JetBrains.Annotations;
using UITemplate.Core.Controller;
using UITemplate.Presentation;
using VContainer.Unity;

namespace UITemplate.Application
{
    [UsedImplicitly]
    public class AppBoot : IStartable
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
            _ui.Run();
            _gameManager.Run();
            
        }
    }
}