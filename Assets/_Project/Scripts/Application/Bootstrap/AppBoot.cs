using JetBrains.Annotations;
using UITemplate.Presentation;
using VContainer.Unity;

namespace UITemplate.Application
{
    [UsedImplicitly]
    public class AppBoot : IStartable
    {
        private readonly UIManager _ui;

        public AppBoot(UIManager ui)
        {
            _ui = ui;
        }

        public void Start()
        {
            _ui.Run();
        }
    }
}