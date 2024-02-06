using JetBrains.Annotations;
using VContainer.Unity;

namespace UITemplate.Presentation
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